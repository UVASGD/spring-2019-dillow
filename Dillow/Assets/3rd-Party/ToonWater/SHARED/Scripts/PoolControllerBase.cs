﻿using UnityEngine;
using System.Collections;
using System;
#if !NO_PEGI
using PlayerAndEditorGUI;
#endif

namespace SharedTools_Stuff
{

    public interface IManageDestroyOnPlay
    {
        void DestroyYourself();
    }

    public interface IManageFading
    {
        void FadeAway();
        bool TryFadeIn();
    }

    public class PoolableBase : MonoBehaviour, IPEGI
    {
        public PoolControllerBase poolController;
        public int poolIndex;

        public virtual void Reboot(PoolControllerBase deactivator, int my_ind)
        {
            poolController = deactivator;
            poolIndex = my_ind;
        }

        public virtual bool PEGI()
        {
            return false;
        }

        public virtual void OnDestroy()
        {

            if (poolController != null)
                poolController.OnDuringDestroy(poolIndex);

        }

        public virtual void Deactivate()
        {
            if (poolController != null)
                poolController.Deactivate(poolIndex);

        }
    }

    public abstract class PoolControllerBase : IPEGI
    {
        public int initializedCount;
        public int activeMax;
        public int firstFree;
        protected int ExpandBy;
        protected int _BufferSize;
        public GameObject prefab;
        public int browsedObject = -1; // For Component Browsing
        protected int _activeAmount;
        public int ActiveAmount { get { return _activeAmount; } }

        protected abstract void ExpandArrays();
        protected abstract void Init(int bufferSize);
        public abstract void DeactivateAll();
        public abstract void DestroyAll();
        public abstract void OnDuringDestroy(int ind);
        public abstract void Deactivate(int i);
        #if !NO_PEGI
        public abstract bool PEGI();
        #endif
        public abstract bool ActiveSelf(int i);
        public abstract GameObject GetFreeGO();
        public abstract void AddToPool(GameObject go);
        public abstract Component GetScript();
        public abstract Component GetScript(int i);

        public PoolControllerBase(int lim, GameObject pref)
        {
            prefab = pref;
            Init(lim);
        }

    }

    public class PoolController<T> : PoolControllerBase where T : PoolableBase, new()
    {
        public T[] scripts;
        
        public ArrayManager<T> ScrptArray = new ArrayManager<T>();

        bool locked = false;

#if !NO_PEGI

        public override bool PEGI()
        {

            if (browsedObject != -1)
            {
                T tmp = scripts[browsedObject];
                if (((!tmp.gameObject.activeSelf)) || ("< Objects".Click()))
                    browsedObject = -1;
                else
                    tmp.Nested_Inspect();
            }

            pegi.newLine();

            if (browsedObject == -1)
                for (int i = 0; i < initializedCount; i++)
                {
                    T tmp = scripts[i];
                    if (tmp.gameObject.activeSelf)
                    {

                        string name = tmp.gameObject.name;
                        if (pegi.edit(ref name))
                            tmp.gameObject.name = name;

                        if (">".Click().nl())
                            browsedObject = i;
                    }
                }
            return false;
        }

#endif

        public override bool ActiveSelf(int i) => scripts[i].gameObject.activeSelf;
        

        protected override void ExpandArrays()
        {
            ScrptArray.Expand(ref scripts, ExpandBy);
            _BufferSize += ExpandBy;
        }

        void FirstFreeAdd()
        {
            firstFree++;
            activeMax = Mathf.Max(activeMax, firstFree);
            initializedCount = Mathf.Max(firstFree, initializedCount);
        }

        private T GetOrInstantiate()
        {
            if (scripts[firstFree] == null)
            {
                scripts[firstFree] = GameObject.Instantiate(prefab).GetComponent<T>();

                scripts[firstFree].Reboot(this, firstFree);

            }

            T tmp = scripts[firstFree];

            tmp.gameObject.SetActive(true);

            tmp.transform.parent = null;

            FirstFreeAdd();

            _activeAmount += 1;

            return tmp;
        }

        public override void AddToPool(GameObject go)
        {

            int before = firstFree;
            firstFree = initializedCount;

            if (firstFree >= _BufferSize) { ExpandArrays(); }


            scripts[firstFree] = go.GetComponent<T>();

            T tmp = scripts[firstFree];

            tmp.poolController = this;
            tmp.poolIndex = firstFree;


            tmp.gameObject.SetActive(true);

            tmp.transform.parent = null;
            FirstFreeAdd();


            firstFree = before; // Because we skipped the inactive ones

            _activeAmount++;
        }
        
        public T GetOne()
        {
            while ((firstFree < initializedCount) && (scripts[firstFree].gameObject.activeSelf) && (firstFree < _BufferSize)) firstFree++;
            if (firstFree >= _BufferSize) { ExpandArrays(); }

            return GetOrInstantiate();
        }

        public T GetOne(Transform parent)
        {
            T tmp = GetOne();
            tmp.transform.parent = parent;
            return tmp;
        }

        void ZeroCounts()
        {
            initializedCount = 0;
            firstFree = 0;
            _activeAmount = 0;
            activeMax = 0;
        }

        public override Component GetScript(int i) => scripts[i];
        
        public override Component GetScript() => GetOne();
        
        public override GameObject GetFreeGO() => GetOne().gameObject;
        

        protected override void Init(int bufferSize)
        {
            ZeroCounts();
            _BufferSize = bufferSize;
            ExpandBy = bufferSize;
            scripts = new T[bufferSize];
        }

        public PoolController(int lim, GameObject pref) : base(lim, pref)
        {
        }

        public override void DeactivateAll()
        {
            for (int i = 0; i < initializedCount; i++)
            {
                scripts[i].gameObject.SetActive(false);
            }

            firstFree = 0;
            _activeAmount = 0;
            activeMax = 0;
        }

        public override void OnDuringDestroy(int ind)
        {
            if (!locked)
            {

                var s = scripts[ind];
                if (s != null)
                {
                    _activeAmount--;

                }

                for (int i = ind; i < initializedCount - 1; i++)
                    if (scripts[i + 1] != null)
                    {
                        scripts[i] = scripts[i + 1];
                        scripts[i].poolIndex--;
                    }


                if (initializedCount > 0)
                    scripts[initializedCount - 1] = null;
                initializedCount--;
                firstFree = 0;
            }
        }

        public override void DestroyAll()
        {

            locked = true;

            for (int i = initializedCount - 1; i >= 0; i--)
                if (scripts[i] != null) {
                    scripts[i].gameObject.DestroyWhatever();
                    scripts[i] = null;
                }


            ZeroCounts();
            locked = false;
        }

        public override void Deactivate(int i)
        {

            if (scripts[i].gameObject.activeSelf)
                _activeAmount--;

            scripts[i].gameObject.SetActive(false);
            firstFree = Mathf.Min(i, firstFree);
            while ((activeMax > 0) && (!scripts[activeMax - 1].gameObject.activeSelf))
                activeMax--;
        }


    }
}