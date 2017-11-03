using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Obstacle system.
/// Spawns obstacles on the asigned plane
/// Author: LAB
/// Attached to: ObstacleSystem
/// </summary>
public class ObstacleSystem : SpawningSystem <Obstacle>
{
	#region implemented abstract members of SpawningSystem

	/// <summary>
	/// Spawns the preys.
	/// </summary>
	/// <param name="index">Index.</param>
	protected override void SpawnEntity (int i)
	{
		var spawnPos = plane.GetRandomPositionAbove (prefabCollider);

		var instance = Instantiate (prefab, spawnPos, Quaternion.identity, transform);

		RegisterVehicle (instance);
	}

	#endregion
	
}
