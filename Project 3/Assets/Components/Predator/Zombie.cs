using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Chaser vehicle derived from vehicle.
/// Author: LAB
/// Attached to: Zombie
/// </summary>
public class Zombie : Vehicle
{
	#region implemented abstract members of Vehicle

	protected override Vector3 GetSteeringForce ()
	{
		
		return Vector3.zero;
	}

	#endregion
	
}