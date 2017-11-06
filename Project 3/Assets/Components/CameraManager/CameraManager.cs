using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: LAB <lab@mail.rit.edu>
/// Description: Manage and cycle through a list of camera
/// Restriction: N/A
/// </summary>

public class CameraManager : MonoBehaviour
{

	public GameObject[] cameras;

	private int currentCameraIndex;

	/// <summary>
	/// Initialize current index, disable all camera except the first
	/// </summary>
	private void Start ()
	{
		currentCameraIndex = 0;

		foreach (var camera in cameras) {
			camera.SetActive (false);
		}

		if (cameras.Length > 0) {
			cameras [0].SetActive (true);
		}
	}

	/// <summary>
	/// Cycle through the available camera in the camera array
	/// </summary>
	/// <returns>The next camera index</returns>
	private int GetNextCameraIndexCyclic ()
	{
		// Cycle index using mod op
		return (currentCameraIndex + 1) % cameras.Length;
	}

	/// <summary>
	/// Disable camera at specified index
	/// </summary>
	/// <param name="index">camera index</param>
	private void DisableCamera (int index)
	{
		cameras [index].SetActive (false);
	}

	/// <summary>
	/// Find the next camera and enable it
	/// </summary>
	/// <returns>The index of the camera enabled</returns>
	private int EnableNextCamera ()
	{
		int nextCameraIndex = GetNextCameraIndexCyclic ();

		cameras [nextCameraIndex].SetActive (true);

		return nextCameraIndex;
	}

	/// <summary>
	/// Check for C key and cycle through the list of available camera
	/// </summary>
	private void Update ()
	{
		bool cKeyDown = Input.GetKeyDown (KeyCode.C);
		if (cKeyDown) {
			DisableCamera (currentCameraIndex);
			currentCameraIndex = EnableNextCamera (); // Mutate currentCameraIndex
		}
	}

}
