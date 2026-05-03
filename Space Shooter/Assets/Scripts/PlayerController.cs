using System.Collections;
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
	private const float LEFT_ANGLE = -90f;

	// The game controller in the scene
	[SerializeField]
	private GameController controller;

	// The rigidbody of the player
	[SerializeField]
	private Rigidbody rb;

	// The audio source of the player
	[SerializeField]
	private AudioSource audioSource;

	// The transform location for spawning bolts
	[SerializeField]
	private Transform boltSpawn;

	// The data for the player's attack
	[SerializeField]
	private AttackModel standardAttack;

	// The speed at which the player will move
	[SerializeField]
	private float speed;

	// The angle at which the player will rotate when moving left and right
	[SerializeField]
	private float tilt;

	// The bounds for the player to move within the scene
	[SerializeField]
	private Bounds bounds;

	// The movement input values along the X and Y axes
	private Vector2 input;

	// The data for the current attack type
	private AttackModel currentAttack;

	// The amount of shots left for the current attack
	private int currentAmmoCount;

	// The time the player wait until before the next shot can be fired
	private float nextFire;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start ( )
	{
		// Set the player's current attack
		currentAttack = standardAttack;

		// Hide attack HUD
		controller.UpdateAttack ( currentAttack, currentAmmoCount );
	}

	// Update is called every frame
	private void Update ( )
	{
		// Update the charge status UI
		controller.UpdateWeaponCharge ( ( currentAttack.FireRate - ( nextFire - Time.time ) ) / currentAttack.FireRate );
	}

	// FixedUpdate is called at a fixed rate for physics calculations
	private void FixedUpdate ( )
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
			nextFire = Time.time + currentAttack.FireRate;

			// Fire burst
			StartCoroutine ( Fire ( ) );

			

			
		}
	}

	// Fire shots each burst in an attack
	private IEnumerator Fire ( )
	{
		// Fire each bolt in the burst
		for ( int i = 0; i < currentAttack.BurstCount; i++ )
		{
			// Fire bolts at each angle
			for ( int j = 0; j < currentAttack.Angles.Length; j++ )
			{
				// Spawn a new bolt
				Mover bolt = Instantiate ( currentAttack.BoltPrefab, boltSpawn.position, boltSpawn.rotation );

				// Get firing angle
				float angle = Random.Range ( currentAttack.Angles [ j ].x, currentAttack.Angles [ j ].y );

				// Set the bolt's direction
				bolt.SetDirectionFromAngle ( angle + LEFT_ANGLE );
				bolt.transform.Rotate ( Vector3.back, angle );
			}

			// Decrease ammo
			currentAmmoCount -= currentAttack.Angles.Length;

			// Play sound effect
			audioSource.Play ( );

			// Update attack HUD
			controller.UpdateAttack ( currentAttack, currentAmmoCount );

			// Check for more shots in the burst
			if ( i + 1 < currentAttack.BurstCount )
			{
				// Wait before firing the next shot in the burst
				yield return new WaitForSeconds ( currentAttack.BurstDelay );
			}
		}

		// Check for power-up
		if ( currentAttack.Type != AttackModel.AttackType.STANDARD )
		{
			// Check if out of ammo
			if ( currentAmmoCount <= 0 )
			{
				// Remote power-up and reset attack
				SetAttack ( standardAttack );
			}
		}
	}

	// SetAttack sets the current attack data from a power-up
	public void SetAttack ( AttackModel attack )
	{
		// Set current attack
		currentAttack = attack;

		// Reset ammo
		currentAmmoCount = attack.AmmoCount;

		// Reset fire rate
		nextFire = Time.time + attack.FireRate;

		// Update attack HUD
		controller.UpdateAttack ( currentAttack, currentAmmoCount );
	}
}
