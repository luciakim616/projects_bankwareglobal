using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OculusGOInputManager : MonoBehaviour 
{
    private BaseInteractionObject currentBaseInteractionObject; //현재 가르키는 오브젝트
    [SerializeField]
    private Transform grabPivot;
    [SerializeField]
    private OVRInput.Button deactiveButton; //오큘러스 릴리즈 버튼

    [SerializeField]
    private UnityEvent interactionObjectEnterEvent;
    [SerializeField]
    private UnityEvent interactionObjectSelectEvent;
    [SerializeField]
    private UnityEvent interactionObjectExitEvent;
    [SerializeField]
    private UnityEvent deactiveButtonEvent;
    private void Update()
    {
        InputCheck();
    }

    public Transform GetGrabPivot()
    {
        return grabPivot;
    }

    public void InteractionObjectEnter(BaseInteractionObject baseInteractionObject)
    {
        /* NONE  추가 스크립트 사용가능*/
        baseInteractionObject.Enter(this);
        interactionObjectEnterEvent?.Invoke();
    }
    public void InteractionObjectSelect(BaseInteractionObject baseInteractionObject)
    {
       
        currentBaseInteractionObject = baseInteractionObject;
        currentBaseInteractionObject.Active(this);
        interactionObjectSelectEvent?.Invoke();
       
    }
    public void InteractionObjectExit(BaseInteractionObject baseInteractionObject)
    {
        /* NONE  추가 스크립트 사용가능*/
        baseInteractionObject.Exit(this);
        interactionObjectExitEvent?.Invoke();
    }
    private void InputCheck()
    {
        DeactiveButtonCheck();
    }
    private void DeactiveButtonCheck()
    {
        if(OVRInput.GetDown(deactiveButton))
        {
            if (currentBaseInteractionObject != null)
            {
                currentBaseInteractionObject.Deactive(this);
                currentBaseInteractionObject = null;
                deactiveButtonEvent?.Invoke();
            }
        }
    }
}
