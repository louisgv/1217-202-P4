using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prey collider.
/// Author: LAB
/// Attached to Human
/// </summary>
public class PreyCollider : CustomBoxCollider
{
	/// <summary>
	/// Checks the collision with the list of closeby predators.
	/// </summary>
	/// <param name="predators">Predators.</param>
	public bool CheckPredatorCollision (List<Transform> targets)
	{
		bool isColliding = false;
		
		for (int i = 0; i < targets.Count; i++) {
			var targetCollider = targets [i].GetComponent <PredatorCollider> ();

			if (isColliding = IsCollidingWith (targetCollider)) {
				break;
			}
		}

		return isColliding;
	}
}
