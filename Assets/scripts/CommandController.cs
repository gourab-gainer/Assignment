using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class CommandController : MonoBehaviour
{
    private GameObject draggingApple;
    private Queue<Command> _commands = new Queue<Command>();
    private Command _currentCommand;
    [SerializeField]private Pots[] totalPots = new Pots[2];
    private void Start()
    {
        DragItem[] allDragItems = FindObjectsOfType<DragItem>();
        foreach (var item in allDragItems)
        {
            ItemType temp = (ItemType)UnityEngine.Random.Range(0, 2);
            item.InitialiseItem(temp, totalPots);
        }
    }

    /*private void Update()
    {
        ListenForCommands();
        ProcessCommands();
    }*/

    /*private void ProcessCommands()
    {
        if(_currentCommand != null && _currentCommand.IsFinished == false)
            return;
        if (_commands.Any() == false) return;
        _currentCommand = _commands.Dequeue();
        _currentCommand.Execute();
    }*/

    private void ListenForCommands()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /*var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                _commands.Enqueue(new MoveCommand(hitInfo.point,_Agent));
            }*/
        }
    }
    private Vector3 screenPoint;
    private Vector3 offset;
 
    void OnMouseDown()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo))
        {
           // _commands.Enqueue(new MoveCommand(hitInfo.point,_Agent));
           screenPoint = Camera.main.WorldToScreenPoint(hitInfo.transform.position);
 
           offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
           
        }
        
        
    }
 
    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
 
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
 
    }

    private void OnMouseUp()
    {
     //   throw new NotImplementedException();
    }

}
