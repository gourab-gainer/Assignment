using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
 
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
   // public static GameObject DraggedInstance;
 
    Vector3 _startPosition;
    Vector3 _offsetToMouse;
    float _zDistanceToCamera;
    public ItemType type;
    [SerializeField] private TextMeshProUGUI typeText;
    private Pots[] totalPots = new Pots[2];
    
    public void InitialiseItem(ItemType type,Pots[] temp)
    {
        this.type = type;
        typeText.text = Enum.GetName(typeof(ItemType), type);
        totalPots = temp;
    }

    public MoveCommand DragMoveCommand { get; set; }

    #region Interface Implementations
 
    public void OnBeginDrag (PointerEventData eventData)
    {
        if (DragMoveCommand == null)
        {
            DragMoveCommand = new MoveCommand(this, totalPots);
            DragMoveCommand.OnBeginDrag();
        }

    }
 
    public void OnDrag (PointerEventData eventData)
    {
        if (DragMoveCommand != null && DragMoveCommand.WrongTurnCoroutine == null)
        {
            DragMoveCommand.OnDrag();
        }

    }
 
    public void OnEndDrag (PointerEventData eventData)
    {
        if (DragMoveCommand != null && DragMoveCommand.WrongTurnCoroutine == null)
        {
            DragMoveCommand.OnEndDrag();
        }

    }
 
    #endregion
}

public enum ItemType
{
    A,
    a
}