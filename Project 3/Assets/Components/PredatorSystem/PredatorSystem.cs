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
	/// Spawns an entity at the specified position.
	/// </summary>
	/// <param name="pos">Position.</param>
	public override void SpawnEntity (Vector3 pos)
	{
		var predatorInstance = Instantiate (prefab, pos, Quaternion.identity, transform);

		predatorInstance.ParentSystem = this;
		predatorInstance.TargetPreySystem = targetPreySystem;
		predatorInstance.TargetObstacleSystem = targetObstacleSystem;

		predatorInstance.BoundingPlane = plane;

		RegisterVehicle (predatorInstance);
	}

	#endregion
}
