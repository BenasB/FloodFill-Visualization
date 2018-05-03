using UnityEngine;

public class Placement : MonoBehaviour 
{
	void Update () 
	{
		if (Input.GetButton("Fire1"))
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

			if (!hit) return;

			IClickable clickable = hit.transform.GetComponent<IClickable>();
			
			if (clickable == null) return;

			clickable.Click(Color.black);
		}
	}
}
