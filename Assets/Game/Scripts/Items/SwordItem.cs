using System;
using Game.Player;
using UnityEngine;

public class SwordItem : MonoBehaviour, IThrowItem
{
	public void Throw()
	{
		Debug.Log("throwing sword");
	}

	public string Name()
	{
		return name;
	}
}
