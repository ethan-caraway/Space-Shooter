using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		// Start spawning waves
		StartCoroutine ( SpawnWaves ( ) );
    }

	// SpawnWaves will continuously spawn waves of hazards until the game ends
	private IEnumerator SpawnWaves()
	{
		// Wait before spawning the first wave
		yield return new WaitForSeconds ( startWait );

		// Continuously spawn waves
		while(true)
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
			}

			// Wait before spawning the next wave
			yield return new WaitForSeconds ( waveWait );
		}
	}

	// SpawnHazard will spawn a given hazard at a random location
	private void SpawnHazard ( GameObject hazard )
	{
		// Get a random spawn position based on the range on the X axis but exact Y and Z axis
		Vector3 spawnPosition = new Vector3 ( Random.Range ( -spawnRange.x, spawnRange.x ), spawnRange.y, spawnRange.z );

		// Spawn the hazard at the spawn position
		Instantiate ( hazard, spawnPosition, Quaternion.identity );
	}
}
