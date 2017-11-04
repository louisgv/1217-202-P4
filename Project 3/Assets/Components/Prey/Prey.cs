using System.Collections;
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

	private Color fleeingLineColor = Color.green;

	private List<Transform> fleeingTargets;

	private PredatorSystem targetPredatorSystem;

	/// <summary>
	/// Gets or sets the target predator system.
	/// </summary>
	/// <value>The target predator system.</value>
	public PredatorSystem TargetPredatorSystem {
		get {
			return targetPredatorSystem;
		}
		set {
			targetPredatorSystem = value;
		}
	}

	#region implemented abstract members of Vehicle

	/// <summary>
	/// Gets the steering force.
	/// </summary>
	/// <returns>The total steering force.</returns>
	protected override Vector3 GetTotalSteeringForce ()
	{
		var totalForce = Vector3.zero;

		totalForce += SteeringForce.GetWanderingForce (this) * wanderingParams.ForceScale;

		totalForce += GetTotalFleeingForce () * fleeingParams.ForceScale;

		totalForce += GetTotalObstacleAvoidanceForce () * evadingParams.ForceScale;

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
	protected Vector3 GetTotalFleeingForce ()
	{
		var totalForce = Vector3.zero;
		// Get fleeing force:
		fleeingTargets = targetPredatorSystem.FindCloseProximityInstances (transform.position, fleeingParams.ThresholdSquared);

		foreach (var fleeingTarget in fleeingTargets) {
			var fleeingForce = SteeringForce.GetSteeringForce (this, fleeingTarget, SteeringMode.FLEEING);
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

		if (fleeingTargets != null) {
			foreach (var fleeingTarget in fleeingTargets) {
				DrawDebugLine (fleeingTarget.position, fleeingLineColor);
			}
		}

		GL.PopMatrix ();
	}
}
