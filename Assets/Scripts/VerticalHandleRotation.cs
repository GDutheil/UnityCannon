using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VerticalHandleRotation : MonoBehaviour
{
    private XRSimpleInteractable interactable;
    private Vector3 lastHandleToHand;
    private float accAngle = 0;
    public ScriptableFloat controlFloat;
    public Vector3 HandleToHand;


    void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        interactable.selectEntered.AddListener(OnSelectEnter);
        interactable.selectExited.AddListener(OnSelectExit);
    }

    void OnSelectEnter(SelectEnterEventArgs args)
    {
        Transform controllerTransform = args.interactorObject.transform;
        lastHandleToHand = controllerTransform.position - transform.position;
        StartCoroutine(RotateCoroutine(args.interactorObject.transform));
    }

    void OnSelectExit(SelectExitEventArgs args)
    {
        // You can add additional logic here if needed when the object is released.
    }

    IEnumerator RotateCoroutine(Transform controllerTransform)
    {
        while (interactable.isSelected)
        {
            //print(lastHandToHandle);
            HandleToHand = projectOnRotationPlane(controllerTransform.position) - transform.position;
            //HandleToHand = projectOnRotationPlane(HandleToHand);
            float deltaAngle = Vector3.SignedAngle(HandleToHand, lastHandleToHand, transform.forward);
            //float deltaAngle = Vector3.Angle(HandleToHand, lastHandleToHand);
            print(deltaAngle);
            lastHandleToHand = HandleToHand;
            accAngle += deltaAngle;
            controlFloat.inputRotation = accAngle / 4;
            if (controlFloat.inputRotation < 25 && controlFloat.inputRotation > -90)
            {   
                Rotate(deltaAngle);
            }  
            
            yield return null;


        }
    }

    void Rotate(float deltaAngle)
    {
        transform.Rotate(Vector3.forward, deltaAngle, Space.Self);
    }

    Vector3 projectOnRotationPlane(Vector3 pos)
    {
        Vector3 handleRelative = transform.InverseTransformPoint(pos);
        handleRelative.z = 0.0f;
        Vector3 worldRelative = transform.TransformPoint(handleRelative);

        return worldRelative;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, HandleToHand);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, lastHandleToHand);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward);
    }

}