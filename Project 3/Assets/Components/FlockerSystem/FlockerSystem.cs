using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Flocker system.
/// Used to spawn flocker and update their direction as well as managing their 
/// flock behavior
/// Author: LAB
/// Attached to: FlockerSystem
/// </summary>
public class FlockerSystem : SpawningSystem <Flocker>
{
	#region implemented abstract members of SpawningSystem

	public override void SpawnEntity (Vector3 pos)
	{
		throw new System.NotImplementedException ();
	}

	#endregion
}
