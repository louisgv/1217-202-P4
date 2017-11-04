using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grid coordinate.
/// Only has x and z position, for batching traversal
/// Author: LAB
/// </summary>
public struct SpawningGridCoordinate
{
	public int x;
	public int z;

	/// <summary>
	/// Initializes a new instance of the <see cref="SpawningGridCoordinate"/> struct.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public SpawningGridCoordinate (int x, int z)
	{
		this.x = x;
		this.z = z;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="SpawningGridCoordinate"/> struct.
	/// </summary>
	/// <param name="pos">Position.</param>
	/// <param name="gridWidth">Grid width.</param>
	public SpawningGridCoordinate (Vector3 pos, float gridWidth)
	{
		return 
	}
}