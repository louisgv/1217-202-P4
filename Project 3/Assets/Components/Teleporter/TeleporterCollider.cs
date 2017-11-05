using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Teleporter collider.
/// Check if the teleporter has collided with any of the prey in the assigned spawning system
/// If so, it will teleport to a random position on the provided terrain.
/// Author: LAB
/// Attached to: Teleporter
/// </summary>
public class TeleporterCollider : CustomBoxCollider
{
	private SpawningSystem<Vehicle> spawningSystem;

	/// <summary>
	/// Gets or sets the spawning system.
	/// </summary>
	/// <value>The spawning system.</value>
	public SpawningSystem<Vehicle> SpawningSystem {
		get {
			return spawningSystem;
		}
		set {
			spawningSystem = value;
		}
	}

	private CubePlaneCollider plane;

	/// <summary>
	/// Gets or sets the plane.
	/// </summary>
	/// <value>The plane.</value>
	public CubePlaneCollider Plane {
		get {
			return plane;
		}
		set {
			plane = value;
		}
	}

	/// <summary>
	/// Teleport this instance to a random position on the plane.
	/// </summary>
	private void Teleport ()
	{
		var teleportingPosition = plane.GetRandomPositionAbove (this);

		transform.position = teleportingPosition;
	}
		
}
