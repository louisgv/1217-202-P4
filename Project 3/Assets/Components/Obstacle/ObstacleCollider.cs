using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Obstacle collider.
/// Used to implement any missing behavior needed for other to avoid this obstacle
/// Author: LAB
/// Attached to: Obstacle
/// </summary>
public class ObstacleCollider : CustomBoxCollider
{
	private float extendSquared;

	private float extend;

	public float ExtendSquared {
		get {
			return extendSquared;
		}
	}

	protected override void Awake ()
	{
		var xzSize = size;
		xzSize.y = 0;
		extendSquared = Vector3.Dot (xzSize, xzSize);
		extend = xzSize.magnitude;
	}

	/// <summary>
	/// Raises the draw gizmos event.
	/// </summary>
	protected override void OnDrawGizmos ()
	{
		Gizmos.color = Color.cyan;

		Gizmos.DrawWireSphere (Vector3.Scale (new Vector3 (1, 0, 1), transform.position), extend);
	}
}
