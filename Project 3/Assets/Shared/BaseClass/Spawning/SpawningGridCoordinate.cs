using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grid coordinate.
/// Only has x and z position, for batching traversal
/// Author: LAB
/// </summary>
public class SpawningGridCoordinate
{
	/// <summary>
	/// Gets the x.
	/// </summary>
	/// <value>The x.</value>
	public int X { get; private set; }

	/// <summary>
	/// Gets the z.
	/// </summary>
	/// <value>The z.</value>
	public int Z { get; private set; }

	/// <summary>
	/// Gets the width of the grid.
	/// </summary>
	/// <value>The width of the grid.</value>
	public float GridWidth { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="SpawningGridCoordinate"/> class.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	/// <param name="gridWith">Grid with.</param>
	public SpawningGridCoordinate (int x, int z, float gridWith)
	{
		X = x;
		Z = z;
		GridWidth = gridWith;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="SpawningGridCoordinate"/> struct.
	/// </summary>
	/// <param name="pos">Position.</param>
	/// <param name="gridWidth">Grid width.</param>
	public SpawningGridCoordinate (Vector3 pos, float gridWidth)
		: this (
			Mathf.CeilToInt (pos.x / gridWidth),
			Mathf.CeilToInt (pos.z / gridWidth),
			gridWidth
		)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="SpawningGridCoordinate"/> class.
	/// </summary>
	/// <param name="t">Transform</param>
	/// <param name="gridWidth">Grid width.</param>
	public SpawningGridCoordinate (Transform t, float gridWidth)
		: this (t.position, gridWidth)
	{
	}


	/// <summary>
	/// Gets the adjacent grids.
	/// </summary>
	/// <returns>The adjacent grids.</returns>
	public SpawningGridCoordinate[] GetAdjacentGrids ()
	{
		return new SpawningGridCoordinate[] {
			new SpawningGridCoordinate (X, Z + 1, GridWidth),
			new SpawningGridCoordinate (X, Z - 1, GridWidth),
			new SpawningGridCoordinate (X + 1, Z, GridWidth),
			new SpawningGridCoordinate (X - 1, Z, GridWidth),
			new SpawningGridCoordinate (X + 1, Z + 1, GridWidth),
			new SpawningGridCoordinate (X + 1, Z - 1, GridWidth),
			new SpawningGridCoordinate (X - 1, Z + 1, GridWidth),
			new SpawningGridCoordinate (X - 1, Z - 1, GridWidth)
		};
	}

	/// <summary>
	/// Gets the grid key.
	/// </summary>
	/// <returns>The grid key.</returns>
	/// <param name="instanceTransform">Instance transform.</param>
	/// <param name="gridWidth">Grid width.</param>
	public static string GetGridKey (Transform instanceTransform, float gridWidth)
	{
		return GetGridKey (instanceTransform.position, gridWidth);
	}

	/// <summary>
	/// Gets the grid key.
	/// </summary>
	/// <returns>The grid key.</returns>
	/// <param name="pos">Position.</param>
	/// <param name="gridWidth">Grid width.</param>
	public static string GetGridKey (Vector3 pos, float gridWidth)
	{
		var gridCoord = new SpawningGridCoordinate (pos, gridWidth);

		return gridCoord.ToString ();
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
	/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="SpawningGridCoordinate"/>.
	/// </summary>
	/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="SpawningGridCoordinate"/>.</param>
	/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
	/// <see cref="SpawningGridCoordinate"/>; otherwise, <c>false</c>.</returns>
	public override bool Equals (object obj)
	{
		var otherGrid = obj as SpawningGridCoordinate;

		if (otherGrid == null) {
			// If it is null then it is not equal to this instance.
			return false;
		}

		return X == otherGrid.X && Z == otherGrid.Z;
	}

	/// <summary>
	/// Serves as a hash function for a <see cref="SpawningGridCoordinate"/> object.
	/// </summary>
	/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
	public override int GetHashCode ()
	{
		// Since it's a mono behavior anyway, this shoudl suffice
		return new Vector3 (X, 0, Z).GetHashCode ();
	}

}