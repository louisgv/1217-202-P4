using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prey behavior.
/// Author: LAB
/// Attracted to: Human
/// </summary>
public class Human : Vehicle
{
	#region implemented abstract members of Vehicle

	protected override Vector3 GetSteeringForce ()
	{
		
		
		return Vector3.zero;
	}

	#endregion

}