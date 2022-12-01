using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : BaseInteractionObject //그랩 오브젝트
{
    //Position
    private Vector3 originPosition; //원래 있던 위치
    private Vector3 lerpSourcePosition; //선형보간 소스 위치
    private Vector3 lerpDestPosition; //선형보간 최종 위치
    //Rotation
    private Quaternion originRotation; //처음 회전 값
    private Quaternion lerpSourceRotation; //선형보간 소스 
    private Quaternion lerpDestRotation; //선형보간 최종간

    private bool isActive = false;
    //Animation Curve
    private float deltaTime = 0;
    [SerializeField]
    private AnimationCurve movementAnimationCurve;
    private float endMovementAnimationCurveTime = 0;
    [SerializeField]
    private float movementSpeed = 1;
    private void Start()
    {
        originPosition = transform.position; //최초 위치기억
        originRotation = transform.rotation;
        endMovementAnimationCurveTime = movementAnimationCurve[movementAnimationCurve.length - 1].time;

    }
    private void Update()
    {
        if(isActive)
        {
            deltaTime += Time.deltaTime;

            float evaluateValue = movementAnimationCurve.Evaluate(deltaTime);
            transform.localPosition = Vector3.Lerp(lerpSourcePosition, lerpDestPosition, evaluateValue);
            transform.localRotation = Quaternion.Lerp(lerpSourceRotation, lerpDestRotation, evaluateValue);
            if(deltaTime > endMovementAnimationCurveTime)
            {
                isActive = false;
            }
        }
    }
    public override void Active(OculusGOInputManager oculusGOInputManager)
    {
        isActive = true;
        deltaTime = 0;
        transform.parent = oculusGOInputManager.transform; //자식으로 들어감

        lerpSourcePosition = transform.position;
        lerpDestPosition = oculusGOInputManager.GetGrabPivot().localPosition;

        lerpSourceRotation = transform.rotation;
        lerpDestRotation = oculusGOInputManager.GetGrabPivot().localRotation;

        base.Active(oculusGOInputManager);
    }
    public override void Deactive(OculusGOInputManager oculusGOInputManager)
    {
        isActive = true;
        deltaTime = 0;
        transform.parent = null; //자식으로 들어감

        lerpSourcePosition = transform.position;
        lerpDestPosition = originPosition;

        lerpSourceRotation = transform.rotation;
        lerpDestRotation = originRotation;

        base.Deactive(oculusGOInputManager);
    }
}
