using System;
using UnityEngine;
using UnityEngine.UI;

public class Placement : MonoBehaviour 
{
	public Node StartNode { get; private set; }

	Action<Node> clickHandler;

	void Start() 
	{
		clickHandler = PlaceObstacle;
	}

	void Update () 
	{
		if (Input.GetButton("Fire1"))
		{
			IClickable<Node> clickableNode = GetClickableNodeAtMousePosition();
			
			if (clickableNode == null) return;

			clickableNode.Click(clickHandler);
		}

		if (Input.GetButton("Fire2"))
		{
			IClickable<Node> clickableNode = GetClickableNodeAtMousePosition();
			
			if (clickableNode == null) return;

			clickableNode.Click(ResetPlacement);
		}
	}
	
	IClickable<Node> GetClickableNodeAtMousePosition()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

		if (!hit) return null;

		return hit.transform.GetComponent<IClickable<Node>>();
	}

	// Called by a dropdown
	public void PlacementSelection(Dropdown dropdown)
	{
		switch (dropdown.value)
		{
			case 0:
				clickHandler = PlaceObstacle;
				break;
			case 1:
				clickHandler = PlaceStartNode;
				break;
		}
	}

	void PlaceObstacle(Node node)
	{
		if (node == StartNode)
			return;

		node.spriteRenderer.color = Color.black;
	}

	void PlaceStartNode(Node node)
	{
		if (StartNode != null)
			ResetPlacement(StartNode);

		StartNode = node;
		node.spriteRenderer.color = Color.green;
	}

	void ResetPlacement(Node node)
	{
		if (node == StartNode)
			StartNode = null;

		node.spriteRenderer.color = Color.white;
	}
}
