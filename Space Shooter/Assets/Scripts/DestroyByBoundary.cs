using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
	// OnTriggerExit is called when a collider exits this trigger
	private void OnTriggerExit ( Collider other )
	{
		// Delete any game object that exits the play area
		Destroy ( other.gameObject );
	}
}
