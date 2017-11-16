using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

/// <summary>
/// Cube plane collider.
/// Mainly used for other to get a random position.
/// </summary>
public class CubePlaneCollider : CustomBoxCollider
{

	/// <summary>
	/// Samples the height.
	/// </summary>
	/// <returns>The height.</returns>
	/// <param name="pos">Position.</param>
	/// <param name="col">Col.</param>
	public Vector3 GetSampledPosition (Vector3 pos, CustomBoxCollider col)
	{
		return TerrainHeightUtils.AddOnSample (pos, col.Size.y / 2f);
	}

	/// <summary>
	/// Gets a random position along the XZ axis.
	/// </summary>
	/// <returns>The random position above.</returns>
	public Vector3 GetRandomPositionXZ ()
	{
		var randomX = Random.Range (-0.45f, 0.45f) * Size.x;

		var randomZ = Random.Range (-0.45f, 0.45f) * Size.z;

		return WorldCenter + new Vector3 (randomX, 0, randomZ);
	}

	/// <summary>
	/// Gets a random position above the plane collider.
	/// </summary>
	/// <returns>The random position above.</returns>
	/// <param name="col">Col.</param>
	public Vector3 GetRandomPositionAbove (CustomBoxCollider col)
	{
		return GetSampledPosition (GetRandomPositionXZ (), col);
	}


}
