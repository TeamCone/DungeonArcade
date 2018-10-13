using System;
using Game.Player;
using UnityEngine;

public class TreasureItem : MonoBehaviour, IThrowItem 
{
	public void Throw()
	{
		Debug.Log("cannot throw treasure");
	}

	public string Name()
	{
		return name;
	}
}
