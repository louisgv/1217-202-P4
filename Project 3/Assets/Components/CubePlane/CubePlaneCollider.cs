using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cube plane collider.
/// Mainly used for other to get a random position.
/// </summary>
public class CubePlaneCollider : CustomBoxCollider
{
	/// <summary>
	/// Gets a random position above the collider.
	/// </summary>
	/// <returns>The random position above.</returns>
	public Vector3 GetRandomPositionAbove ()
	{
		var randomX = Random.Range (-0.45f, 0.45f) * Size.x;

		var randomZ = Random.Range (-0.45f, 0.45f) * Size.z;

		var y = Size.y / 2;

		return WorldCenter + new Vector3 (randomX, y, randomZ);
	}

	/// <summary>
	/// Gets a random position above the plane collider.
	/// </summary>
	/// <returns>The random position above.</returns>
	/// <param name="col">Col.</param>
	public Vector3 GetRandomPositionAbove (CustomBoxCollider col)
	{
		return GetRandomPositionAbove () + col.GetHalfSize ().y * Vector3.up;
	}

	/// <summary>
	/// Gets a random position with an offset from an avoiding position.
	/// </summary>
	/// <returns>The random position from.</returns>
	/// <param name="avoidPos">Avoid position.</param>
	/// <param name="safeDistance">Safe distance.</param>
	public Vector3 GetRandomPositionAboveFrom (Vector3 avoidPos, float offset)
	{
		var randomPos = GetRandomPositionAbove ();

		return (Vector3.Distance (randomPos, avoidPos) > offset) 
			? randomPos
			: GetRandomPositionAboveFrom (avoidPos, offset);
	}
}
