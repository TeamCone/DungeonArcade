using System;
using Game.Player;
using UnityEngine;

public class StarItem : MonoBehaviour, IThrowItem
{
	public void Throw()
	{
		Debug.Log("throwing star");
	}

	public string Name()
	{
		return name;
	}
}
