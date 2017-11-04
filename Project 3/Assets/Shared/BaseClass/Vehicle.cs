using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Vehicle base class.
/// Author LAB
/// Attached to: N/A
/// </summary>
public abstract class Vehicle : MonoBehaviour
{
	[SerializeField]
	private float mass = 1.0f;

	[SerializeField, Range (0, 10f)]
	protected float maxForce = 5.0f;

	/// <summary>
	/// The max fleeing velocity.
	/// </summary>
	[SerializeField, Range (0, 10f)]
	protected float maxSteeringSpeed = 1;

	/// <summary>
	/// Gets the max speed.
	/// </summary>
	/// <value>The max speed.</value>
	public float MaxSteeringSpeed {
		get {
			return maxSteeringSpeed;
		}
	}

	private Vector3 acceleration;

	private Vector3 direction;

	[SerializeField]
	public SteeringParams fleeingParams;

	[SerializeField]
	public SteeringParams seekingParams;

	[SerializeField]
	public SteeringParams evadingParams;

	[SerializeField]
	public SteeringParams wanderingParams;

	/// <summary>
	/// Gets the direction.
	/// </summary>
	/// <value>The direction.</value>
	public Vector3 Direction {
		get {
			return direction;
		}
	}

	private Vector3 velocity;

	/// <summary>
	/// Gets the velocity.
	/// </summary>
	/// <value>The velocity.</value>
	public Vector3 Velocity {
		get {
			return velocity;
		}
	}

	protected virtual void Awake ()
	{

	}

	protected virtual void Start ()
	{
		
	}

	/// <summary>
	/// Gets the steering force.
	/// </summary>
	protected abstract Vector3 GetTotalSteeringForce ();

	/// <summary>
	/// Applies ann add-on acceleration.
	/// </summary>
	/// <param name="addonAcceleration">Addon acceleration.</param>
	protected void ApplyAcceleration (Vector3 addonAcceleration)
	{
		acceleration += addonAcceleration;
	}

	/// <summary>
	/// Applies the force to our acceleartion.
	/// </summary>
	/// <param name="force">Force.</param>
	protected void ApplyForce (Vector3 force)
	{
		acceleration += force / mass;
	}

	/// <summary>
	/// Move this instance.
	/// </summary>
	protected void Move ()
	{
		velocity += acceleration * Time.deltaTime;

		direction = velocity.normalized;

		transform.position += velocity * Time.deltaTime;
	}

	/// <summary>
	/// Faces the moving direction.
	/// </summary>
	protected void RotateTowardMovingDirection ()
	{
		var rotationAngle = Mathf.Atan2 (direction.x, direction.z) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler (0, rotationAngle, 0);
	}

	/// <summary>
	/// Reset variable that has small rate of change, e.g acceleration
	/// </summary>
	protected void Reset ()
	{
		acceleration = Vector3.zero;
	}

	// Update is called once per frame
	protected virtual void LateUpdate ()
	{
		ApplyForce (GetTotalSteeringForce ());
		
		Move ();

		RotateTowardMovingDirection ();

		Reset ();
	}

	/// <summary>
	/// Raises the render object event.
	/// </summary>
	protected virtual void OnRenderObject ()
	{
		Debug.DrawLine (transform.position, transform.position + Velocity * 5.0f, Color.yellow);

		Debug.DrawLine (transform.position, transform.position + transform.right * 3.0f, Color.white);

		Debug.DrawLine (transform.position, transform.position + transform.forward * 3.0f, Color.red);
	}

}