using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Flocker.
/// A flocker's movement is heavily depended on the Flocker System it belongs to
/// </summary>
public class Flocker : SmartBoundedVehicle<Flocker, FlockerCollider, FlockerSystem>
{
	#region implemented abstract members of Vehicle

	protected override Vector3 GetTotalSteeringForce ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion

	protected override void Awake ()
	{
		base.Awake ();
	}

	protected override void Update ()
	{
		base.Update ();
		// Grab the direction from the system here
	}

}
