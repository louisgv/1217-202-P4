using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Predator.
/// Default: Seeking and pursuing the closest Prey
/// Attached to: Predator, Zombie
/// </summary>
public class Predator : SmartBoundedVehicle<PredatorCollider, Predator>
{
	public Material glLineMaterial;

	private Color seekingLineColor = Color.black;

	private Transform seekingTarget;

	private PreySystem targetPreySystem;

	/// <summary>
	/// Gets or sets the prey system instance.
	/// </summary>
	/// <value>The prey system instance.</value>
	public PreySystem TargetPreySystem {
		get {
			return targetPreySystem;
		}
		set {
			targetPreySystem = value;
		}
	}

	#region implemented abstract members of Vehicle

	/// <summary>
	/// Gets the steering force.
	/// </summary>
	/// <returns>The total steering force.</returns>
	protected override Vector3 GetTotalSteeringForce ()
	{
		seekingTarget = targetPreySystem.FindNearestInstance (transform.position);

		var seekingForce = SteeringForce.GetSteeringForce (this, seekingTarget, SteeringMode.SEEKING);

		var totalForce = seekingForce * seekingParams.ForceScale;

		// Check if seeking force is zero, if so add wandering behavior
		var wanderingForce = seekingForce.Equals (Vector3.zero) 
			? SteeringForce.GetWanderingForce (this)
			: Vector3.zero;

		totalForce += wanderingForce * wanderingParams.ForceScale;

		totalForce += GetTotalObstacleAvoidanceForce () * evadingParams.ForceScale;

		totalForce += GetTotalNeighborSeparationForce () * separationParams.ForceScale;

		totalForce += GetBoundingForce () * boundingParams.ForceScale;

		totalForce.y = 0;

		return Vector3.ClampMagnitude (totalForce, maxForce);
	}

	#endregion

	/// <summary>
	/// Draw debug line to current target
	/// </summary>
	protected override void OnRenderObject ()
	{
		glLineMaterial.SetPass (0);

		GL.PushMatrix ();

		base.OnRenderObject ();

		if (seekingTarget != null) {
			// Draw line to seeking target
			DrawDebugLine (seekingTarget.position, seekingLineColor);
		}

		GL.PopMatrix ();

	}
}
