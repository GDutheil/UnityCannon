using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireCannon : MonoBehaviour
{
    public Rigidbody cannonBallPrefab;
    public float fireSpeed;

    public Transform cannonBallSpawnPoint; 
    private PowderLoading powderLoading; 

    void Start()
    {
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnSelectEnter);

        powderLoading = FindObjectOfType<PowderLoading>(); 
        enabled = false;
    }

    void OnSelectEnter(SelectEnterEventArgs args)
    {
        Fire();
        enabled = false;
    }

    public void Fire()
    {
        if (!enabled)
            return;

        // Instantiate the cannon ball
        Rigidbody newBall = Instantiate(cannonBallPrefab, cannonBallSpawnPoint.position, cannonBallSpawnPoint.rotation);

        // Give a velocity to the new ball (in the direction the spawn point is facing)
        newBall.velocity = cannonBallSpawnPoint.forward * fireSpeed;
    }

/*    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (cannonBallSpawnPoint != null)
        {
            Gizmos.DrawSphere(cannonBallSpawnPoint.position, 0.1f);
        }
    }*/
}
