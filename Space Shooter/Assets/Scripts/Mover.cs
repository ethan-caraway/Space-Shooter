using UnityEngine;

public class Mover : MonoBehaviour
{
	// The rigidbody of the object
	[SerializeField]
	private Rigidbody rb;

	// The speed at which the object will move
	[SerializeField]
	private float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		// Set a constant forward linear velocity at the given speed 
		rb.linearVelocity = Vector3.forward * speed;
    }
}
