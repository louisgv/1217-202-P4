using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Terrain height utils.
/// Used to sampling height terrain in a more functional way
/// Author: LAB
/// </summary>
public static class TerrainHeightUtils
{
	/// <summary>
	/// Gets the prime terrain.
	/// </summary>
	/// <returns>The prime terrain.</returns>
	static public Terrain GetPrimeTerrain ()
	{
		return GameObject.FindGameObjectWithTag (Tag.PRIME_TERRAIN).GetComponent <Terrain> ();
	}

	// Sampling vector with terrain heightmap if there is a terrain
	static public Vector3 ApplySample (Terrain terrain, Vector3 pos)
	{
		if (terrain != null) {
			pos.y = terrain.SampleHeight (pos);
		} else {
			pos.y = 0;
		}
		return pos;
	}

	// Sampling with the active terrrain
	static public Vector3 ApplySample (Vector3 pos)
	{	
		return ApplySample (GetPrimeTerrain (), pos);
	}

	/// <summary>
	/// Adds the sample height to the current height.
	/// </summary>
	/// <returns>The on sample.</returns>
	/// <param name="terrain">Terrain.</param>
	/// <param name="pos">Position.</param>
	static public Vector3 AddOnSample (Terrain terrain, Vector3 pos, float initialHeight)
	{
		if (terrain != null) {
			pos.y = initialHeight + terrain.SampleHeight (pos);
		}
		return pos;
	}

	/// <summary>
	/// Adds the on sample.
	/// </summary>
	/// <returns>The on sample.</returns>
	/// <param name="pos">Position.</param>
	/// <param name="initialHeight">Initial height.</param>
	static public Vector3 AddOnSample (Vector3 pos, float initialHeight)
	{
		return AddOnSample (GetPrimeTerrain (), pos, initialHeight);
	}

	/// <summary>
	/// Adds the on sample.
	/// </summary>
	/// <returns>The on sample.</returns>
	/// <param name="pos">Position.</param>
	static public Vector3 AddOnSample (Vector3 pos)
	{
		return AddOnSample (GetPrimeTerrain (), pos, pos.y);
	}
}
