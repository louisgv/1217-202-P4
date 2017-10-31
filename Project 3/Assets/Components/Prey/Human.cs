using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prey behavior.
/// Author: LAB
/// Attracted to: Human
/// </summary>
public class Human : Vehicle
{

	public Material glLineMaterial;

	private float fleeThresholdSquared = 36.0f;
	// Will be equivalent to a distance of 6 unity unit

	private Color seekingLineColor = Color.green;
	private Color fleeingLineColor = Color.white;


	#region implemented abstract members of Vehicle

	/// <summary>
	/// Gets the steering force.
	/// </summary>
	/// <returns>The steering force.</returns>
	protected override Vector3 GetSteeringForce ()
	{
		var seekingForce = SteeringForce.GetSeekingForce (this, seekingTarget.position) * seekingForceScale;

		var fleeingForce = ShouldFlee () 
			?	SteeringForce.GetFleeingForce (this, fleeingTarget.position) * fleeingForceScale
			:	Vector3.zero;
			
		var totalForce = seekingForce + fleeingForce;

		totalForce.y = 0;

		return Vector3.ClampMagnitude (totalForce, maxForce);
	}

	#endregion

	/// <summary>
	/// Shoulds human flee.
	/// </summary>
	/// <returns><c>true</c>, if flee was shoulded, <c>false</c> otherwise.</returns>
	private bool ShouldFlee ()
	{
		var diffVector = fleeingTarget.position - transform.position;

		float distanceSquared = Vector3.Dot (diffVector, diffVector);

		return distanceSquared < fleeThresholdSquared;
	}

	/// <summary>
	/// Draw debug line.
	/// </summary>
	private void OnRenderObject () // Examples of drawing lines – yours might be more complex!
	{
		glLineMaterial.SetPass (0);

		GL.PushMatrix ();

		// Draw line to seeking target
		GL.Begin (GL.LINES);
		GL.Color (seekingLineColor);
		GL.Vertex (transform.position);
		GL.Vertex (seekingTarget.position);
		GL.End ();

		var transformFlatPos = transform.position;
		transformFlatPos.y = 0;

		var fleeingTargetFlatPos = fleeingTarget.position;
		fleeingTargetFlatPos.y = 0;

		if (ShouldFlee ()) {
			// Draw line to fleeing target
			GL.Begin (GL.LINES);
			GL.Color (fleeingLineColor);
			GL.Vertex (transformFlatPos);
			GL.Vertex (fleeingTargetFlatPos);
			GL.End ();
		}


		GL.PopMatrix ();

		Debug.DrawLine (transform.position, seekingTarget.position, seekingLineColor);

		Debug.DrawLine (transformFlatPos, fleeingTargetFlatPos, fleeingLineColor);
	}

}