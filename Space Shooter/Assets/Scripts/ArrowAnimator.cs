using System.Collections;
using TMPro;
using UnityEngine;

public class ArrowAnimator : MonoBehaviour
{
	// The total number of arrows to display in the warning
	private const int ARROW_COUNT = 10;

	// The unicode character for double left facing arrows
	private const string LEFT_ARROW = "\u00AB";

	// The unicode character for double right facing arrows
	private const string RIGHT_ARROW = "\u00BB";

	// The text element containing the left facing arrows on the right
	[SerializeField]
	private TMP_Text leftArrowText;

	// The text element containing the right facing arrows on the left
	[SerializeField]
	private TMP_Text rightArrowText;

	// The amount of time in seconds to wait between arrows appearing
	[SerializeField]
	private float warningArrowRate;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start ( )
	{
		// Continuously animate the arrows
		StartCoroutine ( AnimateArrows ( ) );
	}

	// AnimateArros updates the UI by animates the arrows in the menu
	private IEnumerator AnimateArrows (  )
	{
		// Continuously animate the arrows
		while ( true )
		{
			// Clear arrows
			leftArrowText.text = string.Empty;
			rightArrowText.text = string.Empty;

			// Display arrows one by one
			for ( int i = 0; i < ARROW_COUNT; i++ )
			{
				// Add arrows
				leftArrowText.text += LEFT_ARROW;
				rightArrowText.text += RIGHT_ARROW;

				// Wait before adding another arrow
				yield return new WaitForSeconds ( warningArrowRate );
			}
		}
	}
}
