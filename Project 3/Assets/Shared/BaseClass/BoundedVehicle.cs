using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Vehicle base class.
/// Author LAB
/// Attached to: N/A
/// </summary>
public abstract class BoundedVehicle : Vehicle
{
	[SerializeField]
	public SteeringParams boundingParams;

	protected CubePlaneCollider boundingPlane;

	/// <summary>
	/// Gets or sets the bounding plane.
	/// </summary>
	/// <value>The bounding plane.</value>
	public CubePlaneCollider BoundingPlane {
		get {
			return boundingPlane;
		}
		set {
			boundingPlane = value;
		}
	}

	/// <summary>
	/// Gets the bounding force.
	/// </summary>
	/// <returns>The bounding force.</returns>
	protected Vector3 GetBoundingForce ()
	{
		var minBound = boundingPlane.GetMinBound ();
		var maxBound = boundingPlane.GetMaxBound ();

//		Vector3 desiredDirection = Direction;
//
//		if (transform.position.x > maxBound.x || transform.position.x < minBound.x) {
//			desiredDirection.x *= -1.0f;
//		} else if (transform.position.z > maxBound.z || transform.position.z < minBound.z) {
//			desiredDirection.z *= -1.0f;
//		}
//
//		if (desiredDirection.Equals (Direction)) {
//			return Vector3.zero;
//		}
//
//		var desiredVelocity = desiredDirection.normalized * boundingParams.MaxSpeed;

		Vector3 desiredVelocity = Vector3.zero;

		if (transform.position.x > maxBound.x) {
			desiredVelocity = new Vector3 (-boundingParams.MaxSpeed, 0, Velocity.z);
		} else if (transform.position.x < minBound.x) {
			desiredVelocity = new Vector3 (boundingParams.MaxSpeed, 0, Velocity.z);
		} 

		if (transform.position.z > maxBound.z) {
			desiredVelocity = new Vector3 (Velocity.x, 0, -boundingParams.MaxSpeed);
		} else if (transform.position.z < minBound.z) {
			desiredVelocity = new Vector3 (Velocity.x, 0, boundingParams.MaxSpeed);
		}

		if (desiredVelocity.Equals (Vector3.zero)) {
			return Vector3.zero;
		}

		desiredVelocity = desiredVelocity.normalized * boundingParams.MaxSpeed;

		return SteeringForce.GetSteeringForce (this, desiredVelocity) * boundingParams.ForceScale;
	}
}