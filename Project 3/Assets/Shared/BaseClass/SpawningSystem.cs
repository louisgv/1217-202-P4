using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;

/// <summary>
/// Spawning system.
/// Author: LAB
/// Attached to: N/A
/// </summary>
public abstract class SpawningSystem <T>: MonoBehaviour where T : Component
{
	public int spawnCount = 9;

	public T prefab;

	[SerializeField]
	protected float gridWidth = 3.0f;

	protected CustomBoxCollider prefabCollider;

	protected Dictionary <string, HashSet<T>> instanceMap;

	protected Dictionary <string, HashSet<CustomBoxCollider>> colliderInstanceMap;

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

	public Vector3 GetGridCoordinate (Vector3 pos)
	{
		return new Vector3 ();
	}

	/// <summary>
	/// Add the specified instance.
	/// </summary>
	/// <param name="instance">Instance.</param>
	protected void RegisterVehicle (T instance)
	{
		int x = Mathf.CeilToInt (instance.transform.position.x / 2.0f);

		int y = Mathf.CeilToInt (instance.transform.position.y / 2.0f);

//		instanceMap [x, y] = (instance);

		colliderInstanceMap [x, y] (instance.GetComponent <CustomBoxCollider> ());
	}

	protected void Awake ()
	{
		instanceMap = new Dictionary<string, HashSet<T>> ();

		colliderInstanceMap = new Dictionary<string ,HashSet<CustomBoxCollider>> ();

		prefabCollider = prefab.GetComponent <CustomBoxCollider> ();

		SpawnEntities ();
	}

	/// <summary>
	/// Finds the a list of Transform surrounding a certain position.
	/// </summary>
	/// <returns>The nearest instance.</returns>
	/// <param name="pos">Position.</param>
	public List<Transform> FindCloseProximityInstances (Vector3 pos, float minDistanceSquared)
	{
		if (instanceMap == null || instanceMap.Count == 0) {
			return null;
		}

		// TODO: Add a delegate parameter here so we can define and reuse the same for loop instead
		var targets = new List<Transform> ();

		foreach (var instance in instanceMap) {
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
		if (instanceMap == null || instanceMap.Count == 0) {
			return null;
		}

		// Default to null
		Transform target = null;

		foreach (var prey in instanceMap) {
			var diffVector = prey.transform.position - pos;

			float distanceSquared = Vector3.Dot (diffVector, diffVector);

			if (minDistanceSquared > distanceSquared) {

				minDistanceSquared = distanceSquared;

				target = prey.transform;
			}
		}

		return target;
	}

	/// <summary>
	/// Gets the instance map.
	/// </summary>
	/// <value>The instance map.</value>
	public Dictionary<string, HashSet<T>> InstanceMap {
		get {
			return instanceMap;
		}
	}

	/// <summary>
	/// Gets the collider instance map.
	/// </summary>
	/// <value>The collider instance map.</value>
	public Dictionary<string, HashSet<CustomBoxCollider>> ColliderInstanceMap {
		get {
			return colliderInstanceMap;
		}
	}
}

