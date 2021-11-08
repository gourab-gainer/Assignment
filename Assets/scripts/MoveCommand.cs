using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    #region MyRegion

    //private readonly Vector3 _destination;
    /*private readonly GameObject apple;

    public MoveCommand(Vector3 destination, GameObject apple)
    {
      //  _destination = destination;
        this.apple = apple;
    }
    
    public override void Execute(Vector3 postion)
    {
        apple.transform.position = postion;
        //  _agent.SetDestination(_destination);
    }

    public override bool IsFinished { get; }// => _agent.remainingDistance <= 0.1f;*/

    #endregion
   
    public MoveCommand(DragItem apple,Pots[] temp)
    {
        //  _destination = destination;
        DragObject = apple;
        totalPots = temp;
    }
    public override DragItem DragObject { get; }
    Vector3 _startPosition;
    Vector3 _offsetToMouse;
    float _zDistanceToCamera;
    private Pots[] totalPots = new Pots[2];
    public Coroutine WrongTurnCoroutine;
    public override void OnBeginDrag()
    {
       // DraggedInstance = gameObject;
        _startPosition = DragObject.transform.position;
        _zDistanceToCamera = Mathf.Abs (_startPosition.z - Camera.main.transform.position.z);
 
        _offsetToMouse = _startPosition - Camera.main.ScreenToWorldPoint (
            new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
        );
    }

    public override void OnDrag()
    {
        if(Input.touchCount > 1)
            return;
        Debug.LogError("on dragg");
        DragObject.transform.position = Camera.main.ScreenToWorldPoint (
            new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
        ) + _offsetToMouse;
    }

    public override void OnEndDrag()
    {

        //  DraggedInstance = null;
        for (int i = 0; i < totalPots.Length; i++)
        {
            Debug.LogError("end dragg" +
                           Vector3.Distance(DragObject.transform.position, totalPots[i].transform.position));
            if (Vector3.Distance(DragObject.transform.position, totalPots[i].transform.position) < 1f)
            {

                if (totalPots[i].potItemType == DragObject.type)
                {
                    Debug.LogError("drag successfull");
                    DragObject.gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogError("drag unsuccessfull");
                    if (WrongTurnCoroutine != null) DragObject.StopCoroutine(WrongTurnCoroutine);
                    WrongTurnCoroutine = DragObject.StartCoroutine(MoveObject(DragObject.transform,
                        DragObject.transform.position, _startPosition, 1.5f,DragObject));

                    // DragObject.gameObject.SetActive(false);
                }

                return;
            }
            Debug.LogError("drag unsuccessfull");
            if (WrongTurnCoroutine != null) DragObject.StopCoroutine(WrongTurnCoroutine);
            WrongTurnCoroutine = DragObject.StartCoroutine(MoveObject(DragObject.transform,
                DragObject.transform.position, _startPosition, 1.5f,DragObject));
        }

        _offsetToMouse = Vector3.zero;
    }

    IEnumerator MoveObject ( Transform transform, Vector3 startpos,  Vector3 endPos,  float time, DragItem _item) {
        Debug.LogError(Time.time);
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startpos, endPos, i);
            yield return null;
        }
        /*obj.StopCoroutine(coroutine);*/
        Debug.LogError(Time.time);
        _item.DragMoveCommand = null;

    }
}
