using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prey system.
/// Spawns prey and attach the pre-defined targetPredatorSystem
/// Author: LAB
/// Attached to: PreySystem
/// </summary>
public class PreySystem : SpawningSystem <Prey>
{
	[SerializeField]
	private PredatorSystem targetPredatorSystem;

	[SerializeField]
	private ObstacleSystem targetObstacleSystem;

	#region implemented abstract members of SpawningSystem

	/// <summary>
	/// Spawns an entity at the specified position.
	/// </summary>
	/// <param name="pos">Position.</param>
	public override void SpawnEntity (Vector3 pos)
	{
		var preyInstance = Instantiate (prefab, pos, Quaternion.identity, transform);

		preyInstance.ParentSystem = this;
		preyInstance.TargetPredatorSystem = targetPredatorSystem;
		preyInstance.TargetObstacleSystem = targetObstacleSystem;

		preyInstance.BoundingPlane = plane;

		RegisterVehicle (preyInstance);
	}

	#endregion
}
