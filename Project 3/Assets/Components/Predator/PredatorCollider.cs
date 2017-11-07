using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Chaser collider.
/// Author: LAB
/// Attached to: Chaser objects
/// </summary>
public class PredatorCollider : CustomBoxCollider
{
	private CameraManager cameraManager;

	private Vector3 originalScale;

	private Vector3 hoverScale;

	private float hoverScaleFactor = 2f;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	protected override void Awake ()
	{
		base.Awake ();

		cameraManager = GameObject.FindGameObjectWithTag (Tag.CAMERA_MANAGER).GetComponent <CameraManager> ();

		originalScale = transform.localScale;

		hoverScale = transform.localScale * hoverScaleFactor;
	}

	/// <summary>
	/// Check on mouse down event taken from Unity's trigger for assigning camera focus
	/// </summary>
	private void OnMouseDown ()
	{
		cameraManager.SetSmoothCamera (transform);
	}

	/// <summary>
	/// Scale the transform up upon hover
	/// </summary>
	private void OnMouseOver ()
	{
		transform.localScale = hoverScale;
	}

	private void OnMouseExit ()
	{
		transform.localScale = originalScale;
	}

	
}
