using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Smart bounded vehicle.
/// It is a bounded vehicle that avoids obstacles
/// It is also smart enought to know that it is in a grid
/// and it is awared of the plane it can walk on
/// It is also smart because it know to pursue or evade instead
/// of normal seek/flee
/// Author: LAB
/// </summary>
[RequireComponent (typeof(C))]
abstract public class SmartBoundedVehicle <C, V>: BoundedVehicle 
	where C : CustomBoxCollider 
	where V: Vehicle // it's acyclic so it's fine
{
	[SerializeField]
	public SteeringParams separationParams;

	[SerializeField]
	public SteeringParams avoidingParams;

	/// <summary>
	/// Gets or sets the parent system.
	/// </summary>
	/// <value>The parent system.</value>
	public SpawningSystem<V> ParentSystem { get; set; }

	/// <summary>
	/// Gets or sets the target obstacle system.
	/// </summary>
	/// <value>The target obstacle system.</value>
	public ObstacleSystem TargetObstacleSystem { get; set; }

	/// <summary>
	/// Gets or sets the collider instance.
	/// </summary>
	/// <value>The collider instance.</value>
	public C ColliderInstance { get ; set ; }

	#region Unity Lifecycle

	protected override void Awake ()
	{
		ColliderInstance = GetComponent <C> ();
	}

	#endregion


	protected override void Reset ()
	{
		base.Reset ();

		ParentSystem.RenewVehicle (this as V);
	}

	/// <summary>
	/// Gets the total neighbor separation force.
	/// </summary>
	/// <returns>The total neighbor separation force.</returns>
	protected Vector3 GetTotalNeighborSeparationForce ()
	{
		var nearbyNeighbors = ParentSystem.FindCloseProximityInstances (this, ColliderInstance.ExtendSquared);

		return SteeringForce.GetNeighborSeparationForce (this, nearbyNeighbors);
	}

	/// <summary>
	/// Gets the total obstacle avoidance force.
	/// </summary>
	/// <returns>The total obstacle avoidance force.</returns>
	protected Vector3 GetTotalObstacleAvoidanceForce ()
	{
		var nearbyObstacles = TargetObstacleSystem.FindCloseProximityInstances (this, avoidingParams.ThresholdSquared);

		return SteeringForce.GetObstacleAvoidanceForce (this, nearbyObstacles);
	}

	/// <summary>
	/// Gets the target future position.
	/// </summary>
	/// <returns>The target future position.</returns>
	/// <param name="target">Target.</param>
	protected Vector3 GetTargetFuturePosition (Vehicle target)
	{
		var diff = target.transform.position - transform.position;

		var distanceSquared = diff.sqrMagnitude;

		float predictionTimeSquared = distanceSquared / MaxSteeringSpeedSquared;

		return target.transform.position + target.Velocity * Mathf.Sqrt (predictionTimeSquared);
	}

	/// <summary>
	/// Gets the pursuing force.
	/// </summary>
	/// <returns>The pursuing force.</returns>
	protected Vector3 GetPursuingForce (Vehicle target)
	{
		var finalTarget = GetTargetFuturePosition (target);

		target.FuturePosition = finalTarget;

		return SteeringForce.GetSeekingForce (this, finalTarget);
	}

	/// <summary>
	/// Gets the evading force.
	/// </summary>
	/// <returns>The evading force.</returns>
	/// <param name="target">Target.</param>
	protected Vector3 GetEvadingForce (Vehicle target)
	{
		var finalTarget = GetTargetFuturePosition (target);

		target.FuturePosition = finalTarget;

		return SteeringForce.GetFleeingForce (this, finalTarget);
	}
}