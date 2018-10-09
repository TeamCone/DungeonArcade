using System;
using Game.Player;
using UnityEngine;

[Serializable]
public class GameResult : MonoBehaviour
{
	public EnumPlayer Winner;
	public int Kills;
	public int Deaths;
}
