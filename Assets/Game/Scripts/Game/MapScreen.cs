using System.Collections.Generic;
using System.Linq;
using Game.Input;
using Game.Player;
using Game.Scripts.Game;
using UnityEngine;

public class MapScreen : MonoBehaviour
{

    [SerializeField] private Transform[] _playerSpawns;
    [SerializeField] private GameInputController _gameInputController;
	[SerializeField] private TimeController _timeController;

	private List<IPlayer> _players = new List<IPlayer>();
	
	
    void Start () 
    {
        SpawnPlayer(EnumPlayer.Player1);
        SpawnPlayer(EnumPlayer.Player2);
        SpawnPlayer(EnumPlayer.Player3);
        SpawnPlayer(EnumPlayer.Player4);
	    _timeController.SetTimeUpCallback(OnTimeUp);
	    _timeController.StartTime();
    }

	//when time up, remove controls of each player and load result screen
	private void OnTimeUp()
	{
		_gameInputController.SetPlayer(EnumPlayer.Player1, null);
		_gameInputController.SetPlayer(EnumPlayer.Player2, null);
		_gameInputController.SetPlayer(EnumPlayer.Player3, null);
		_gameInputController.SetPlayer(EnumPlayer.Player4, null);

		var gameResult = GetGameResult();
		GameManager.Instance.SetGameResult(gameResult);
		GameManager.Instance.LoadResultScene();
	}

	private GameResult GetGameResult()
	{
		var gameResult = new GameResult();
		var winner = _players.SingleOrDefault(player => player.IsWinner());
		gameResult.Kills = 10;
		gameResult.Deaths = 10;
		if (winner == null)
		{
			gameResult.Winner = EnumPlayer.None;
		}
		else
		{
			gameResult.Winner = winner.GetPlayerId();
		}
		return gameResult;
	}


	private void SpawnPlayer(EnumPlayer enumPlayer)
    {
        var player = GameManager.Instance.SpawnPlayer(enumPlayer, _playerSpawns[(int)enumPlayer]);
	    _players.Add(player);
        _gameInputController.SetPlayer(enumPlayer, player);
    }
	
	
	
}