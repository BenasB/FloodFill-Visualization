using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Node : MonoBehaviour, IClickable<Node>
{
	public Vector2Int GraphPosition { get; set; }
	public SpriteRenderer spriteRenderer { get; private set; }

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Click(Action<Node> action)
	{
		if (FloodFillManager.State != FloodFillManager.StateTypes.Placement) return;

		if (action != null)
			action(this);
	}
}
