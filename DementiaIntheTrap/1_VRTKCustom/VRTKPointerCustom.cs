using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.Events;
public class VRTKPointerCustom : VRTK_Pointer
{
    [SerializeField]
    private OculusGOInputManager oculusGOInputManager; //추가적인 행동 명령을 수행하기 위한 스크립트
    [SerializeField]
    private UnityEvent enterEvent; //포인터가 들어왔을시 실행되는 이벤트 변수
    [SerializeField]
    private UnityEvent selectEvent; //선택했을 때 실행되는 이벤트 변수
    [SerializeField]
    private UnityEvent exitEvent; //포인터가 나갔을시 실행되는 이벤트 변수


    //Enter
    public override void PointerEnter(RaycastHit givenHit)
    {
        if (enabled && givenHit.transform != null && (!ControllerRequired() || VRTK_ControllerReference.IsValid(controllerReference)))
        {
            SetHoverSelectionTimer(givenHit.collider);
            DestinationMarkerEventArgs destinationEventArgs = SetDestinationMarkerEvent(givenHit.distance, givenHit.transform, givenHit, givenHit.point, controllerReference, false, GetCursorRotation());
            if (pointerRenderer != null && givenHit.collider != pointerRenderer.GetDestinationHit().collider)
            {
                OnDestinationMarkerEnter(destinationEventArgs);

                //현재 체크된 오브젝트가 인터렉션 가능한 오브젝트인지 판단
                BaseInteractionObject baseInteractionObject = givenHit.collider.GetComponent<BaseInteractionObject>();
                if(baseInteractionObject != null)
                {
                    oculusGOInputManager.InteractionObjectEnter(baseInteractionObject);
                }
                //Custom
                enterEvent?.Invoke();
            }
            else
            {
                OnDestinationMarkerHover(destinationEventArgs);
            }
            StartUseAction(givenHit.transform);

         

        }
    }
    //Select 
    protected override void ExecuteSelectionButtonAction()
    {
        if (EnabledPointerRenderer() && CanSelect() && (IsPointerActive() || wasActivated))
        {
            wasActivated = false;
            RaycastHit pointerRendererDestinationHit = pointerRenderer.GetDestinationHit();
            AttemptUseOnSet(pointerRendererDestinationHit.transform);
            if (pointerRendererDestinationHit.transform && IsPointerActive() && pointerRenderer.ValidPlayArea() && !PointerActivatesUseAction(pointerInteractableObject) && pointerRenderer.IsValidCollision())
            {
                ResetHoverSelectionTimer(pointerRendererDestinationHit.collider);
                ResetSelectionTimer();
                OnDestinationMarkerSet(SetDestinationMarkerEvent(pointerRendererDestinationHit.distance, pointerRendererDestinationHit.transform, pointerRendererDestinationHit, pointerRendererDestinationHit.point, controllerReference, false, GetCursorRotation()));

                //현재 체크된 오브젝트가 인터렉션 가능한 오브젝트인지 판단
                BaseInteractionObject baseInteractionObject = pointerRendererDestinationHit.collider.GetComponent<BaseInteractionObject>();
                if (baseInteractionObject != null)
                {
                    oculusGOInputManager.InteractionObjectSelect(baseInteractionObject);
                }
                //Custom
                selectEvent?.Invoke();
            }
        }
    }
    //Exit
    public override void PointerExit(RaycastHit givenHit)
    {
        ResetHoverSelectionTimer(givenHit.collider);
        if (givenHit.transform != null && (!ControllerRequired() || VRTK_ControllerReference.IsValid(controllerReference)))
        {
            OnDestinationMarkerExit(SetDestinationMarkerEvent(givenHit.distance, givenHit.transform, givenHit, givenHit.point, controllerReference, false, GetCursorRotation()));
            StopUseAction();

            //현재 체크된 오브젝트가 인터렉션 가능한 오브젝트인지 판단
            BaseInteractionObject baseInteractionObject = givenHit.collider.GetComponent<BaseInteractionObject>();
            if (baseInteractionObject != null)
            {
                oculusGOInputManager.InteractionObjectExit(baseInteractionObject);
            }

            //Custom
            exitEvent?.Invoke();
        }
    }
}
