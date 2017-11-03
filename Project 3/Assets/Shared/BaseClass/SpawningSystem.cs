using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawning system.
/// Author: LAB
/// Attached to: N/A
/// </summary>
public abstract class SpawningSystem <T>: MonoBehaviour where T : Component
{
	public int spawnCount = 9;

	public T prefab;

	protected CustomBoxCollider prefabCollider;

	protected List<T> instances;

	protected List<CustomBoxCollider> colliderInstances;

	[SerializeField]
	protected CubePlaneCollider plane;

	/// <summary>
	/// Spawns the entities.
	/// </summary>
	protected abstract void SpawnEntity (int i);

	/// <summary>
	/// Add the specified instance.
	/// </summary>
	/// <param name="instance">Instance.</param>
	protected void RegisterVehicle (T instance)
	{
		instances.Add (instance);
		colliderInstances.Add (instance.GetComponent <CustomBoxCollider> ());
	}

	protected void Awake ()
	{
		instances = new List<T> ();
		colliderInstances = new List<CustomBoxCollider> ();

		prefabCollider = prefab.GetComponent <CustomBoxCollider> ();

		for (int i = 0; i < spawnCount; i++) {
			SpawnEntity (i);
		}
	}

	/// <summary>
	/// Finds the nearest instance to given pos.
	/// </summary>
	/// <returns>The nearest instance.</returns>
	/// <param name="pos">Position.</param>
	public Transform FindNearestInstance (Vector3 pos, float minDistanceSquared = float.MaxValue)
	{
		if (instances == null || instances.Count == 0) {
			return null;
		}

		// Default to null
		Transform target = null;

		foreach (var prey in instances) {
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
	/// Gets the spawned instances.
	/// </summary>
	/// <value>The prey instances.</value>
	public List<T> Instances {
		get {
			return instances;
		}
	}

	/// <summary>
	/// Gets the spawned collider instances.
	/// </summary>
	/// <value>The prey collider instances.</value>
	public List<CustomBoxCollider> ColliderInstances {
		get {
			return colliderInstances;
		}
	}

}
