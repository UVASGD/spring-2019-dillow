﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using PlayerAndEditorGUI;

using SharedTools_Stuff;

namespace STD_Logic
{
    
    public class TaggedTarget: ValueIndex {  // if there are zero
        
        public int targValue; // if zero - we are talking about bool target

        public override StdEncoder Encode() {

            StdEncoder cody = new StdEncoder();
            cody.Add("g", groupIndex);
            cody.Add("t",triggerIndex);
            cody.Add_ifNotZero("v", targValue);

            return cody;
        }

        public override bool Decode(string tag, string data) {
            switch (tag) {
                case "g" : groupIndex = data.ToInt(); break;
                case "t": triggerIndex = data.ToInt(); break;
                case "v": targValue = data.ToInt(); break;
                default: return false;
            }
            return true;
        }
        
        public override bool IsBoolean() {
            return targValue == 0;
        }
        
        public string TagName => Trigger.name + (IsBoolean() ?  "" : Trigger.enm[triggerIndex]);

        public List<Values> GetObjectsByTag() {
            if (targValue > 0)
                return TriggerGroup.all[groupIndex].taggedInts[triggerIndex][targValue];
            else 
                return TriggerGroup.all[groupIndex].taggedBool[triggerIndex];
        }
#if !NO_PEGI
        public override bool PEGI() {
            bool changed = false;
           
            if (Trigger.editedTrigger != Trigger) {
                if (icon.Edit.Click(20))
                    Trigger.editedTrigger = Trigger;

                string focusName = "Tt";
                int index = pegi.NameNextUnique(ref focusName);

                string tmpname = Trigger.name;

                if (Trigger.focusIndex == index)
                    changed |= pegi.edit(ref Trigger.searchField);
                else
                    changed |= pegi.edit(ref tmpname);

            } else if (icon.Close.Click(20))
                Trigger.editedTrigger = null;

            return false;
        }

#endif
        

    }
    
    public static class TaggedExtensions
    {

        public static Values TryGetValues(this TaggedTarget trg, Values current)
        {
            if (trg != null)  {
                var val = trg.GetObjectsByTag();

                if (val != null && val.Count > 0)
                    current = val[0];

            } else if (current == null)
                current = Values.global;

            return current;
        } 

    }

}
