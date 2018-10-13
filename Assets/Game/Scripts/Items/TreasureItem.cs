using Game.Player;
using UnityEngine;

public class TreasureItem : MonoBehaviour, IThrowItem 
{
	public void Throw(bool isFacingRight)
	{
		Debug.Log("Cannot throw treasure");
	}


	public string Name()
	{
		return name;
	}
}
