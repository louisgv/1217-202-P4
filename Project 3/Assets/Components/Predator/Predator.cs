using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Predator.
/// Default: Seeking and pursuing the closest Prey
/// Attached to: Predator, Zombie
/// </summary>
public class Predator : BoundedVehicle
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

	protected override Vector3 GetTotalSteeringForce ()
	{
		seekingTarget = targetPreySystem.FindNearestInstance (transform.position);

		var seekingForce = SteeringForce.GetSteeringForce (this, seekingTarget, SteeringMode.SEEKING);

		var totalForce = seekingForce;

		totalForce.y = 0;

		return Vector3.ClampMagnitude (totalForce, maxForce);
	}

	#endregion


	/// <summary>
	/// Draw debug line to current target
	/// </summary>
	private void OnRenderObject ()
	{
		if (seekingTarget == null) {
			return;
		}
		
		glLineMaterial.SetPass (0);

		GL.PushMatrix ();

		// Draw line to seeking target
		GL.Begin (GL.LINES);
		GL.Color (seekingLineColor);
		GL.Vertex (transform.position);
		GL.Vertex (seekingTarget.position);
		GL.End ();

		GL.PopMatrix ();

		Debug.DrawLine (transform.position, seekingTarget.position, seekingLineColor);
	}
}
