using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Score tracker.
/// Author: LAB
/// Attached to: Score object
/// </summary>
public class ScoreTracker : MonoBehaviour
{
	private Text scoreText;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	private void Awake ()
	{
		scoreText = GetComponent <Text> ();
	}

	/// <summary>
	/// Sets the score.
	/// </summary>
	/// <param name="score">Score.</param>
	public void SetScore (int score)
	{
		if (int.Parse (scoreText.text) > score)
			return;

		scoreText.text = score.ToString ();
	}
}
