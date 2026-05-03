using UnityEngine;

public class PowerUp : MonoBehaviour
{
	// The attack data for this power-up
	[SerializeField]
	private AttackModel attack;

	// OnTriggerExit is called when a collider enters a trigger
	private void OnTriggerEnter ( Collider other )
	{
		// Check if the collision is with a player's bolt
		if ( other.tag == "PlayerBolt" )
		{
			// Get the player
			GameObject player = GameObject.FindWithTag ( "Player" );

			// Check for player
			if ( player != null )
			{
				// Give the player a power-up
				player.GetComponent<PlayerController> ( ).SetAttack ( attack );
			}
		}
	}
}
