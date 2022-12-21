using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationManager
{
    public Animator animator;
    public MovementManager owner;

    public Transform LeftArmTarget;
    public IRigConstraint leftArm;
    public Transform RightArmTarget;
    public IRigConstraint rightArm;

    public Transform LeftFootTarget;
    public IRigConstraint leftLeg;
    public Transform RightFootTarget;
    public IRigConstraint rightLeg;

    public float speed = 10;
    private GameObject CurrentObject;
    private GameObject LastObject;

    private bool useLeftHand;

    public AnimationManager(MovementManager owner, Animator animator) {
        this.owner = owner;
        this.animator = animator;
    }

    public void OnUpdate() {
        if (!CurrentObject) {
            ResetIK();
            return;
        }

        if(useLeftHand) {
            LeftArmTarget.position = Vector3.MoveTowards(LeftArmTarget.position, CurrentObject.transform.position, speed * Time.deltaTime);
            leftArm.weight = 1;

            if (LastObject != null) {
                rightArm.weight = 1;
                RightArmTarget.position = Vector3.MoveTowards(RightArmTarget.position, LastObject.transform.position, speed * Time.deltaTime);
            }
            else
                rightArm.weight = 0;
        }
        else {
            RightArmTarget.position = Vector3.MoveTowards(RightArmTarget.position, CurrentObject.transform.position, speed * Time.deltaTime);
            rightArm.weight = 1;

            if (LastObject != null) {
                leftArm.weight = 1;
                LeftArmTarget.position = Vector3.MoveTowards(LeftArmTarget.position, LastObject.transform.position, speed * Time.deltaTime);
            }
            else
                leftArm.weight = 0;
        }
    }

    public void HandToObject(GameObject NewObject, bool switchHands) {
        if (switchHands) {
            useLeftHand = !useLeftHand;
            LastObject = CurrentObject;
        }
        else {
            LastObject = null;
        }

        CurrentObject = NewObject;
    }

    public void ResetIK() {
        useLeftHand = false;

        CurrentObject = null;
        LastObject = null;

        leftArm.weight = 0;
        rightArm.weight = 0;
        leftLeg.weight = 0;
        rightLeg.weight = 0;
    }
}