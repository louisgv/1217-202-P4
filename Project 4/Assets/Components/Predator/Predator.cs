﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Predator.
/// Default: Seeking and pursuing the closest Prey
/// Attached to: Predator, Zombie
/// </summary>
public class Predator : SmartBoundedVehicle<Predator, PredatorCollider, PredatorSystem>
{
	public Material glLineMaterial;

	private Color seekingLineColor = Color.black;

	private Transform seekingTarget;

	private Prey targetPrey;

	/// <summary>
	/// Gets or sets the prey system instance.
	/// </summary>
	/// <value>The prey system instance.</value>
	public PreySystem TargetPreySystem { get ; set ; }

	#region implemented abstract members of Vehicle

	/// <summary>
	/// Gets the steering force.
	/// </summary>
	/// <returns>The total steering force.</returns>
	protected override Vector3 GetTotalSteeringForce ()
	{
		var totalForce = Vector3.zero;

		seekingTarget = TargetPreySystem.FindNearestInstance (this);

		// Check if seeking target is null, if so add wandering behavior
		if (seekingTarget == null) {
			totalForce += SteeringForce.GetWanderingForce (this) * wanderingParams.ForceScale;
		} else {
			targetPrey = seekingTarget.GetComponent <Prey> ();
			totalForce += GetPursuingForce (targetPrey) * seekingParams.ForceScale;
		}

		totalForce += GetTotalObstacleAvoidanceForce () * avoidingParams.ForceScale;

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

			targetPrey.DrawFutureMarker (Color.magenta);
		}

		GL.PopMatrix ();
	}
}
