using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BaseInteractionObject : MonoBehaviour //기본적인 인터렉션 스크립트
{
    [SerializeField]
    private UnityEvent enterEvent; //엔터 이벤트
    [SerializeField]
    private UnityEvent exitEvent; //Exit 이벤트
    [SerializeField]
    private UnityEvent activeEvent; //활성 이벤트
    [SerializeField]
    private UnityEvent deactiveEvent; //비활성 이벤트

    public virtual void Enter(OculusGOInputManager oculusGOInputManager)
    {
        enterEvent?.Invoke();
    }
    public virtual void Exit(OculusGOInputManager oculusGOInputManager)
    {
        exitEvent?.Invoke();
    }
    public virtual void Active(OculusGOInputManager oculusGOInputManager)
    {
        activeEvent.Invoke();
    }
    public virtual void Deactive(OculusGOInputManager oculusGOInputManager)
    {
        deactiveEvent.Invoke();
    }
}
