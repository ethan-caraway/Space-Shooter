using System.Collections;
using UnityEngine;

public class Evader : MonoBehaviour
{
	// The Rigidbody of the enemy
	[SerializeField]
	private Rigidbody rb;

	// The bounds for the enemy to move within the scene
	[SerializeField]
	private Bounds bounds;

	// The angle at which the player will rotate when moving left and right
	[SerializeField]
	private float tilt;

	// The maximum speed the enemy can move on the X axis when evading
	[SerializeField]
	private float maxEvadeSpeed;

	// The rate of acceleration when evading
	[SerializeField]
	private float acceleration;

	// The range of time in seconds to randomly wait before evading the first time
	[SerializeField]
	private Vector2 startWait;

	// The range of time in seconds to randomly evade 
	[SerializeField]
	private Vector2 evadeTime;

	// The range of time in seconds to randomly wait before evading again
	[SerializeField]
	private Vector2 evadeWait;

	// The enemy's current speed on the Z axis
	private float currentSpeedZ;

	// The target speed on the Y axis for the evade
	private float targetSpeedX;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start ( )
	{
		// Store current speed on the Z axis
		currentSpeedZ = rb.linearVelocity.z;

		// Begin evading
		StartCoroutine ( Evade ( ) );
	}

	// FixedUpdate is called at a fixed rate for physics calculations
	void FixedUpdate ( )
	{
		// Accelerate the enemy's speed toward the target
		float currentSpeedX = Mathf.MoveTowards ( rb.linearVelocity.x, targetSpeedX, acceleration * Time.fixedDeltaTime );

		// Set the enemy's speed
		rb.linearVelocity = new Vector3 ( currentSpeedX, 0f, currentSpeedZ );

		// Clamp the enemy's position within the bounds of the scene
		rb.position = new Vector3 ( Mathf.Clamp ( rb.position.x, bounds.MinX, bounds.MaxX ), 0f, Mathf.Clamp ( rb.position.z, bounds.MinZ, bounds.MaxZ ) );

		// Rotate the enemy when moving on the X axis
		rb.rotation = Quaternion.Euler ( 0f, 0f, rb.linearVelocity.x * -tilt );
	}

	// Evade will contiuously apply evasive maneuvers to an enemy
	private IEnumerator Evade ( )
	{
		// Wait a random amount of time in seconds before the first evade
		yield return new WaitForSeconds ( Random.Range ( startWait.x, startWait.y ) );

		// Continuously evade
		while ( true )
		{
			// Get a direction based on the current position
			// Using Sign(x) ensures moving left when on the right side of the screen and moving right when on the left side of the screen
			float direction = -Mathf.Sign ( transform.position.x );

			// Get a random speed on the X axis
			targetSpeedX = Random.Range ( 1f, maxEvadeSpeed ) * direction;

			// Wait a random amount of time in seconds while the enemy is evading
			yield return new WaitForSeconds ( Random.Range ( evadeTime.x, evadeTime.y ) );

			// Stop any evading
			targetSpeedX = 0f;

			// Wait a random amount of time in seconds before evading again
			yield return new WaitForSeconds ( Random.Range ( evadeWait.x, evadeWait.y ) );
		}
	}
}
