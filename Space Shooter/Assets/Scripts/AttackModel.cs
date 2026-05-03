using UnityEngine;

[System.Serializable]
public class AttackModel
{
	// The list of types of attacks from the player
	public enum AttackType
	{
		STANDARD,
		SPREAD_SHOT,
		DOUBLE_TAP,
		STAR_BURST,
		CHAIN_GUN,
		LASER
	}

	// The type of attack
	[SerializeField]
	private AttackType type;

	// The name of the attack
	[SerializeField]
	private string name;

	// The color associated with the attack
	[SerializeField]
	private Color32 attackColor;

	// The prefab of the bolt to be fired
	[SerializeField]
	private Mover boltPrefab;

	// The angle ranges to fire each bolt of an attack
	[SerializeField]
	private Vector2 [ ] angles;

	// The number of bolts to fire per burst
	[SerializeField]
	private int burstCount;

	// The amount of time in seconds before each shot in a burst is fired
	[SerializeField]
	private float burstDelay;

	// The amount of time in seconds before the next shot can be fired
	[SerializeField]
	private float fireRate;

	// The total amount of bolts that can be fired before the attach expires
	[SerializeField]
	private int ammoCount;

	// The read-only property for the type of attack
	public AttackType Type
	{
		get
		{
			return type;
		}
	}

	// The read-only property for the name of the attack
	public string Name
	{
		get
		{
			return name;
		}
	}

	// The read-only property for the color associated with the attack
	public Color32 AttackColor
	{
		get
		{
			return attackColor;
		}
	}

	// The read-only property for the prefab of the bolt to be fired
	public Mover BoltPrefab
	{
		get
		{
			return boltPrefab;
		}
	}

	// The read-only property for the angle ranges to fire each bolt on an attack
	public Vector2 [ ] Angles
	{
		get
		{
			return angles;
		}
	}

	// The read-only property for the number of bolts to fire per burst
	public int BurstCount
	{
		get
		{
			return burstCount;
		}
	}

	// The read-only property for the amount of time in seconds before each shot in a burst is fired
	public float BurstDelay
	{
		get
		{
			return burstDelay;
		}
	}

	// The read-only property for the amount of time in seconds before the next shot can be fired
	public float FireRate
	{
		get
		{
			return fireRate;
		}
	}

	// The read-only property for the total amount of bolts that can be fired before the attach expires
	public int AmmoCount
	{
		get
		{
			return ammoCount;
		}
	}
}
