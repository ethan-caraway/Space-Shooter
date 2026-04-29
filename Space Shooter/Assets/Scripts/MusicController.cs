using UnityEngine;

public class MusicController : MonoBehaviour
{
	// The singleton instance of the music manager
	private static MusicController instance;

	// Awake is called once at the start of the MonoBehaviour lifecycle
	private void Awake ( )
	{
		// Check if a singleton instance has been assigned
		if ( instance == null )
		{
			// Store this game object as the singleton instance
			instance = this;
		}
		else
		{
			// Destroy the non-singleton instance so that only one music controller exists
			Destroy ( gameObject );
		}
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start ( )
	{
		// Ensure that this instance persists from scene to scene
		DontDestroyOnLoad ( gameObject );
	}
}
