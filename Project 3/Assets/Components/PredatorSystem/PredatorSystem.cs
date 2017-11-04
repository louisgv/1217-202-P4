using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Predator system.
/// Spawns predator and attach the pree-defined targetPreySystem
/// Author: LAB
/// Attached to: PredatorSystem
/// </summary>
public class PredatorSystem : SpawningSystem <Predator>
{
	[SerializeField]
	private PreySystem targetPreySystem;

	[SerializeField]
	private ObstacleSystem targetObstacleSystem;

	#region implemented abstract members of SpawningSystem

	/// <summary>
	/// Spawns the preys.
	/// </summary>
	/// <param name="index">Index.</param>
	protected override void SpawnEntity ()
	{
		var predatorPos = plane.GetRandomPositionAbove (prefabCollider);

		var predatorInstance = Instantiate (prefab, predatorPos, Quaternion.identity, transform);

		predatorInstance.ParentSystem = this;
		predatorInstance.TargetPreySystem = targetPreySystem;
		predatorInstance.TargetObstacleSystem = targetObstacleSystem;

		predatorInstance.BoundingPlane = plane;

		RegisterVehicle (predatorInstance);
	}

	#endregion
}
