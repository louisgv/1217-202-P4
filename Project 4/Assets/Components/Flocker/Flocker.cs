using System.Collections;
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

		totalForce += GetBoundingForce () * boundingParams.ForceScale;

		totalForce.y = 0;

		return Vector3.ClampMagnitude (totalForce, maxForce);
	}

	#endregion


	/// <summary>
	/// Gets the total neighbor separation force.
	/// </summary>
	/// <returns>The total neighbor separation force.</returns>
	protected Vector3 GetTotalNeighborAlignmentForce ()
	{
		return SteeringForce.GetSeekingForce (this, 
			ParentSystem.FlockAveragePositionMap [GridCoordinate]);
		//		var nearbyNeighbors = ParentSystem.FindCloseProximityInstances (this, alignParams.);

		//		return SteeringForce.GetNeighborSeparationForce (this, nearbyNeighbors);
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
