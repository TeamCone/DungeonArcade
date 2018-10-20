

using System;
using System.Collections.Generic;
using System.Linq;
using Game.Player;
using UnityEngine;

[Serializable]
public class OverAllResult
{	
	[SerializeField]
	private List<EnumPlayer> _mapWinners = new List<EnumPlayer>();

	public void AddWinner(EnumPlayer enumPlayer)
	{
		_mapWinners.Add(enumPlayer);
	}

	public EnumPlayer GetOverAllWinner()
	{
		var winner = _mapWinners.GroupBy( x => x)
		.OrderByDescending( g => g.Count())
		.Select( g => g.Key)
		.First();

		return winner;
	}
}

