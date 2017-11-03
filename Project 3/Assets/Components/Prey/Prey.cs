using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prey: 
/// Default: Wandering around
/// If of close proximity to a predator: Run away
/// Attached to: Prey, Human
/// </summary>
public class Prey : BoundedVehicle
{
	public Material glLineMaterial;

	private Color fleeingLineColor = Color.green;

	private Transform fleeingTarget;

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

	protected override Vector3 GetTotalSteeringForce ()
	{
		// Get fleeing force:
		fleeingTarget = targetPredatorSystem.FindNearestInstance (transform.position, fleeingParams.ThresholdSquared);

		var fleeingForce = SteeringForce.GetSteeringForce (this, fleeingTarget, SteeringMode.FLEEING);

		var bouncingForce = GetBoundingForce ();

		var totalForce = fleeingForce + bouncingForce;

		totalForce.y = 0;

		return Vector3.ClampMagnitude (totalForce, maxForce);	
	}

	#endregion


	/// <summary>
	/// Draw debug line to current target
	/// </summary>
	private void OnRenderObject ()
	{
		if (fleeingTarget == null) {
			return;
		}

		glLineMaterial.SetPass (0);

		GL.PushMatrix ();

		// Draw line to seeking target
		GL.Begin (GL.LINES);
		GL.Color (fleeingLineColor);
		GL.Vertex (transform.position);
		GL.Vertex (fleeingTarget.position);
		GL.End ();

		GL.PopMatrix ();

		Debug.DrawLine (transform.position, fleeingTarget.position, fleeingLineColor);
	}
}
