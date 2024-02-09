using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PowderLoading : MonoBehaviour
{
    public float sphereRadius = 0.2f;
    public FireCannon fireCannon;
    public ParticleSystem powderParticle;
    private XRGrabInteractable grabInteractable;

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
        while (grabInteractable.isSelected)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius);
            if (transform.eulerAngles.z == 180.0f) {
                foreach (Collider collider in colliders) {
                    if (collider.CompareTag("Rope")) {
                        fireCannon.enabled = true;
                    }
                }
                powderParticle.Play();
            }
            else {
                powderParticle.Stop();
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
