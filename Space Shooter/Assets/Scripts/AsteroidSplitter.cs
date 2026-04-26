using UnityEngine;

public class AsteroidSplitter : MonoBehaviour
{
	// A constant value representing a 90 degree angle
	private const float RIGHT_ANGLE = 90;

	// The debris prefabs to spawn
	[SerializeField]
	private Mover [ ] debris;

	// The local offset position for spawning the left asteroid
	[SerializeField]
	private Vector3 leftOffset;

	// The local offset position for spawning the center asteroid
	[SerializeField]
	private Vector3 centerOffset;

	// The local offset position for spawning the right asteroid
	[SerializeField]
	private Vector3 rightOffset;

	// The min and max speed for the center asteroid
	[SerializeField]
	private Vector2 centerSpeedRange;

	// The min and max speed for the left and right asteroids
	[SerializeField]
	private Vector2 sideSpeedRange;

	// The max angle for the direction of the center asteroid
	[SerializeField]
	private float maxCenterAngle;

	// The min and max angle for the direction of the left and right asteroids
	[SerializeField]
	private Vector2 sideAngleRange;

	// Tracks whether or not the object is being destroyed due to a collision
	private bool isColliding;

	private void OnTriggerEnter ( Collider other )
	{
		// Check that it is not colliding with the boundary
		if ( other.tag != "Boundary")
		{
			// Mark that the object is colliding with something
			isColliding = true;
		}
	}

	// OnDestroy is called when a game object or component is destoryed
	private void OnDestroy ( )
	{
		// Check that the game object is being destroyed due to a collision
		if ( isColliding )
		{
			// Spawn a random center asteroid
			Mover center = Instantiate ( debris [ Random.Range ( 0, debris.Length ) ], transform.position + centerOffset, Quaternion.identity );

			// Give the center asteroid a random direction and speed
			center.SetDirection ( AngleToDirection ( Random.Range ( -maxCenterAngle, maxCenterAngle ) + RIGHT_ANGLE ), Random.Range ( centerSpeedRange.x, centerSpeedRange.y ) );
			
			// Spawn a random right asteroid
			Mover right = Instantiate ( debris [ Random.Range ( 0, debris.Length ) ], transform.position + rightOffset, Quaternion.identity );

			// Give the right asteroid a random direction and speed
			right.SetDirection ( AngleToDirection ( -Random.Range ( sideAngleRange.x, sideAngleRange.y ) + RIGHT_ANGLE ), Random.Range ( sideSpeedRange.x, sideSpeedRange.y ) );

			// Spawn a random left asteroid
			Mover left = Instantiate ( debris [ Random.Range ( 0, debris.Length ) ], transform.position + leftOffset, Quaternion.identity );

			// Give the left asteroid a random direction and speed
			left.SetDirection ( AngleToDirection ( Random.Range ( sideAngleRange.x, sideAngleRange.y ) + RIGHT_ANGLE ), Random.Range ( sideSpeedRange.x, sideSpeedRange.y ) );
		}
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
