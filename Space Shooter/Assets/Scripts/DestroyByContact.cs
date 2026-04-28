using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	// The game controller in the scene
	[HideInInspector]
	public GameController Controller;

	// The explosion prefab for this hazard
	[SerializeField]
	private GameObject explosion;

	// The explosion prefab for the player
	[SerializeField]
	private GameObject playerExplosion;

	// The amount of score earned for destroying this object
	[SerializeField]
	private int scoreValue;

	// OnTriggerExit is called when a collider enters a trigger
	private void OnTriggerEnter ( Collider other )
	{
		// Ensure the hazard isn't destroyed for entering the play boundary
		if ( other.tag == "Boundary" )
		{
			return;
		}
		// Check if the collision is with the player
		else if ( other.tag == "Player" )
		{
			// Create the player explosion
			Instantiate ( playerExplosion, other.transform.position, other.transform.rotation );

			// Mark the game as over
			Controller.GameOver ( );
		}
		// Check if the collision is with a bolt from the player
		else if ( other.tag == "PlayerBolt" )
		{
			// Increase score
			Controller.AddScore ( scoreValue );
		}

		// Check for explosion
		if ( explosion != null )
		{
			// Create the explosion for this hazard
			Instantiate ( explosion, transform.position, transform.rotation );
		}

		// Destroy the other game object first
		Destroy ( other.gameObject );

		// Destroy this game object
		Destroy ( gameObject );
	}
}
