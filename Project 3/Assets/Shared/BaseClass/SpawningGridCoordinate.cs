using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grid coordinate.
/// Only has x and z position, for batching traversal
/// Author: LAB
/// </summary>
public class SpawningGridCoordinate <T> where T : Component // I'm... too lazy
{
	public int X { get; private set; }

	public int Z { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="SpawningGridCoordinate"/> struct.
	/// </summary>
	/// <param name="pos">Position.</param>
	/// <param name="gridWidth">Grid width.</param>
	public SpawningGridCoordinate (Vector3 pos, float gridWidth)
	{
		X = Mathf.CeilToInt (pos.x / gridWidth);

		Z = Mathf.CeilToInt (pos.z / gridWidth);
	}

	/// <summary>
	/// Returns a <see cref="System.String"/> that represents the current <see cref="SpawningGridCoordinate"/>.
	/// </summary>
	/// <returns>A <see cref="System.String"/> that represents the current <see cref="SpawningGridCoordinate"/>.</returns>
	public override string ToString ()
	{
		return string.Format ("{0}-{1}", X, Z);
	}

	/// <summary>
	/// Gets the grid key.
	/// </summary>
	/// <returns>The grid key.</returns>
	/// <param name="pos">Position.</param>
	/// <param name="gridWidth">Grid width.</param>
	public static string GetGridKey (T instance, float gridWidth)
	{
		// I'm just too lazy...
		var gridCoord = new SpawningGridCoordinate <T> (
			                instance.transform.position, gridWidth
		                );

		return gridCoord.ToString ();

	}
}