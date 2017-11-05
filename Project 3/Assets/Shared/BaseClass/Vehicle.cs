using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Vehicle base class.
/// Author LAB
/// Attached to: N/A
/// </summary>
public abstract class Vehicle : SpawningGridComponent
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

	[SerializeField]
	public SteeringParams fleeingParams;

	[SerializeField]
	public SteeringParams seekingParams;

	[SerializeField]
	public SteeringParams evadingParams;

	[SerializeField]
	public SteeringParams pursuingParams;

	[SerializeField]
	public SteeringParams wanderingParams;

	/// <summary>
	/// Gets the direction.
	/// </summary>
	/// <value>The direction.</value>
	public Vector3 Direction { get; private set; }

	/// <summary>
	/// Gets the velocity.
	/// </summary>
	/// <value>The velocity.</value>
	public Vector3 Velocity { get ; private set; }

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
		Velocity += acceleration * Time.deltaTime;

		Direction = Velocity.normalized;

		transform.position += Velocity * Time.deltaTime;
	}

	/// <summary>
	/// Faces the moving direction.
	/// </summary>
	protected void RotateTowardMovingDirection ()
	{
		var rotationAngle = Mathf.Atan2 (Direction.x, Direction.z) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler (0, rotationAngle, 0);
	}

	/// <summary>
	/// Reset variable that has small rate of change, e.g acceleration
	/// </summary>
	protected virtual void Reset ()
	{
		acceleration = Vector3.zero;
	}

	#region Unity Lifecycle

	protected virtual void Awake ()
	{

	}

	protected virtual void Start ()
	{

	}

	protected virtual void Update ()
	{

	}

	protected virtual void LateUpdate ()
	{
		ApplyForce (GetTotalSteeringForce ());
		
		Move ();

		RotateTowardMovingDirection ();

		Reset ();
	}

	#endregion

	/// <summary>
	/// Draws the debug line from transform' center.
	/// </summary>
	protected void DrawDebugLine (Vector3 end, Color color)
	{
		Debug.DrawLine (transform.position, end, color);
		GL.Begin (GL.LINES);
		GL.Color (color);
		GL.Vertex (transform.position);
		GL.Vertex (end);
		GL.End ();
	}

	/// <summary>
	/// Raises the render object event.
	/// </summary>
	protected virtual void OnRenderObject ()
	{
//		DrawDebugLine (transform.position + Velocity * 5.0f, Color.yellow);

		DrawDebugLine (transform.position + transform.right * 3.0f, Color.white);

		DrawDebugLine (transform.position + transform.forward * 3.0f, Color.red);
	}

}