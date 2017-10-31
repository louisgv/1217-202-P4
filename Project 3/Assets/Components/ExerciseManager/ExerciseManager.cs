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

	protected override void SpawnEntities ()
	{
		var teleporterPos = plane.GetRandomPositionAbove ();
		var teleporterInstance = Instantiate (teleporterPrefab, teleporterPos, Quaternion.identity, transform);

		teleporterInstance.SpawningSystem = this;
		teleporterInstance.Plane = plane;

		var predatorPos = plane.GetRandomPositionAbove ();
		var predatorInstance = Instantiate (predatorPrefab, predatorPos, Quaternion.identity, transform);

		for (int i = 0; i < preyCount; i++) {
			var preyPos = plane.GetRandomPositionAbove ();
			var preyInstance = Instantiate (preyPrefab, preyPos, Quaternion.identity, transform);

			preyInstance.FleeingTarget = predatorInstance.transform;
			preyInstance.SeekingTarget = teleporterInstance.transform;

			preyColliderInstances.Add (preyInstance.GetComponent <PreyCollider> ());
		}
	}

	#endregion
	
}
