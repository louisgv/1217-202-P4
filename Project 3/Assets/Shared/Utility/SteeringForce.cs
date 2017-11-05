using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Steering mode public enum.
/// </summary>
public enum SteeringMode
{
	SEEKING,
	FLEEING,
	EVADING,
	AVOIDANCE
}

/// <summary>
/// Steering force static class.
/// All force function assumed mass is 1 and force is applied every 1 second
/// Author: LAB
/// Attached to: N/A
/// </summary>
public static class SteeringForce
{
	internal delegate Vector3 SteeringFx (Vehicle vehicle, Vector3 target);

	internal static SteeringFx[] steeringFunctions = {
		GetSeekingForce,
		GetFleeingForce,
		GetEvadingForce,
		GetAvoidanceForce
	};

	/// <summary>
	/// Gets the steering force.
	/// </summary>
	/// <returns>The steering force.</returns>
	/// <param name="steeringFx">Steering type.</param>
	/// <param name="targetTransform">Target transform.</param>
	internal static Vector3 GetSteeringForce (Vehicle vehicle, Transform targetTransform, SteeringMode sMode)
	{
		return targetTransform == null
			? Vector3.zero
			: steeringFunctions [(int)sMode] (vehicle, targetTransform.position);
	}

	/// <summary>
	/// Gets the steering force based on desired velocity.
	/// </summary>
	/// <returns>The steering force.</returns>
	/// <param name="desiredVelocity">Desired velocity.</param>
	internal static Vector3 GetSteeringForce (Vehicle vehicle, Vector3 desiredVelocity)
	{
		var steeringForce = desiredVelocity - vehicle.Velocity;

		return steeringForce;
	}


	/// <summary>
	/// Gets the fleeing force.
	/// </summary>
	/// <returns>The fleeing force.</returns>
	/// <param name="target">Target.</param>
	internal static Vector3 GetFleeingForce (Vehicle vehicle, Vector3 target)
	{
		var diff = vehicle.transform.position - target;

		var desiredVelocity = diff.normalized * vehicle.MaxSteeringSpeed;

		return GetSteeringForce (vehicle, desiredVelocity);
	}

	/// <summary>
	/// Gets the seeking force.
	/// </summary>
	/// <returns>The seeking force.</returns>
	/// <param name="target">Target.</param>
	internal static Vector3 GetSeekingForce (Vehicle vehicle, Vector3 target)
	{
		var diff = target - vehicle.transform.position;

		var desiredVelocity = diff.normalized * vehicle.MaxSteeringSpeed;

		return GetSteeringForce (vehicle, desiredVelocity);
	}

	/// <summary>
	/// Gets the evasion force.
	/// </summary>
	/// <returns>The evasion force.</returns>
	/// <param name="target">Target.</param>
	internal static Vector3 GetEvadingForce (Vehicle vehicle, Vector3 target)
	{
		return Vector3.zero;
	}

	/// <summary>
	/// Gets the wandering force.
	/// </summary>
	/// <returns>The wandering force.</returns>
	/// <param name="target">Target.</param>
	internal static Vector3 GetWanderingForce (Vehicle vehicle)
	{
		// Get a position slightly ahead of the vehicle's forward direction
		var wanderAnchor = vehicle.transform.position + vehicle.transform.forward * vehicle.wanderingParams.ThresholdSquared;

		// Get a random rotation position
		var randomDirection = Random.insideUnitSphere;

		// Reconstruct the target position
		var finalTarget = wanderAnchor + randomDirection;

		//	Debug.DrawLine (wanderAnchor, finalTarget, Color.black);

		return GetSeekingForce (vehicle, finalTarget);
	}

	/// <summary>
	/// Gets the bounding force.
	/// </summary>
	/// <returns>The bounding force.</returns>
	internal static Vector3 GetBoundingForce (Vehicle vehicle, CustomBoxCollider boundingPlane)
	{
		var minBound = boundingPlane.GetMinBound ();
		var maxBound = boundingPlane.GetMaxBound ();

		Vector3 desiredVelocity = Vector3.zero;

		float vehicleX = vehicle.transform.position.x;
		float vehicleZ = vehicle.transform.position.z;

		float maxSteeringSpeed = vehicle.MaxSteeringSpeed;

		if (vehicleX > maxBound.x) {
			desiredVelocity = new Vector3 (-maxSteeringSpeed, 0, vehicle.Velocity.z);
		} else if (vehicleX < minBound.x) {
			desiredVelocity = new Vector3 (maxSteeringSpeed, 0, vehicle.Velocity.z);
		} 

		if (vehicleZ > maxBound.z) {
			desiredVelocity = new Vector3 (vehicle.Velocity.x, 0, -maxSteeringSpeed);
		} else if (vehicleZ < minBound.z) {
			desiredVelocity = new Vector3 (vehicle.Velocity.x, 0, maxSteeringSpeed);
		}

		if (desiredVelocity.Equals (Vector3.zero)) {
			return Vector3.zero;
		}

		desiredVelocity = desiredVelocity.normalized * maxSteeringSpeed;

		return GetSteeringForce (vehicle, desiredVelocity);
	}

	/// <summary>
	/// Gets the obstacle avoidance force.
	/// </summary>
	/// <returns>The evasion force.</returns>
	/// <param name="target">Target.</param>
	internal static Vector3 GetAvoidanceForce (Vehicle vehicle, Vector3 target)
	{
		var diff = target - vehicle.transform.position;

		var forwardProjection = Vector3.Dot (diff, vehicle.transform.forward);

		if (forwardProjection < 0) {
			return Vector3.zero;
		}

		var rightProjection = Vector3.Dot (diff, vehicle.transform.right);

		var desiredDirection = (rightProjection > 0
		                       // Object to the right, turn left
			? -vehicle.transform.right
		                       // Object to the left, turn right
			: vehicle.transform.right);

		var desiredVelocity = desiredDirection.normalized * vehicle.MaxSteeringSpeed;

		return GetSteeringForce (vehicle, desiredVelocity);
	}

	/// <summary>
	/// Gets the obstacle avoidance force.
	/// </summary>
	/// <returns>The obstacle avoidance force.</returns>
	internal static Vector3 GetObstacleAvoidanceForce (Vehicle vehicle, List<Transform> nearbyObstacles)
	{
		var totalForce = Vector3.zero;

		foreach (var obstacle in nearbyObstacles) {
			totalForce += GetAvoidanceForce (vehicle, obstacle.position);
		}

		return totalForce;
	}

	/// <summary>
	/// Gets the neighbor separating force.
	/// </summary>
	/// <returns>The obstacle evading force.</returns>
	internal static Vector3 GetNeighborSeparationForce (Vehicle vehicle, List<Transform> nearbyNeighbors)
	{
		var sum = Vector3.zero;

		foreach (var neighbor in nearbyNeighbors) {
			// TODO: Add distance factor to each of these
			sum += (vehicle.transform.position - neighbor.transform.position).normalized;
		}

		var desiredVelocity = sum * vehicle.MaxSteeringSpeed / nearbyNeighbors.Count;

		return SteeringForce.GetSteeringForce (vehicle, desiredVelocity);
	}
}
