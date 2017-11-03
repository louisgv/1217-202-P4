using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prey: 
/// Default: Wandering around
/// If of close proximity to a predator: Run away
/// Attached to: Prey, Human
/// </summary>
public class Prey : Vehicle
{
	[SerializeField]
	private float fleeThresholdSquared = 36.0f;

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

	protected override Vector3 GetSteeringForce ()
	{
		fleeingTarget = targetPredatorSystem.FindNearestInstance (transform.position, fleeThresholdSquared);

		var fleeingForce = SteeringForce.GetSteeringForce (this, fleeingTarget, SteeringMode.FLEEING);

		var totalForce = fleeingForce;

		totalForce.y = 0;

		return Vector3.ClampMagnitude (totalForce, maxForce);	
	}

	#endregion


	/// <summary>
	/// Draw debug line to current target
	/// </summary>
	private void OnRenderObject () // Examples of drawing lines – yours might be more complex!
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
