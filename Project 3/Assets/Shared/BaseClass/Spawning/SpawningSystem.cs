using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;
using System.ComponentModel;

/// <summary>
/// Spawning system.
/// Author: LAB
/// Attached to: N/A
/// </summary>
public abstract class SpawningSystem <T>: MonoBehaviour where T : SpawningGridComponent // Ack... Seems like T cannot be just plain component...
{
	public int spawnCount = 9;

	public T prefab;

	[SerializeField]
	protected float gridWidth = 3.0f;

	protected CustomBoxCollider prefabCollider;

	/// <summary>
	/// Gets the instance map.
	/// </summary>
	/// <value>The instance map.</value>
	protected Dictionary <string, HashSet<T>> InstanceMap {
		get;
		private set;
	}

	/// <summary>
	/// Gets the collider instance map.
	/// </summary>
	/// <value>The collider instance map.</value>
	protected Dictionary <string, HashSet<CustomBoxCollider>> ColliderInstanceMap {
		get;
		private set;
	}

	[SerializeField]
	protected CubePlaneCollider plane;

	/// <summary>
	/// Spawns an entity.
	/// </summary>
	protected abstract void SpawnEntity ();

	/// <summary>
	/// Spawns the entities.
	/// </summary>
	protected virtual void SpawnEntities ()
	{
		for (int i = 0; i < spawnCount; i++) {
			SpawnEntity ();
		}
	}

	protected virtual void Awake ()
	{
		InstanceMap = new Dictionary<string, HashSet<T>> ();

		ColliderInstanceMap = new Dictionary<string ,HashSet<CustomBoxCollider>> ();

		prefabCollider = prefab.GetComponent <CustomBoxCollider> ();

		SpawnEntities ();
	}


	/// <summary>
	/// Add the specified instance.
	/// </summary>
	/// <param name="instance">Instance.</param>
	public void RegisterVehicle (T instance)
	{
		var gridKey = SpawningGridCoordinate.GetGridKey (instance.transform, gridWidth);

		RegisterVehicle (gridKey, instance);
	}

	/// <summary>
	/// Registers the vehicle.
	/// </summary>
	/// <param name="gridKey">Grid key.</param>
	/// <param name="instance">Instance.</param>
	public void RegisterVehicle (string gridKey, T instance)
	{
		InstanceMap [gridKey].Add (instance);

		ColliderInstanceMap [gridKey].Add (instance.GetComponent <CustomBoxCollider> ());
	}

	/// <summary>
	/// Removes the vehicle.
	/// </summary>
	/// <param name="gridKey">Grid key.</param>
	/// <param name="instance">Instance.</param>
	public void RemoveVehicle (string gridKey, T instance)
	{
		InstanceMap [gridKey].Remove (instance);

		ColliderInstanceMap [gridKey].Remove (instance.GetComponent <CustomBoxCollider> ());
	}

	/// <summary>
	/// Swaps the instance grid if it should update itself.
	/// </summary>
	/// <param name="instance">Instance.</param>
	public void SwapInstanceGrid (T instance)
	{
		var newGrid = instance.UpdatedGrid ();

		if (newGrid == null) {
			return;
		}
		
		var currentGrid = instance.GridCoordinate;

		RemoveVehicle (currentGrid, instance);

		RegisterVehicle (newGrid, instance);
	}

	protected virtual void Start ()
	{
		// TODO: refresh the map every now and then...

	}


	/// <summary>
	/// Finds the a list of Transform surrounding a certain position.
	/// </summary>
	/// <returns>The nearest instance.</returns>
	/// <param name="pos">Position.</param>
	public List<Transform> FindCloseProximityInstances (Vector3 pos, float minDistanceSquared)
	{
		if (InstanceMap == null || InstanceMap.Count == 0) {
			return null;
		}

		// Grab the instance set coorespoinding to the current position...
		var gridKey = SpawningGridCoordinate.GetGridKey (pos, gridWidth);

		var instanceSet = InstanceMap [gridKey];

		// TODO: Add a delegate parameter here so we can define and reuse the same for loop instead
		var targets = new List<Transform> ();

		foreach (var instance in instanceSet) {
			var diffVector = instance.transform.position - pos;

			float distanceSquared = Vector3.Dot (diffVector, diffVector);

			if (minDistanceSquared > distanceSquared) {
				targets.Add (instance.transform);
			}
		}

		return targets;
	}

	/// <summary>
	/// Finds the nearest instance to given pos.
	/// </summary>
	/// <returns>The nearest instance.</returns>
	/// <param name="pos">Position.</param>
	public Transform FindNearestInstance (Vector3 pos, float minDistanceSquared = float.MaxValue)
	{
		if (InstanceMap == null || InstanceMap.Count == 0) {
			return null;
		}

		// Grab the instance set coorespoinding to the current position...
		var gridKey = SpawningGridCoordinate.GetGridKey ();

		var instanceSet = InstanceMap [gridKey];

		// Default to null
		Transform target = null;

		foreach (var prey in instanceSet) {
			var diffVector = prey.transform.position - pos;

			float distanceSquared = Vector3.Dot (diffVector, diffVector);

			if (minDistanceSquared > distanceSquared) {

				minDistanceSquared = distanceSquared;

				target = prey.transform;
			}
		}

		return target;
	}
}

