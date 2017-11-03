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
	EVASION,
	WANDERING
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
		GetEvasionForce,
		GetWanderingForce
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
	/// Gets the fleeing force.
	/// </summary>
	/// <returns>The fleeing force.</returns>
	/// <param name="target">Target.</param>
	internal static Vector3 GetFleeingForce (Vehicle vehicle, Vector3 target)
	{
		var diff = vehicle.transform.position - target;

		var desiredVelocity = diff.normalized * vehicle.MaxFleeingVelocity;

		var fleeingForce = desiredVelocity - vehicle.Velocity;

		return fleeingForce;
	}

	/// <summary>
	/// Gets the seeking force.
	/// </summary>
	/// <returns>The seeking force.</returns>
	/// <param name="target">Target.</param>
	internal static Vector3 GetSeekingForce (Vehicle vehicle, Vector3 target)
	{
		var diff = target - vehicle.transform.position;

		var desiredVelocity = diff.normalized * vehicle.MaxSeekingVelocity;

		var seekingForce = desiredVelocity - vehicle.Velocity;

		return seekingForce;
	}

	/// <summary>
	/// Gets the evasion force.
	/// </summary>
	/// <returns>The evasion force.</returns>
	/// <param name="target">Target.</param>
	internal static Vector3 GetEvasionForce (Vehicle vehicle, Vector3 target)
	{

		return Vector3.zero;
	}

	/// <summary>
	/// Gets the wandering force.
	/// </summary>
	/// <returns>The wandering force.</returns>
	/// <param name="target">Target.</param>
	internal static Vector3 GetWanderingForce (Vehicle vehicle, Vector3 target)
	{
		// Get a position slightly ahead of the vehicle's forward direction
		var vehicleForward = vehicle.transform.forward.normalized * 3.0f;

		// Get a random position
		var randomDirection = (Vector3)Random.insideUnitCircle;

		// Reconstruct the target position
		var finalTarget = vehicle.transform.position + vehicleForward + randomDirection;

		return GetSeekingForce (vehicle, finalTarget);
	}
}
