using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prey system.
/// Spawns prey and attach the pre-defined targetPredatorSystem
/// Attached to: PreySystem
/// </summary>
public class PreySystem : SpawningSystem <Prey>
{
	[SerializeField]
	private PredatorSystem targetPredatorSystem;

	#region implemented abstract members of SpawningSystem

	/// <summary>
	/// Spawns the preys.
	/// </summary>
	/// <param name="index">Index.</param>
	protected override void SpawnEntity (int i)
	{
		var preyPos = plane.GetRandomPositionAbove (prefabCollider);

		var preyInstance = Instantiate (prefab, preyPos, Quaternion.identity, transform);

		preyInstance.TargetPredatorSystem = targetPredatorSystem;

		RegisterVehicle (preyInstance);
	}

	#endregion
}
