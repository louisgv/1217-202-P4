﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prey: 
/// Default: Wandering around
/// If of close proximity to a predator: Run away
/// Attached to: Prey, Human
/// </summary>
public class Prey : SmartBoundedVehicle<PreyCollider, Prey>
{
	public Material glLineMaterial;

	private Color fleeingLineColor = Color.white;

	private List<Transform> fleeingTargets;

	/// <summary>
	/// Gets or sets the target predator system.
	/// </summary>
	/// <value>The target predator system.</value>
	public PredatorSystem TargetPredatorSystem { get; set; }

	#region implemented abstract members of Vehicle

	/// <summary>
	/// Gets the steering force.
	/// </summary>
	/// <returns>The total steering force.</returns>
	protected override Vector3 GetTotalSteeringForce ()
	{
		var totalForce = Vector3.zero;

		totalForce += SteeringForce.GetWanderingForce (this) * wanderingParams.ForceScale;

		totalForce += GetTotalEvadingForce () * fleeingParams.ForceScale;

		totalForce += GetTotalObstacleAvoidanceForce () * avoidingParams.ForceScale;

		totalForce += GetTotalNeighborSeparationForce () * separationParams.ForceScale;

		totalForce += GetBoundingForce () * boundingParams.ForceScale;

		totalForce.y = 0;

		return Vector3.ClampMagnitude (totalForce, maxForce);	
	}

	#endregion

	/// <summary>
	/// Gets the total fleeing force.
	/// </summary>
	/// <returns>The total fleeing force.</returns>
	protected Vector3 GetTotalEvadingForce ()
	{
		var totalForce = Vector3.zero;
		// Get fleeing force:
		fleeingTargets = TargetPredatorSystem.FindCloseProximityInstances (this, fleeingParams.ThresholdSquared);

		if (fleeingTargets == null) {
			return totalForce;
		}

		foreach (var fleeingTarget in fleeingTargets) {
			var targetPredator = fleeingTarget.GetComponent <Predator> ();

			var fleeingForce = GetEvadingForce (targetPredator);

			totalForce += fleeingForce;
		}

		return totalForce;
	}

	/// <summary>
	/// Draw debug line to current target
	/// </summary>
	protected override void OnRenderObject ()
	{
		glLineMaterial.SetPass (0);

		GL.PushMatrix ();

		base.OnRenderObject ();

		DrawDebugMark (FuturePosition, Color.magenta);

		if (fleeingTargets != null) {
			foreach (var fleeingTarget in fleeingTargets) {
				DrawDebugLine (fleeingTarget.position, fleeingLineColor);
			}
		}

		GL.PopMatrix ();
	}
}
