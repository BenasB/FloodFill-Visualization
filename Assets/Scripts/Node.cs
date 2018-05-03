using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Node : MonoBehaviour, IClickable
{
	SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Click(Color color)
	{
		if (FloodFillManager.State != FloodFillManager.StateTypes.Placement) return;

		spriteRenderer.color = color;   
	}
}
