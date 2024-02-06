using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireCannon : MonoBehaviour
{
    public Rigidbody cannonBallPrefab;
    public float fireSpeed;

    public Transform cannonBallSpawnPoint; // Reference to the spawn point

    void Start()
    {
        if (cannonBallSpawnPoint == null)
        {
            Debug.LogError("CannonBallSpawnPoint not assigned! Assign it in the inspector.");
        }

        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnSelectEnter);
    }

    void OnSelectEnter(SelectEnterEventArgs args)
    {
        Fire();
    }

    void Fire()
    {
        // Instantiate the cannon ball
        Rigidbody newBall = Instantiate(cannonBallPrefab, cannonBallSpawnPoint.position, cannonBallSpawnPoint.rotation);

        // Give a velocity to the new ball (in the direction the spawn point is facing)
        newBall.velocity = cannonBallSpawnPoint.forward * fireSpeed;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (cannonBallSpawnPoint != null)
        {
            Gizmos.DrawSphere(cannonBallSpawnPoint.position, 0.1f);
        }
    }

    void Update()
    {
        // Additional logic can be added here if needed
    }
}