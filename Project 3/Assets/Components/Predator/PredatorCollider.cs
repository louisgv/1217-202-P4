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

	/// <summary>
	/// Awake this instance.
	/// </summary>
	protected override void Awake ()
	{
		base.Awake ();

		cameraManager = GameObject.FindGameObjectWithTag (Tag.CAMERA_MANAGER).GetComponent <CameraManager> ();
	}

	/// <summary>
	/// Check on mouse down event taken from Unity's trigger for assigning camera focus
	/// </summary>
	private void OnMouseDown ()
	{
		cameraManager.SetSmoothCamera (transform);
	}
	
}
