using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Flocker system.
/// Used to spawn flocker and update their direction as well as managing their 
/// flock behavior
/// Author: LAB
/// Attached to: FlockerSystem
/// </summary>
public class FlockerSystem : SpawningSystem <Flocker>
{
	[SerializeField]
	private ObstacleSystem targetObstacleSystem;

	#region implemented abstract members of SpawningSystem

	public override void SpawnEntity (Vector3 pos)
	{
		var flockerInstance = Instantiate (prefab, pos, Quaternion.identity, transform);

		flockerInstance.ParentSystem = this;

		flockerInstance.TargetObstacleSystem = targetObstacleSystem;

		flockerInstance.BoundingPlane = plane;

		RegisterVehicle (flockerInstance);
	}

	#endregion
}
