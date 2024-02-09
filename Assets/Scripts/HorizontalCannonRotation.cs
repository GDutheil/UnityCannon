using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class HorizontalCannonRotation : MonoBehaviour
{
    public GameObject rotator;
    public float rotateSpeed = 1, thresholdAngle = 0.03f;
    Transform controller;
    XRSimpleInteractable interactable;
    Vector3 controllerDirection, controllerPosition, cannonPosition;
    Quaternion currentRot;
    Vector3 startPos;
    bool offsetSet;

    void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnSelectEnter);
        offsetSet = false;
    }

    void OnSelectEnter(SelectEnterEventArgs args)
    {
        controller = args.interactorObject.transform;
    }

    void SetOffsets()
    {
        if (offsetSet)
            return;
        controllerPosition = controller.transform.position;
        controllerPosition.y = 0;
        cannonPosition = transform.position;
        cannonPosition.y = 0;
        startPos = Vector3.Normalize(controllerPosition - cannonPosition);
        currentRot = rotator.transform.rotation;

        offsetSet = true;
    }

    void Update()
    {
        if (interactable.isSelected) {
            SetOffsets();
            controllerDirection = controller.transform.forward;
            controllerDirection.y = 0;
            Vector3 closestPoint = Vector3.Normalize(controller.transform.position - transform.position);
            var rot = Quaternion.FromToRotation(startPos, closestPoint);
            rot = Quaternion.Euler(0, rot.eulerAngles.y, 0);
            rotator.transform.rotation = rot * currentRot;
        }
        else {
            offsetSet = false;
        }

    }
}