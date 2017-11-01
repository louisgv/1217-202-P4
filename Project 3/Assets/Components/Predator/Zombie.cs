using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Chaser vehicle derived from vehicle.
/// Author: LAB
/// Attached to: Zombie
/// </summary>
public class Zombie : Vehicle
{
	public Material glLineMaterial;

	private Color seekingLineColor = Color.black;

	private SpawningSystem spawningSystem;

	private Transform seekingTarget;

	/// <summary>
	/// Gets or sets the spawning system.
	/// </summary>
	/// <value>The spawning system.</value>
	public SpawningSystem SpawningSystem {
		get {
			return spawningSystem;
		}
		set {
			spawningSystem = value;
		}
	}


	/// <summary>
	/// Finds the nearest human.
	/// </summary>
	/// <returns>The nearest human.</returns>
	private Transform FindNearestHuman ()
	{
		float minDistanceSquared = int.MaxValue;

		// Default to first prey in the instance array
		Transform target = spawningSystem.PreyInstances [0].transform;

		if (spawningSystem.PreyInstances.Count > 1) {
			foreach (var prey in spawningSystem.PreyInstances) {
				var diffVector = prey.transform.position - transform.position;

				float distanceSquared = Vector3.Dot (diffVector, diffVector);
				if (minDistanceSquared > distanceSquared) {
					
					minDistanceSquared = distanceSquared;
					
					target = prey.transform;
				}
			}
		}
		return target;
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	protected override void Start ()
	{
		base.Start ();
	}

	#region implemented abstract members of Vehicle

	/// <summary>
	/// Gets the steering force.
	/// </summary>
	/// <returns>The steering force.</returns>
	protected override Vector3 GetSteeringForce ()
	{
		seekingTarget = FindNearestHuman ();
		
		var seekingForce = SteeringForce.GetSeekingForce (this, seekingTarget.position) * seekingForceScale;

		var totalForce = seekingForce;

		totalForce.y = 0;

		return Vector3.ClampMagnitude (totalForce, maxForce);
	}

	#endregion

	/// <summary>
	/// Draw debug line to current target
	/// </summary>
	private void OnRenderObject () // Examples of drawing lines – yours might be more complex!
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