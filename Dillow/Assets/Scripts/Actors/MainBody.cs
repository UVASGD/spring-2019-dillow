using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DeathDel();

public class MainBody : MonoBehaviour, ILockable, IMortal
{
    public DeathDel DeathEvent;

    public void Die() { DeathEvent?.Invoke(); }
}
