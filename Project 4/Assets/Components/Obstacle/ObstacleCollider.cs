﻿using System.Collections;
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
	private GameObject watchingSystemParent;

	private CustomBoxCollider[] targetColliders;

	private bool isColliding;

	public bool debugColliding;

	protected override void Start ()
	{
		base.Start ();

		watchingSystemParent = GameObject.FindGameObjectWithTag (Tag.FLOCKER_SYSTEM);

		targetColliders = watchingSystemParent.GetComponentsInChildren <CustomBoxCollider> ();
	}

	protected override void Update ()
	{
		base.Update ();

		if (debugColliding) {
			isColliding = false;
			for (int i = 0; i < targetColliders.Length; i++) {
				var other = targetColliders [i];

				if (IsCollidingWith (other)) {
					isColliding = true;
					Debug.Log ("IT IS COLIDING!!!!");
				}
			}
		}
	}

	protected override void OnDrawGizmos ()
	{
		if (debugColliding && isColliding) {
			Gizmos.color = Color.red;
			
			Gizmos.DrawWireCube (WorldCenter, size);
		} else {
			base.OnDrawGizmos ();
		}
	}
}
