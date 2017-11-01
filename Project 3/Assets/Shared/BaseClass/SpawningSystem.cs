using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawning system.
/// Author: LAB
/// Attached to: N/A
/// </summary>
public abstract class SpawningSystem : MonoBehaviour
{
	protected List<Vehicle> preyInstances;
	protected List<Vehicle> predatorInstances;

	protected List<CustomBoxCollider> preyColliderInstances;
	protected List<CustomBoxCollider> predatorColliderInstances;

	public Vehicle predatorPrefab;

	public Vehicle preyPrefab;

	/// <summary>
	/// Spawns the entities.
	/// </summary>
	protected abstract void SpawnEntities ();

	protected void Awake ()
	{
		preyInstances = new List<Vehicle> ();
		predatorInstances = new List<Vehicle> ();

		preyColliderInstances = new List<CustomBoxCollider> ();
		predatorColliderInstances = new List<CustomBoxCollider> ();

		SpawnEntities ();
	}

	/// <summary>
	/// Gets the prey instances.
	/// </summary>
	/// <value>The prey instances.</value>
	public List<Vehicle> PreyInstances {
		get {
			return preyInstances;
		}
	}

	/// <summary>
	/// Gets the prey collider instances.
	/// </summary>
	/// <value>The prey collider instances.</value>
	public List<CustomBoxCollider> PreyColliderInstances {
		get {
			return preyColliderInstances;
		}
	}

	/// <summary>
	/// Gets the predator instances.
	/// </summary>
	/// <value>The predator instances.</value>
	public List<Vehicle> PredatorInstances {
		get {
			return predatorInstances;
		}
	}
}
