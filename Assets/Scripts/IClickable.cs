using UnityEngine;
using System;

public interface IClickable<T>
{
	void Click(Action<T> action);
}