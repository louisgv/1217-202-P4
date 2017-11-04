using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Smart bounded vehicle.
/// It is a bounded vehicle that avoids obstacles
/// Author: LAB
/// </summary>
[RequireComponent (typeof(T))]
abstract public class SmartBoundedVehicle <T>: BoundedVehicle where T : CustomBoxCollider
{
	protected ObstacleSystem targetObstacleSystem;

	private float safeRadius;

	protected T colliderInstance;

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
	public T ColliderInstance {
		get {
			return colliderInstance;
		}
		set {
			colliderInstance = value;
		}
	}


	private void Awake ()
	{
		colliderInstance = GetComponent <T> ();

		var xzSize = colliderInstance.Size;

		xzSize.y = 0;

		safeRadius = xzSize.magnitude;
	}

	/// <summary>
	/// Gets the obstacle evading force.
	/// </summary>
	/// <returns>The obstacle evading force.</returns>
	protected Vector3 GetObstacleEvadingForce ()
	{
		var totalForce = Vector3.zero;
		
		var nearbyObstacles = targetObstacleSystem.FindCloseProximityInstances (transform.position, evadingParams.ThresholdSquared);

		foreach (var obstacle in nearbyObstacles) {
			totalForce += SteeringForce.GetSteeringForce (this, obstacle, SteeringMode.EVADING);
		}

		return totalForce;
	}

	/// <summary>
	/// Raises the draw gizmos event.
	/// </summary>
	private void OnDrawGizmos ()
	{
		Gizmos.color = Color.black;

		Gizmos.DrawWireSphere (Vector3.Scale (new Vector3 (1, 0, 1), transform.position), safeRadius);
	}
}