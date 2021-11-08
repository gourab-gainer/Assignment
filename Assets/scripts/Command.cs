using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract DragItem DragObject { get; }
    public abstract void OnBeginDrag();
    public abstract void OnDrag();
    public abstract void OnEndDrag();
}
