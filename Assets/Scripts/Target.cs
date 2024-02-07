using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    public GameObject targetBrokenPrefab;  // Prefab of the broken target parts
    public UnityEvent scored;
    
    void OnCollisionEnter(Collision other)
    {

        Debug.Log(gameObject.name + " collided with " + other.collider.name);
        Disintegrate();
        Score();
    }

    void Disintegrate()
    {
        // Instantiate the broken target parts at the same position
        GameObject brokenTarget = Instantiate(targetBrokenPrefab, transform.position, transform.rotation);

        // Optionally add randomness to initial velocities of the target parts
        Rigidbody[] rigidbodies = brokenTarget.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddRelativeForce(Vector3.down * Random.Range(1f, 5f), ForceMode.Impulse);
            rb.AddRelativeTorque(Random.onUnitSphere * Random.Range(1f, 5f), ForceMode.Impulse);
        }

        // Destroy the original target
        Destroy(gameObject);
        Destroy(brokenTarget, 3f);

    }

    void Score()
    {
        scored.Invoke();
    }
}