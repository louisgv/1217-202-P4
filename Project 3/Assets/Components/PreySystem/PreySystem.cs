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
	/// Spawns the preys.
	/// </summary>
	/// <param name="index">Index.</param>
	protected override void SpawnEntity ()
	{
		var preyPos = plane.GetRandomPositionAbove (prefabCollider);

		var preyInstance = Instantiate (prefab, preyPos, Quaternion.identity, transform);

		preyInstance.ParentSystem = this;
		preyInstance.TargetPredatorSystem = targetPredatorSystem;
		preyInstance.TargetObstacleSystem = targetObstacleSystem;

		preyInstance.BoundingPlane = plane;

		RegisterVehicle (preyInstance);
	}

	#endregion
}
