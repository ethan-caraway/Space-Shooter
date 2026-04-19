using UnityEngine;

public class Rotator : MonoBehaviour
{
	// The rigidbody of the object
	[SerializeField]
	private Rigidbody rb;

	// The speed at which the object will rotate
	[SerializeField]
	private float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		// Set a constant angular velocity at a random angle and a given speed
		// Converts the Euler angles from degrees to radians then scalar multiplies by speed
		rb.angularVelocity = Random.rotation.eulerAngles * Mathf.Deg2Rad * speed;
    }
}
