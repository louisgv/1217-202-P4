﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Flocker.
/// A flocker's movement is heavily influenced by 
/// the Flocker System it belongs to
/// Author: LAB
/// Attached to: Flocker
/// </summary>
public class Flocker : SmartBoundedVehicle<Flocker, FlockerCollider, FlockerSystem>
{
	public Material glLineMaterial;

	private Vector3 totalForce;

	// This param includes the safe distance
	[SerializeField]
	public SteeringParams alignParams;

	[SerializeField]
	public SteeringParams cohesionParams;

	#region implemented abstract members of Vehicle

	/// <summary>
	/// Gets the steering force.
	/// </summary>
	/// <returns>The total steering force.</returns>
	protected override Vector3 GetTotalSteeringForce ()
	{
		totalForce = Vector3.zero;

		totalForce += SteeringForce.GetWanderingForce (this) * wanderingParams.ForceScale;

		totalForce += GetTotalObstacleAvoidanceForce () * avoidingParams.ForceScale;

		totalForce += GetTotalNeighborSeparationForce () * separationParams.ForceScale;

		totalForce += GetTotalNeighborAlignmentForce () * alignParams.ForceScale;

		totalForce += GetTotalNeighborCohesionForce () * cohesionParams.ForceScale;

		totalForce += GetBoundingForce () * boundingParams.ForceScale;

		totalForce.y = 0;

		return Vector3.ClampMagnitude (totalForce, maxForce);
	}

	#endregion


	/// <summary>
	/// Gets the total neighbor alignment force.
	/// </summary>
	/// <returns>The total neighbor alignment force.</returns>
	protected Vector3 GetTotalNeighborAlignmentForce ()
	{
		return SteeringForce.GetSteeringForce (this, ParentSystem.FlockAverageVelocityMap [GridCoordinate]);
	}

	/// <summary>
	/// Gets the total neighbor cohesion force.
	/// </summary>
	/// <returns>The total neighbor cohesion force.</returns>
	protected Vector3 GetTotalNeighborCohesionForce ()
	{
		return SteeringForce.GetSeekingForce (this, ParentSystem.FlockAveragePositionMap [GridCoordinate]);
	}

	protected override void Awake ()
	{
		base.Awake ();
	}

	protected override void Update ()
	{
		base.Update ();
		// Grab the direction from the system here
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();

		transform.position = BoundingPlane.GetSampledPosition (transform.position, ColliderInstance);
	}


	/// <summary>
	/// Draw debug line to current target
	/// </summary>
	protected override void OnRenderObject ()
	{
		glLineMaterial.SetPass (0);

		GL.PushMatrix ();

		base.OnRenderObject ();

		GL.PopMatrix ();
	}
}
