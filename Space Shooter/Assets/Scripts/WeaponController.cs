using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	// The game controller in the scene
	[HideInInspector]
	public GameController Controller;

	// The audio source of the weapon
	[SerializeField]
	private AudioSource audioSource;

	// The transform location for spawning bolts
	[SerializeField]
	private Transform boltSpawn;

	// The prefab for the bolt
	[SerializeField]
	private DestroyByContact boltPrefab;

	// The amount of time in seconds before the first shot is to be fired
	[SerializeField]
	private float firstShotDelay;

	// The amount of shots fired in a burst
	[SerializeField]
	private int burstCount;

	// The amount of time in seconds before the next shot in the burst can be fired
	[SerializeField]
	private float burstDelay;

	// The amount of time in seconds before the next shot can be fired
	[SerializeField]
	private float fireRate;

	// The offset on the X axis for spawning each bolt
	[SerializeField]
	private float offset;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		// Start firing bolts
		StartCoroutine ( FireBolts ( ) );
    }

	// FireBolts will continuously fire bolts until the enemy is destroyed
	private IEnumerator FireBolts()
	{
		// Wait before firing the first shot
		yield return new WaitForSeconds ( firstShotDelay );

		// Continuously fire shots
		while(true)
		{
			// Fire shots for each burst
			for ( int i = 0; i < burstCount; i++ )
			{
				// Fire left bolt
				DestroyByContact leftInstance = Instantiate ( boltPrefab, boltSpawn.position + ( Vector3.left * offset ), boltSpawn.rotation );

				// Set game controller for the left bolt
				leftInstance.Controller = Controller;

				// Fire left bolt
				DestroyByContact rightInstance = Instantiate ( boltPrefab, boltSpawn.position + ( Vector3.right * offset ), boltSpawn.rotation );

				// Set game controller for the right bolt
				rightInstance.Controller = Controller;

				// Play the sound effect
				audioSource.Play ( );

				// Wait before firing the next shot in the burst
				yield return new WaitForSeconds ( burstDelay );
			}

			// Wait before firing the next shot
			yield return new WaitForSeconds ( fireRate );
		}
	}
}
