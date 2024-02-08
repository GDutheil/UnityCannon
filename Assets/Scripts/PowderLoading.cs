using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PowderLoading : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public float sphereRadius = 0.2f;
    public FireCannon fireCannon;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        StartCoroutine(CheckForRope());
    }

    private IEnumerator CheckForRope()
    {
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Rope"))
                {
                        fireCannon.enabled = true;
                }
            }

            yield return null;
        }
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }*/
}
