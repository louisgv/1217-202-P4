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
	/// Empty abstract as it is not needed
	/// </summary>
	/// <param name="index">Index.</param>
	protected override void SpawnEntity (int i)
	{
		// INTENTIONALLY LEFT BLANK	
	}

	protected override void SpawnEntities ()
	{
		var minBound = plane.GetMinBound ();

		int xCount = spawnCount / 2;

		int zCount = spawnCount - xCount;

		float xStep = plane.Size.x / xCount;
		float zStep = plane.Size.z / zCount;

		var initialPos = new Vector3 (xStep, 0, zStep) / 2 + prefabCollider.GetHalfSize ().y * Vector3.up;

		for (int x = 0; x < xCount; x++) {
			for (int z = 0; z < zCount; z++) {
				var spawnPos = minBound + new Vector3 (x * xStep, 0, z * zStep) + initialPos;
				
				var instance = Instantiate (prefab, spawnPos, Quaternion.identity, transform);
				
				RegisterVehicle (instance);
			}
		}
	}

	#endregion
	
}
