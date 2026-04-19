using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
	// The amount of time in seconds before this game object is destroyed
	[SerializeField]
	private float delay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		// Delete this game object after the specified delay
		Destroy ( gameObject, delay );
    }
}
