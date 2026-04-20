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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		for ( int i = 0; i < hazardsPerWave; i++ )
		{
			GameObject hazard = hazards [ Random.Range ( 0, hazards.Length ) ];
			SpawnHazard ( hazard );
		}
    }

    // Update is called once per frame
    void Update()
    {
        
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
