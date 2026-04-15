using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
	// The speed at which the background will move
	[SerializeField]
	private float speed;

	// The distance the background should move before resetting
	[SerializeField]
	private float distance;

	// The starting position of the background
	private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		// Store the starting position of the background
		startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		// Get a looping offset for moving the background
		float offset = Mathf.Repeat ( Time.time * speed, distance );

		// Move the background to the offset from the starting position
		transform.position = startPosition + ( Vector3.back * offset );
    }
}
