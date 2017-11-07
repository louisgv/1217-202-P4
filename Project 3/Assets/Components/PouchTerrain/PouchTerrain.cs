using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pouch terrain, used to generate a terrain that looks like a pouch.
/// At the moment, using the function x^8.
/// Author: LAB
/// Attached to: Pouch Terrain
/// </summary>
public class PouchTerrain : MonoBehaviour
{

	float[,] heightmap;

	[SerializeField]
	int resolution = 100;

	public Vector3 worldSize = new Vector3 (100, 100, 100);

	Terrain terrain;

	TerrainData terrainData;

	/// <summary>
	/// Initialize the terrain and its height map
	/// </summary>
	private void Awake ()
	{
		terrain = GetComponent <Terrain> ();

		terrainData = terrain.terrainData;

		heightmap = new float[resolution, resolution];

		terrainData.heightmapResolution = resolution;

		terrainData.size = worldSize;

		for (int i = 0; i < resolution; i++) {
			for (int j = 0; j < resolution; j++) {
				heightmap [i, j] = CalculateHeight (i, j, resolution);
			}
		}

		terrainData.SetHeights (0, 0, heightmap);

		terrain.terrainData = terrainData;

	}

	/// <summary>
	/// Calculates the height of the pouch terrain.
	/// </summary>
	/// <returns>The height.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="res">Res.</param>
	private float CalculateHeight (int x, int y, int res)
	{
		float xCoord = ScaleRamp (x, res);

		float yCoord = ScaleRamp (y, res);

		float truncatedYCoord = 2f * yCoord - 1f;

		float truncatedXCoord = 2f * xCoord - 1f;

		return Mathf.Pow (truncatedYCoord, 8) +
		Mathf.Pow (truncatedXCoord, 8);
	}

	/// <summary>
	/// Scales the ramp.
	/// </summary>
	/// <returns>The ramp.</returns>
	/// <param name="i">The index.</param>
	/// <param name="res">Res.</param>
	private float ScaleRamp (int i, int res)
	{
		return (float)i / res;
	}
}
