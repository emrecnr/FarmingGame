using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class State     
{
    public abstract void EnterState();
    public abstract void TickState();
    public abstract void ExitState();
}
