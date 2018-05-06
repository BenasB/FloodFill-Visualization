using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Placement))]
public class FloodFillManager : MonoBehaviour 
{
	[SerializeField] Transform nodePrefab;
	[SerializeField] InputField lengthInputField;
	[SerializeField] InputField widthInputField;

	[SerializeField] Transform generationGroup;
	[SerializeField] Transform placementGroup;
	[SerializeField] Transform customizationGroup;
	[SerializeField] Transform runtimeGroup;

	public int Length { get; private set; }
	public int Width { get; private set; }

	public enum StateTypes { Generation, Placement, Customization, Runtime }
	public static StateTypes State { get; private set; }

	Node[,] graph;
	Placement placement;
	float delay;

	void Start() 
	{
		placement = GetComponent<Placement>();
	}

	// Called by a button
	public void GenerateGraph ()
	{
		// Get dimensions from input fields
		if (string.IsNullOrEmpty(lengthInputField.text) || string.IsNullOrEmpty(widthInputField.text)) return;
		Length = Int32.Parse(lengthInputField.text);
		Width = Int32.Parse(widthInputField.text);

		graph = new Node[Length, Width];

		for (int x = 0; x < Length; x++)
		{
			for (int y = 0; y < Width; y++)
			{
				Vector2 position = new Vector2(x, y);
				Node newNode = Instantiate(nodePrefab, position, Quaternion.identity, transform).GetComponent<Node>();
				newNode.GraphPosition = new Vector2Int(x, y);
				graph[x, y] = newNode;
			}
		}

		CenterCamera();
		State = StateTypes.Placement;
		generationGroup.gameObject.SetActive(false);
		placementGroup.gameObject.SetActive(true);
	}

	void CenterCamera()
	{
		Camera mainCamera = Camera.main;

		Vector3 center = new Vector3(Length / 2 - (Length % 2 == 0 ? 0.5f : 0), Width / 2 - (Width % 2 == 0 ? 0.5f : 0));
		center.z = mainCamera.transform.position.z;
		mainCamera.transform.position = center;

		mainCamera.orthographicSize = ((Length > Width) ? (Length/2) * (1 / mainCamera.aspect) : Width / 2) + 1;
	}

	// Called by an input field
	public void LimitValue(InputField inputField)
	{
		if (string.IsNullOrEmpty(inputField.text)) return;

		int value = Int32.Parse(inputField.text);
		inputField.text = Mathf.Max(2, value).ToString();
	}

	public void SwitchToCustomization()
	{
		if (placement.StartNode == null) return;

		State = StateTypes.Customization;
		placementGroup.gameObject.SetActive(false);
		customizationGroup.gameObject.SetActive(true);
	}

	// Called by a button
	public void StartFloodFillQueue()
	{
		customizationGroup.gameObject.SetActive(false);
		runtimeGroup.gameObject.SetActive(true);
		State = StateTypes.Runtime;

		StartCoroutine(FloodFill.FloodFillQueue(graph, placement.StartNode, Color.white, Color.red, delay));
	}

	// Called by a button
	public void StartFloodFillStack()
	{
		customizationGroup.gameObject.SetActive(false);
		runtimeGroup.gameObject.SetActive(true);
		State = StateTypes.Runtime;

		StartCoroutine(FloodFill.FloodFillStack(graph, placement.StartNode, Color.white, Color.red, delay));
	}

	// Called by a button
	public void SetDelay(Slider slider)
	{
		delay = slider.value;
	}

	// Called by a button
	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
