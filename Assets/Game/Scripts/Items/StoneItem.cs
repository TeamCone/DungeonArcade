using System;
using Game.Player;
using UnityEngine;

public class StoneItem : MonoBehaviour, IThrowItem
{
	public void Throw()
	{
		Debug.Log("throwing stone");
	}

	public string Name()
	{
		return name;
	}
}
