using UnityEngine;
using UnityEngine.InputSystem;

// A class that stores the bounds for movement within the scene
[System.Serializable]
public class Bounds
{
	// The minimum X bounds of the scene
	public float MinX;

	// The maximum X bounds of the scene
	public float MaxX;

	// The minimum Z bounds of the scene
	public float MinZ;

	// The maximum Z bounds of the scene
	public float MaxZ;
}

public class PlayerController : MonoBehaviour
{
	// The rigidbody of the player
	[SerializeField]
	private Rigidbody rb;

	// The audio source of the player
	[SerializeField]
	private AudioSource audioSource;

	// The transform location for spawning bolts
	[SerializeField]
	private Transform boltSpawn;

	// The prefab for the bolt
	[SerializeField]
	private GameObject boltPrefab;

	// The speed at which the player will move
	[SerializeField]
	private float speed;

	// The angle at which the player will rotate when moving left and right
	[SerializeField]
	private float tilt;

	// The amount of time in seconds before the next shot can be fired
	[SerializeField]
	private float fireRate;

	// The bounds for the player to move within the scene
	[SerializeField]
	private Bounds bounds;

	// The movement input values along the X and Y axes
	private Vector2 input;

	// The time the player wait until before the next shot can be fired
	private float nextFire;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // FixedUpdate is called at a fixed rate for physics calculations
    void FixedUpdate()
    {
		// Create a 3D vector using the X and Y input values
		Vector3 movement = new Vector3 ( input.x, 0f, input.y );

		// Set the velocity of the player
		rb.linearVelocity = movement * speed;

		// Clamp the player's position within the bounds of the scene
		rb.position = new Vector3 ( Mathf.Clamp ( rb.position.x, bounds.MinX, bounds.MaxX ), 0f, Mathf.Clamp ( rb.position.z, bounds.MinZ, bounds.MaxZ ) );

		// Rotate the player when moving on the X axis
		rb.rotation = Quaternion.Euler ( 0f, 0f, rb.linearVelocity.x * -tilt );
    }

	// OnMove is called when the Input System triggers the Move action
	private void OnMove ( InputValue moveValue )
	{
		// Convert the movement input value into a 2D vector
		input = moveValue.Get<Vector2> ( );
	}

	// OnAttack is called when the Input System triggers the Attack action
	private void OnAttack ( )
	{
		// Check if enough time has passed to fire again
		if ( Time.time >= nextFire )
		{
			// Store time when the next shot can be fired
			nextFire = Time.time + fireRate;

			// Spawn a new bolt
			Instantiate ( boltPrefab, boltSpawn.position, boltSpawn.rotation );

			// Play sound effect
			audioSource.Play ( );
		}
	}
}
