using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameResult
{
	[SerializeField]
	public int Player;
	[SerializeField]
	public bool IsWinner;
	[SerializeField]
	public int Kills;
	[SerializeField]
	public int Deaths;
}

[Serializable]
public class GameResults
{
	[SerializeField]
	public List<GameResult> Results;
	public GameResults(List<GameResult> results)
	{
		Results = results;
	}
}