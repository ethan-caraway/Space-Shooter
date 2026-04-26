using UnityEngine;

public class Mover : MonoBehaviour
{
	// The speed at which the object will move
	[SerializeField]
	private float speed;

	// The rigidbody of the object
	[SerializeField]
	private Rigidbody rb;

    // Awake is called once at the start of the MonoBehaviour lifecycle
    void Awake()
    {
		// Set a constant forward linear velocity at the given speed 
		rb.linearVelocity = Vector3.forward * speed;
    }

	// SetDirection will redirect the object at a given speed
	public void SetDirection ( Vector3 direction, float newSpeed )
	{
		// Set a constant linear velocity in a new direction and speed
		rb.linearVelocity = direction * newSpeed;
	}
}
