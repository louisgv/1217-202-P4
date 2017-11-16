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
	private Vector3 originalScale;

	private Vector3 hoverScale;

	private float hoverScaleFactor = 4f;

	private Prey preyInstance;

	private ScoreTracker scoreTracker;

	protected override void Awake ()
	{
		base.Awake ();

		scoreTracker = GameObject.FindGameObjectWithTag (Tag.SCORE_TRACKER).GetComponent <ScoreTracker> ();

		originalScale = transform.localScale;

		hoverScale = transform.localScale * hoverScaleFactor;

		preyInstance = GetComponent <Prey> ();
	}

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

	/// <summary>
	/// Check on mouse down event taken from Unity's trigger for assigning camera focus
	/// </summary>
	private void OnMouseDown ()
	{
		// Remove itself first so we can query nearby
		preyInstance.ParentSystem.RemoveVehicle (preyInstance);

		var preyList = preyInstance.ParentSystem.FindCloseProximityInstances (preyInstance, 1);

		var predatorList = preyInstance.TargetPredatorSystem.FindCloseProximityInstances (preyInstance, 1);

		int destroyCount = preyList.Count + predatorList.Count + 1;

		scoreTracker.SetScore (destroyCount);

		foreach (var prey in preyList) {
			preyInstance.ParentSystem.RemoveVehicle (prey);

			prey.Explode ();

			Destroy (prey.gameObject);
		}

		foreach (var predator in predatorList) {
			preyInstance.TargetPredatorSystem.RemoveVehicle (predator);

			predator.Explode ();

			Destroy (predator.gameObject);
		}

		preyInstance.Explode ();

		Destroy (gameObject);
	}

	/// <summary>
	/// Scale the transform up upon hover
	/// </summary>
	private void OnMouseOver ()
	{
		transform.localScale = hoverScale;
	}

	/// <summary>
	/// Scale the transform back 
	/// </summary>
	private void OnMouseExit ()
	{
		transform.localScale = originalScale;
	}

}
