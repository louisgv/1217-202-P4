using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializableAttribute]
public class SteeringParams
{
	[SerializeField, Range (0, 10f)]
	protected float forceScale = 2.0f;

	/// <summary>
	/// The max fleeing velocity.
	/// </summary>
	[SerializeField, Range (0, 10f)]
	protected float maxSpeed = 1;

	[SerializeField]
	private float thresholdSquared = 36.0f;

	/// <summary>
	/// Gets the max speed.
	/// </summary>
	/// <value>The max speed.</value>
	public float MaxSpeed {
		get {
			return maxSpeed;
		}
	}

	/// <summary>
	/// Gets the force scale.
	/// </summary>
	/// <value>The force scale.</value>
	public float ForceScale {
		get {
			return forceScale;
		}
	}

	/// <summary>
	/// Gets the threshold squared.
	/// </summary>
	/// <value>The threshold squared.</value>
	public float ThresholdSquared {
		get {
			return thresholdSquared;
		}
	}
}
	