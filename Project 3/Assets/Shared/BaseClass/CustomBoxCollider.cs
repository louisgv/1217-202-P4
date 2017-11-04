using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Custom box collider.
/// It uttilizes AABB collision algorithm to check for collision
/// It is also used to calculate the square extend of an obstacle
/// </summary>
public abstract class CustomBoxCollider : MonoBehaviour
{
	[SerializeField]
	protected Vector3 center;

	[SerializeField]
	protected Vector3 size;

	protected bool isColliding = false;

	public bool IsColliding {
		get {
			return isColliding;
		}
	}

	/// <summary>
	/// Gets the center.
	/// </summary>
	/// <value>The center.</value>
	public Vector3 Center {
		get {
			return center;
		}
	}

	/// <summary>
	/// Gets the size.
	/// </summary>
	/// <value>The size.</value>
	public Vector3 Size {
		get {
			return size;
		}
	}

	/// <summary>
	/// Determines whether this instance is colliding using AABB algorithm.
	/// </summary>
	/// <returns><c>true</c> if this instance is colliding; otherwise, <c>false</c>.</returns>
	public bool IsCollidingWith (CustomBoxCollider other)
	{
		var ourMinBound = GetMinBound ();
		var otherMinBound = other.GetMinBound ();

		var ourMaxBound = GetMaxBound ();
		var otherMaxBound = other.GetMaxBound ();

		return GreaterComponentCompare (otherMaxBound, ourMinBound) &&
		GreaterComponentCompare (ourMaxBound, otherMinBound);
	}

	/// <summary>
	/// Return true if all of a's component are greater than b.
	/// </summary>
	/// <returns><c>true</c>, if component compare was greatered, <c>false</c> otherwise.</returns>
	/// <param name="a">The alpha component.</param>
	/// <param name="b">The blue component.</param>
	internal bool GreaterComponentCompare (Vector3 a, Vector3 b)
	{
		return 	
		a.x > b.x &&
		a.y > b.y &&
		a.z > b.z;
	}

	/// <summary>
	/// Gets the world center.
	/// </summary>
	/// <value>The world center.</value>
	internal Vector3 WorldCenter {
		get {
			return transform.position + center;
		}
	}

	/// <summary>
	/// Gets the minimum bound.
	/// </summary>
	/// <returns>The minimum bound.</returns>
	internal Vector3 GetMinBound ()
	{
		return WorldCenter - GetHalfSize ();
	}

	/// <summary>
	/// Gets the max bound.
	/// </summary>
	/// <returns>The max bound.</returns>
	internal Vector3 GetMaxBound ()
	{
		return WorldCenter + GetHalfSize ();
	}

	/// <summary>
	/// Gets the half size of the collider.
	/// </summary>
	/// <returns>The half size.</returns>
	public Vector3 GetHalfSize ()
	{
		return size / 2f;
	}

	/// <summary>
	/// Awake this instance.
	/// </summary>
	protected virtual void Awake ()
	{

	}

	/// <summary>
	/// Raises the draw gizmos event.
	/// </summary>
	protected virtual void OnDrawGizmos ()
	{
		Gizmos.color = Color.black;

		if (isColliding) {
			Gizmos.color = Color.red;
		}

		Gizmos.DrawWireCube (WorldCenter, size);
	}
}
