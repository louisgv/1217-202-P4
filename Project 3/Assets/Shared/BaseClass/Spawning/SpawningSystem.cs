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
public abstract class SpawningSystem <T>: MonoBehaviour 
	where T : SpawningGridComponent // Ack... Seems like T cannot be just plain component...
{
	public int spawnCount = 9;

	public T prefab;

	protected CustomBoxCollider prefabCollider;

	[SerializeField]
	protected CubePlaneCollider plane;

	// A x A grid
	protected int gridResolution = 5;

	// size is max plane size / res
	protected float gridSize;

	/// <summary>
	/// Gets the instance map.
	/// </summary>
	/// <value>The instance map.</value>
	protected Dictionary <SpawningGridCoordinate, HashSet<T>> InstanceMap {
		get;
		private set;
	}

	/// <summary>
	/// Gets the collider instance map.
	/// </summary>
	/// <value>The collider instance map.</value>
	protected Dictionary <SpawningGridCoordinate, HashSet<CustomBoxCollider>> ColliderInstanceMap {
		get;
		private set;
	}

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
		InstanceMap = new Dictionary<SpawningGridCoordinate, HashSet<T>> ();

		ColliderInstanceMap = new Dictionary<SpawningGridCoordinate, HashSet<CustomBoxCollider>> ();

		prefabCollider = prefab.GetComponent <CustomBoxCollider> ();

		var planeSize = plane.Size;

		gridSize = Mathf.Max (planeSize.x, planeSize.z) / gridResolution;

		SpawnEntities ();
	}


	/// <summary>
	/// Add the specified instance.
	/// </summary>
	/// <param name="instance">Instance.</param>
	public void RegisterVehicle (T instance)
	{
		var gridCoord = new SpawningGridCoordinate (instance.transform, gridSize, gridResolution);

		RegisterVehicle (gridCoord, instance);
	}

	/// <summary>
	/// Registers the vehicle.
	/// </summary>
	/// <param name="gridKey">Grid key.</param>
	/// <param name="instance">Instance.</param>
	public void RegisterVehicle (SpawningGridCoordinate gridKey, T instance)
	{
		if (!InstanceMap.ContainsKey (gridKey) ||
		    !ColliderInstanceMap.ContainsKey (gridKey)) {
			InstanceMap.Add (gridKey, new HashSet<T> ());
			ColliderInstanceMap.Add (gridKey, new HashSet<CustomBoxCollider> ());
		}

		instance.GridCoordinate = gridKey;

		InstanceMap [gridKey].Add (instance);

		ColliderInstanceMap [gridKey].Add (instance.GetComponent <CustomBoxCollider> ());
	}

	/// <summary>
	/// Removes the vehicle.
	/// </summary>
	/// <param name="gridKey">Grid key.</param>
	/// <param name="instance">Instance.</param>
	public void RemoveVehicle (SpawningGridCoordinate gridKey, T instance)
	{
		InstanceMap [gridKey].Remove (instance);

		ColliderInstanceMap [gridKey].Remove (instance.GetComponent <CustomBoxCollider> ());
	}

	/// <summary>
	/// Renews the vehicle's grid position.
	/// </summary>
	/// <param name="instance">Instance.</param>
	public void RenewVehicle (T instance)
	{
		var newGrid = instance.UpdatedGrid ();

		if (newGrid == null) {
			return;
		}
		
		var currentGrid = instance.GridCoordinate;

		RemoveVehicle (currentGrid, instance);

		RegisterVehicle (newGrid, instance);
	}

	#region Unity Lifecycle

	protected virtual void Start ()
	{

	}

	#endregion

	/// <summary>
	/// Finds the a list of Transform surrounding a certain position.
	/// </summary>
	/// <returns>The nearest instance.</returns>
	/// <param name="pos">Position.</param>
	public List<Transform> FindCloseProximityInstances (SpawningGridComponent inst, float minDistanceSquared)
	{
		if (InstanceMap == null || InstanceMap.Count == 0) {
			return null;
		}

		// TODO: Add a delegate parameter here so we can define and reuse the same for loop instead
		var targets = new List<Transform> ();

		for (int level = 0; level <= inst.GridCoordinate.MaxTracingLevel; level++) {
			var adjacentCoords = inst.GridCoordinate.GetAdjacentGrids (level);

			foreach (var coord in adjacentCoords) {
				if (!InstanceMap.ContainsKey (coord)) {
					continue;
				}

				var instanceSet = InstanceMap [coord];

				foreach (var instance in instanceSet) {
					var diffVector = instance.transform.position - inst.transform.position;
					
					float distanceSquared = Vector3.Dot (diffVector, diffVector);
					
					if (minDistanceSquared > distanceSquared) {
						targets.Add (instance.transform);
					}
				}
			}
		}

		return targets;
	}

	/// <summary>
	/// Finds the nearest instance to given pos.
	/// </summary>
	/// <returns>The nearest instance.</returns>
	/// <param name="pos">Position.</param>
	public Transform FindNearestInstance (SpawningGridComponent inst, float minDistanceSquared = float.MaxValue)
	{
		if (InstanceMap == null || InstanceMap.Count == 0) {
			return null;
		}

		var instanceSets = InstanceMap.Values;

		// Default to null
		Transform target = null;

		for (int level = 0; level <= inst.GridCoordinate.MaxTracingLevel; level++) {
			var adjacentCoords = inst.GridCoordinate.GetAdjacentGrids (level);

			foreach (var coord in adjacentCoords) {
				if (!InstanceMap.ContainsKey (coord)) {
					continue;
				}
				var instanceSet = InstanceMap [coord];

				foreach (var instance in instanceSet) {
					var diffVector = instance.transform.position - inst.transform.position;

					float distanceSquared = Vector3.Dot (diffVector, diffVector);

					if (minDistanceSquared > distanceSquared) {
						target = (instance.transform);
					}
				}
			}
			// If we found a potential target within an inner level,
			// then we don't have to check the outer level
			if (target != null) {
				break;
			}
		}

		return target;
	}
}

