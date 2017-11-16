using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: LAB <lab@mail.rit.edu>
/// Description: Manage and cycle through a list of camera
/// Restriction: N/A
/// </summary>

public class CameraManager : MonoBehaviour
{
	public GameObject OverallCamera;

	public SmoothFollow SmoothCamera;

	public Transform FollowingRef { get; set; }

	/// <summary>
	/// Initialize current index, disable all camera except the first
	/// </summary>
	private void Start ()
	{
		SetOverallCamera ();
	}

	/// <summary>
	/// Enable only the overall camera
	/// </summary>
	private void SetOverallCamera ()
	{
		foreach (Transform child in transform) {
			child.gameObject.SetActive (false);
		}

		FollowingRef = null;

		OverallCamera.gameObject.SetActive (true);
	}

	/// <summary>
	/// 
	/// </summary>
	public void SetSmoothCamera (Transform target)
	{
		if (FollowingRef == target) {
			SetOverallCamera ();
		}
		FollowingRef = target;

		SmoothCamera.target = target;

		foreach (Transform child in transform) {
			child.gameObject.SetActive (false);
		}

		SmoothCamera.gameObject.SetActive (true);
	}

	/// <summary>
	/// Check for C key and cycle through the list of available camera
	/// </summary>
	private void Update ()
	{
		bool cKeyDown = Input.GetKeyDown (KeyCode.C);
		if (cKeyDown) {
			SetOverallCamera ();
		}

		if (!FollowingRef || !FollowingRef.gameObject) {
			SetOverallCamera ();
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			Vehicle.debugLine = !Vehicle.debugLine;
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}

}
