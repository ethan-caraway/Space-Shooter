using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	// The total number of arrows to display in the warning
	private const int ARROW_COUNT = 10;

	// The unicode character for double left facing arrows
	private const string LEFT_ARROW = "\u00AB";

	// The unicode character for double right facing arrows
	private const string RIGHT_ARROW = "\u00BB";

	// The text element for displaying the score
	[SerializeField]
	private TMP_Text scoreText;

	// The slider element for displaying the weapon charge status
	[SerializeField]
	private Slider chargeSlider;

	// The text element for displaying the weapon charge status
	[SerializeField]
	private TMP_Text chargeText;

	// The UI element containing the warning banner
	[SerializeField]
	private GameObject warningContainer;

	// The text element containing the left facing arrows on the right
	[SerializeField]
	private TMP_Text leftArrowText;

	// The text element containing the right facing arrows on the left
	[SerializeField]
	private TMP_Text rightArrowText;

	// The text element for displaying the warning
	[SerializeField]
	private TMP_Text warningText;

	// The list of hazard prefabs
	[SerializeField]
	private GameObject [ ] hazards;

	// The values for where a hazard can spawn
	[SerializeField]
	private Vector3 spawnRange;

	// The amount of hazards to spawn per wave
	[SerializeField]
	private int hazardsPerWave;

	// The amount of time in seconds to wait before spawning the first wave
	[SerializeField]
	private float startWait;

	// The amount of time in seconds to wait before spawning hazards
	[SerializeField]
	private float spawnWait;

	// The amount of time in seconds to wait before spawning the next wave
	[SerializeField]
	private float waveWait;

	// The amount of time in seconds to wait between arrows appearing
	[SerializeField]
	private float warningArrowRate;

	// The amount of time in seconds to wait after a warning appears
	[SerializeField]
	private float postWarningWait;

	// Whether or not the game has ended
	private bool isGameOver = false;

	// The current score of the player
	private int score = 0;

	// The current round of the game
	private int round = 1;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start ( )
	{
		// Update the UI
		DisplayScore ( );

		// Start spawning waves
		StartCoroutine ( SpawnWaves ( ) );
	}

	// GameOver will mark that the game has ended
	public void GameOver ( )
	{
		// Mark that the game is over
		isGameOver = true;
	}

	// AddScore increases the player's score
	public void AddScore ( int value )
	{
		// Increase score
		score += value;

		// Update the UI
		DisplayScore ( );
	}

	// UpdateWeaponCharge displays the current charge status for the player's weapon
	public void UpdateWeaponCharge ( float percentage )
	{
		// Check if charge status is ready
		if ( percentage >= 1f )
		{
			// Set charge slider as full
			chargeSlider.value = 1f;

			// Display that the player's weapon is ready to fire
			chargeText.text = "Ready to Fire!";
		}
		else
		{
			// Set the charge slider at the percentage
			chargeSlider.value = percentage;

			// Display that the player's weapon is charging
			chargeText.text = "Charging";
		}
	}

	// SpawnWaves will continuously spawn waves of hazards until the game ends
	private IEnumerator SpawnWaves ( )
	{
		// Wait before spawning the first wave
		yield return new WaitForSeconds ( startWait );

		// Wait for warning message
		yield return DisplayWarning ( "Hazards Incoming!" );

		// Wait for round message
		yield return DisplayWarning ( $"Round {round}" );

		// Wait before beginning the round
		yield return new WaitForSeconds ( postWarningWait );

		// Continuously spawn waves
		while ( true )
		{
			// Spawn each hazard in the wave
			for ( int i = 0; i < hazardsPerWave; i++ )
			{
				// Get a random hazard
				GameObject hazard = hazards [ Random.Range ( 0, hazards.Length ) ];

				// Spawn the random hazard
				SpawnHazard ( hazard );

				// Wait before spawning the next hazard
				yield return new WaitForSeconds ( spawnWait );

				// Check if the game can continue
				if ( isGameOver )
				{
					// Exit the infinite loop
					break;
				}
			}

			// Check if the game can continue
			if ( isGameOver )
			{
				// Exit the infinite loop
				break;
			}

			// Wait before spawning the next wave
			yield return new WaitForSeconds ( waveWait );

			// Increment the round count
			round++;

			// Wait for warning message
			yield return DisplayWarning ( "Hazards Incoming!" );

			// Wait for round message
			yield return DisplayWarning ( $"Round {round}" );

			// Wait before beginning the round
			yield return new WaitForSeconds ( postWarningWait );
		}
	}

	// SpawnHazard will spawn a given hazard at a random location
	private void SpawnHazard ( GameObject hazard )
	{
		// Get a random spawn position based on the range on the X axis but exact Y and Z axis
		Vector3 spawnPosition = new Vector3 ( Random.Range ( -spawnRange.x, spawnRange.x ), spawnRange.y, spawnRange.z );

		// Spawn the hazard at the spawn position
		GameObject instance = Instantiate ( hazard, spawnPosition, Quaternion.identity );

		// Assign this as the game controller to the hazard instance
		instance.GetComponent<DestroyByContact> ( ).Controller = this;

		// Get the potential weapon from the hazard
		WeaponController weapon = instance.GetComponent<WeaponController> ( );

		// Check for weapon
		if ( weapon != null )
		{
			// Assign this as the game controller to the weapon
			weapon.Controller = this;
		}
	}

	// DisplayScore updates the UI to display the current score
	private void DisplayScore ( )
	{
		// Format and display the score
		scoreText.text = $"Score:\n<b><color=orange>{score}</b></color>";
	}

	// DisplayWarning updates the UI with the message in the warning banner and animates the arrows
	private IEnumerator DisplayWarning ( string warning )
	{
		// Display the warning banner
		warningContainer.SetActive ( true );

		// Display the warning message
		warningText.text = warning;

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

		// Hide the warning banner
		warningContainer.SetActive ( false );
	}
}
