using UnityEngine;

[System.Serializable]
public class AttackModel
{
	// The list of types of attacks from the player
	public enum AttackType
	{
		STANDARD,
		SPREAD_SHOT
	}

	// The type of attack
	[SerializeField]
	private AttackType type;

	// The name of the attack
	[SerializeField]
	private string name;

	// The prefab of the bolt to be fired
	[SerializeField]
	private Mover boltPrefab;

	// The angles to fire each bolt of an attack
	[SerializeField]
	private float [ ] angles;

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

	// The read-only property for the prefab of the bolt to be fired
	public Mover BoltPrefab
	{
		get
		{
			return boltPrefab;
		}
	}

	// The read-only property for the angles to fire each bolt on an attack
	public float [ ] Angles
	{
		get
		{
			return angles;
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
