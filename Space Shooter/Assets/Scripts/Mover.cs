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

	// SetDirectionFromAngle will redirect the object at the current speed
	public void SetDirectionFromAngle ( float angle )
	{
		// Set the direction using the existing speed
		SetDirectionFromAngle ( angle, speed );
	}

	// SetDirectionFromAngle will redirect the object at a given speed
	public void SetDirectionFromAngle ( float angle, float newSpeed )
	{
		// Gets the rotation angle
		Vector3 direction = AngleToDirection ( angle );

		// Set a constant linear velocity in a new direction and speed
		rb.linearVelocity = direction.normalized * newSpeed;
	}

	// AngleToDirection is used to convet an angle in degrees to a directional vector on the X and Z axis
	private Vector3 AngleToDirection ( float angle )
	{
		// Gets the rotation angle
		Vector3 direction = Quaternion.AngleAxis ( angle, Vector3.up ) * Vector3.right;

		// Normalize the direction for equal speed distribution
		return direction.normalized;
	}
}
