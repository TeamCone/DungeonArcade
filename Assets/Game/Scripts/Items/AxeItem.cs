using System;
using Game.Player;
using UnityEngine;

public class AxeItem : MonoBehaviour, IThrowItem
{
	public void Throw()
	{
		Debug.Log("throwing axe");
	}

	public string Name()
	{
		return name;
	}
}
