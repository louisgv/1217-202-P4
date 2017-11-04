using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Smart bounded vehicle.
/// It is a bounded vehicle that avoids obstacles
/// Author: LAB
/// </summary>
[RequireComponent (typeof(C))]
abstract public class SmartBoundedVehicle <C, V>: BoundedVehicle where C : CustomBoxCollider where V: Vehicle
{
	protected ObstacleSystem targetObstacleSystem;

	protected C colliderInstance;

	protected SpawningSystem<V> parentSystem;

	[SerializeField]
	public SteeringParams separationParams;

	[SerializeField]
	public SteeringParams avoidingParams;

	/// <summary>
	/// Gets or sets the parent system.
	/// </summary>
	/// <value>The parent system.</value>
	public SpawningSystem<V> ParentSystem {
		get {
			return parentSystem;
		}
		set {
			parentSystem = value;
		}
	}

	/// <summary>
	/// Gets or sets the target obstacle system.
	/// </summary>
	/// <value>The target obstacle system.</value>
	public ObstacleSystem TargetObstacleSystem {
		get {
			return targetObstacleSystem;
		}
		set {
			targetObstacleSystem = value;
		}
	}

	/// <summary>
	/// Gets or sets the collider instance.
	/// </summary>
	/// <value>The collider instance.</value>
	public C ColliderInstance {
		get {
			return colliderInstance;
		}
		set {
			colliderInstance = value;
		}
	}

	protected override void Awake ()
	{
		colliderInstance = GetComponent <C> ();
	}

	/// <summary>
	/// Gets the total neighbor separation force.
	/// </summary>
	/// <returns>The total neighbor separation force.</returns>
	protected Vector3 GetTotalNeighborSeparationForce ()
	{
		var nearbyNeighbors = parentSystem.FindCloseProximityInstances (transform.position, separationParams.ThresholdSquared);

		return SteeringForce.GetNeighborSeparationForce (this, nearbyNeighbors);
	}

	/// <summary>
	/// Gets the total obstacle avoidance force.
	/// </summary>
	/// <returns>The total obstacle avoidance force.</returns>
	protected Vector3 GetTotalObstacleAvoidanceForce ()
	{
		var nearbyObstacles = targetObstacleSystem.FindCloseProximityInstances (transform.position, avoidingParams.ThresholdSquared);

		return SteeringForce.GetObstacleAvoidanceForce (this, nearbyObstacles);
	}


}