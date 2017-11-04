using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
	public float RotationPerSecond = 1;

	[SerializeField]
	private bool isRotating;

	protected void Update ()
	{
		if (isRotating)
			RenderSettings.skybox.SetFloat ("_Rotation", Time.time * RotationPerSecond);
	}

	public void ToggleSkyboxRotation ()
	{
		isRotating = !isRotating;
	}
}