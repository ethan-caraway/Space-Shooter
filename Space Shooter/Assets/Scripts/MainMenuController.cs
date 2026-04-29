using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	// PlayGame loads the Space scene
	public void PlayGame ( )
	{
		// Load the Space scene
		SceneManager.LoadScene ( "Space" );
	}

	// QuitGame closes the application
	public void QuitGame ( )
	{
		// Output message to the console for feedback when in the editor
		Debug.Log ( "Quitting game" );

		// Exit to the desktop
		Application.Quit ( );
	}
}
