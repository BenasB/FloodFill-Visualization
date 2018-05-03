using System;
using UnityEngine;
using UnityEngine.UI;

public class FloodFillManager : MonoBehaviour 
{
	[SerializeField] Transform nodePrefab;
	[SerializeField] InputField lengthInputField;
	[SerializeField] InputField widthInputField;

	public int Length { get; private set; }
	public int Width { get; private set; }

	public enum StateTypes { Generation, Placement, Runtime }
	public static StateTypes State { get; private set; }

	public void GenerateGraph ()
	{
		// Get dimensions from input fields
		if (string.IsNullOrEmpty(lengthInputField.text) || string.IsNullOrEmpty(widthInputField.text)) return;
		Length = Int32.Parse(lengthInputField.text);
		Width = Int32.Parse(widthInputField.text);

		for (int x = 0; x < Length; x++)
		{
			for (int y = 0; y < Width; y++)
			{
				Vector2 position = new Vector2(x, y);
				Instantiate(nodePrefab, position, Quaternion.identity, transform);
			}
		}

		CenterCamera();
		State = StateTypes.Placement;
	}

	void CenterCamera()
	{
		Camera mainCamera = Camera.main;

		Vector3 center = new Vector3(Length / 2 - (Length % 2 == 0 ? 0.5f : 0), Width / 2 - (Width % 2 == 0 ? 0.5f : 0));
		center.z = mainCamera.transform.position.z;
		mainCamera.transform.position = center;

		mainCamera.orthographicSize = ((Length > Width) ? Length / 2 : Width / 2) + 1;
	}
}
