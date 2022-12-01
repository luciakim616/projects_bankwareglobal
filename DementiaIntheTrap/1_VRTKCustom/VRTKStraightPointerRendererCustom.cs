using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

//기존 VRTK Straight Pointer Renderer 스크립트를 그랩 및 인터렉션을 하기 위해서 상속받는 커스텀 클레스
public class VRTKStraightPointerRendererCustom : VRTK_StraightPointerRenderer
{
    [SerializeField]
    private LayerMask interactionLayerMask; //인터렉션을 받는 레이어들을 설정하는 변수

    protected override float CastRayForward() //전방으로 나가는 레이캐스트 함수를 인터렉션 하기 위한 함수
    {
        Transform origin = GetOrigin();
        Ray pointerRaycast = new Ray(origin.position, origin.forward);
        RaycastHit pointerCollidedWith;
        bool rayHit = VRTK_CustomRaycast.Raycast(customRaycast, pointerRaycast, out pointerCollidedWith, ~interactionLayerMask.value, maximumLength);

        CheckRayMiss(rayHit, pointerCollidedWith);
        CheckRayHit(rayHit, pointerCollidedWith);

        float actualLength = maximumLength;
        if (rayHit && pointerCollidedWith.distance < maximumLength)
        {
            actualLength = pointerCollidedWith.distance;
        }

        return OverrideBeamLength(actualLength);
    }
}
