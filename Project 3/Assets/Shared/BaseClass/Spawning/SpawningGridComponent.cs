using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grid component.
/// A base MonoBehavior component that 
/// contains a definition for a SpawningGridCoordinate
/// Author: LAB
/// </summary>
public abstract class SpawningGridComponent : MonoBehaviour
{
	/// <summary>
	/// Gets the grid coordinate.
	/// </summary>
	/// <value>The grid coordinate.</value>
	public SpawningGridCoordinate GridCoordinate { get; set; }

	/// <summary>
	/// Return an updated grid if the new grid is different from
	/// current grid position, else it return null
	/// as a checking mechanism
	/// </summary>
	/// <returns>Updated grid position, else null.</returns>
	public SpawningGridCoordinate UpdatedGrid ()
	{
		var updatedGrid = new SpawningGridCoordinate (transform, GridCoordinate.GridWidth);

		return  updatedGrid != GridCoordinate 
				? updatedGrid 
				: null;
	}
}
