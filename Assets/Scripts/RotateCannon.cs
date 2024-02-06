using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class RotateCannon : MonoBehaviour
{
    public GameObject rotator;
    public Transform leftController;
    public float rotateSpeed, thresholdAngle;
    private XRSimpleInteractable interactable;
    private Vector3 grabDirection, controllerDirection, cannonDirection;
    private Quaternion currentRot;
    private Vector3 startPos;
    private bool offsetSet;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnSelectEnter);
        offsetSet = false;
    }

    void OnSelectEnter(SelectEnterEventArgs args)
    {
        // grabDirection = args.interactableObject.transform.position - args.interactorObject.transform.position;

        // print("grab dir : " + grabDirection);
        // print("controller dir : " + args.interactorObject.transform.forward);
    }

    void SetOffsets()
    {
        if (offsetSet)
            return;
        startPos = Vector3.Normalize(leftController.transform.position - transform.position);
        currentRot = rotator.transform.rotation;
        print("start point : " + startPos);

        offsetSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.isSelected)
        {
            SetOffsets();
            controllerDirection = leftController.transform.forward;
            cannonDirection = rotator.transform.forward;
            Vector3 closestPoint = Vector3.Normalize(leftController.transform.position - transform.position);
            // print("closest point : " + closestPoint);
            var rot = Quaternion.FromToRotation(startPos, closestPoint);
            rot = Quaternion.Euler(0, rot.eulerAngles.y, 0);
            rotator.transform.rotation = rot * currentRot;
            // print("rotating " + rotator.transform.rotation);
        }
        else
        {
            offsetSet = false;
        }

    }
}