using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Vehicle base class.
/// Author LAB
/// Attached to: N/A
/// </summary>
public class Vehicle : MonoBehaviour
{
	[SerializeField]
	private float mass;

	public float gravityAcceleration = 0.98f;

	public bool grounded;

	private Vector3 acceleration;

	private Vector3 direction;

	[SerializeField]
	internal float maxFleeingVelocity;

	[SerializeField]
	internal float maxSeekingVelocity;

	[SerializeField]
	internal float maxEvasionVelocity;

	[SerializeField]
	internal float maxWanderingVelocity;

	internal CustomBoxCollider boxCollider;

	public Vector3 Direction {
		get {
			return direction;
		}
	}

	private Vector3 velocity;

	public Vector3 Velocity {
		get {
			return velocity;
		}
	}

	/// <summary>
	/// Applies ann add-on acceleration.
	/// </summary>
	/// <param name="addonAcceleration">Addon acceleration.</param>
	internal void ApplyAcceleration (Vector3 addonAcceleration)
	{
		acceleration += addonAcceleration;
	}

	/// <summary>
	/// Applies the force to our acceleartion.
	/// </summary>
	/// <param name="force">Force.</param>
	internal void ApplyForce (Vector3 force)
	{
		acceleration += force / mass;
	}

	/// <summary>
	/// Move this instance.
	/// </summary>
	internal void Move ()
	{
		velocity += acceleration * Time.deltaTime;

		direction = velocity.normalized;

		transform.position += velocity * Time.deltaTime;
	}

	// Update is called once per frame
	internal void LateUpdate ()
	{
		if (grounded) {
			ApplyAcceleration (Vector3.down * gravityAcceleration);
		}
		
		Move ();
	}
}