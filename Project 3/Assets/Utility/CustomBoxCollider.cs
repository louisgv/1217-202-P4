using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Custom collider using renderer's mesh bound.
/// It uttilizes AABB collision algorithm
/// </summary>
public class CustomBoxCollider : MonoBehaviour
{
	[SerializeField]
	private CustomBoxCollider target;

	[SerializeField]
	private Vector3 center;

	[SerializeField]
	private Vector3 size;

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
	public bool IsColliding (CustomBoxCollider other)
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
		return WorldCenter - size / 2f;
	}

	/// <summary>
	/// Gets the max bound.
	/// </summary>
	/// <returns>The max bound.</returns>
	internal Vector3 GetMaxBound ()
	{
		return WorldCenter + size / 2f;
	}

	/// <summary>
	/// Raises the draw gizmos event.
	/// </summary>
	private void OnDrawGizmos ()
	{
		Gizmos.color = Color.green;

		if (target != null && IsColliding (target)) {
			Gizmos.color = Color.red;
		}

		Gizmos.DrawWireCube (WorldCenter, size);

	}
}
