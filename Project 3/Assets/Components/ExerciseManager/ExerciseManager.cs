using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Exercise manager object, handle spawning human and zombies.
/// Author: LAB
/// Attached to: ExerciseManager
/// </summary>
public class ExerciseManager : SpawningSystem
{
	public TeleporterCollider teleporterPrefab;

	public int preyCount = 9;

	[SerializeField]
	private CubePlaneCollider plane;

	#region implemented abstract members of SpawningSystem

	/// <summary>
	/// Spawns the entities.
	/// </summary>
	protected override void SpawnEntities ()
	{
		var teleporterPos = plane.GetRandomPositionAbove (teleporterPrefab);
		var teleporterInstance = Instantiate (teleporterPrefab, teleporterPos, Quaternion.identity, transform);

		teleporterInstance.SpawningSystem = this;
		teleporterInstance.Plane = plane;

		var predatorPos = plane.GetRandomPositionAbove (predatorPrefab.GetComponent <CustomBoxCollider> ());
		var predatorInstance = Instantiate (predatorPrefab, predatorPos, Quaternion.identity, transform) as Zombie;

		predatorInstance.SpawningSystem = this;

		var preyPrefabCollider = preyPrefab.GetComponent <CustomBoxCollider> ();

		for (int i = 0; i < preyCount; i++) {
			var preyPos = plane.GetRandomPositionAbove (preyPrefabCollider);
			var preyInstance = Instantiate (preyPrefab, preyPos, Quaternion.identity, transform) as Human;

			preyInstance.FleeingTarget = predatorInstance.transform;
			preyInstance.SeekingTarget = teleporterInstance.transform;

			preyInstances.Add (preyInstance);
			preyColliderInstances.Add (preyInstance.GetComponent <PreyCollider> ());
		}
	}

	#endregion
	
}
