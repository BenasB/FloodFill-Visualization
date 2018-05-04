using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class FloodFill
{
    public static IEnumerator FloodFillQueue(Node[,]graph, Node startNode, Color targetColor, Color replacementColor, float delay)
	{
		Queue<Vector2Int> queue = new Queue<Vector2Int>();
		startNode.spriteRenderer.color = targetColor;
		queue.Enqueue(startNode.GraphPosition);

		while (queue.Count != 0)
		{
			Vector2Int currentPosition = queue.Dequeue();

			if (!Fits(graph, currentPosition)) continue;

			Node currentNode = graph[currentPosition.x, currentPosition.y];

			if (currentNode.spriteRenderer.color != targetColor) continue;

			currentNode.spriteRenderer.color = replacementColor;
			
			queue.Enqueue(currentPosition + Vector2Int.right);
			queue.Enqueue(currentPosition + Vector2Int.left);
			queue.Enqueue(currentPosition + Vector2Int.up);
			queue.Enqueue(currentPosition + Vector2Int.down);

			yield return new WaitForSecondsRealtime(delay);
		}
	}

    public static IEnumerator FloodFillStack(Node[,]graph, Node startNode, Color targetColor, Color replacementColor, float delay)
	{
		Stack<Vector2Int> stack = new Stack<Vector2Int>();
		startNode.spriteRenderer.color = targetColor;
		stack.Push(startNode.GraphPosition);

		while (stack.Count != 0)
		{
			Vector2Int currentPosition = stack.Pop();

			if (!Fits(graph, currentPosition)) continue;

			Node currentNode = graph[currentPosition.x, currentPosition.y];

			if (currentNode.spriteRenderer.color != targetColor) continue;

			currentNode.spriteRenderer.color = replacementColor;
			
			stack.Push(currentPosition + Vector2Int.right);
			stack.Push(currentPosition + Vector2Int.left);
			stack.Push(currentPosition + Vector2Int.up);
			stack.Push(currentPosition + Vector2Int.down);

			yield return new WaitForSecondsRealtime(delay);
		}
	}

    // Check whether the given position is within the graph
	static bool Fits(Node[,]graph, Vector2Int position)
	{
		return (position.x >= 0 && position.x < graph.GetLength(0)) && (position.y >= 0 && position.y < graph.GetLength(1));
	}
}