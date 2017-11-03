using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Predator system.
/// Spawns predator and attach the pree-defined targetPreySystem
/// Attached to: PredatorSystem
/// </summary>
public class PredatorSystem : SpawningSystem <Predator>
{
	[SerializeField]
	private PreySystem targetPreySystem;

	#region implemented abstract members of SpawningSystem

	/// <summary>
	/// Spawns the preys.
	/// </summary>
	/// <param name="index">Index.</param>
	protected override void SpawnEntity (int i)
	{
		var predatorPos = plane.GetRandomPositionAbove (prefabCollider);

		var predatorInstance = Instantiate (prefab, predatorPos, Quaternion.identity, transform);

		predatorInstance.TargetPreySystem = targetPreySystem;

		RegisterVehicle (predatorInstance);
	}

	#endregion
}
