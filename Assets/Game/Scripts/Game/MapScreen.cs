using System.Collections.Generic;
using System.Linq;
using Game.Input;
using Game.Player;
using Game.Scripts.Game;
using UnityEngine;
using UnityEngine.UI;

public class MapScreen : MonoBehaviour
{

    [SerializeField] private GameInputController _gameInputController;
	private List<IPlayer> _players = new List<IPlayer>();
	
	[SerializeField] private Image[] _pressStartImages;
	[SerializeField] private Transform[] _playerSpawns;
	[SerializeField] private GameObject[] _playerContainers;
	[SerializeField] private TimeController _timeController;

	private List<EnumPlayer> _enumPlayers;
	
    void Start () 
    {
	  
	    Init();
	    GetPlayers();
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

		var gameResults = GetGameResult();
		GameManager.Instance.SetGameResult(gameResults);
		GameManager.Instance.LoadResultScene();
	}

	private List<GameResult> GetGameResult()
	{
		var gameResults = new List<GameResult>();
		foreach (var player in _players)
		{
			Debug.LogFormat("Player {0}, Winner: {1}", player.GetPlayerId(), player.IsWinner());
			gameResults.Add(new GameResult
			{
				Player = (int)player.GetPlayerId(),
				IsWinner = player.IsWinner(),
				Deaths = 10,
				Kills = 10
			});
		}
		return gameResults;
	}


	private void SpawnPlayer(EnumPlayer enumPlayer)
    {
        var player = GameManager.Instance.SpawnPlayer(enumPlayer, _playerSpawns[(int)enumPlayer]);
	    _players.Add(player);
        _gameInputController.SetPlayer(enumPlayer, player);
    }
	
	private void GetPlayers()
	{
		var players = GameManager.Instance.GetPlayers();

		if (players.list.Any(player => player == EnumPlayer.Player1))
		{
			if (_enumPlayers.Any(player => player == EnumPlayer.Player1) == false)
			{
				_enumPlayers.Add(EnumPlayer.Player1);
				ShowPlayer(EnumPlayer.Player1);
			}
		}
		
		if (players.list.Any(player => player == EnumPlayer.Player2))
		{
			if (_enumPlayers.Any(player => player == EnumPlayer.Player2) == false)
			{
				_enumPlayers.Add(EnumPlayer.Player2);
				ShowPlayer(EnumPlayer.Player2);
			}
		}
		
		if (players.list.Any(player => player == EnumPlayer.Player3))
		{
			if (_enumPlayers.Any(player => player == EnumPlayer.Player3) == false)
			{
				_enumPlayers.Add(EnumPlayer.Player3);
				ShowPlayer(EnumPlayer.Player3);
			}
		}
		
		if (players.list.Any(player => player == EnumPlayer.Player4))
		{
			if (_enumPlayers.Any(player => player == EnumPlayer.Player4) == false)
			{
				_enumPlayers.Add(EnumPlayer.Player4);
				ShowPlayer(EnumPlayer.Player4);
			}
		}
	}
	
	private void Init()
	{
		_enumPlayers = new List<EnumPlayer>();
		_gameInputController.SetPlayerJoinGame(OnPlayerJoinGame);
		for (var i = 0; i < 4; i++)
		{
			_pressStartImages[i].gameObject.SetActive(true);
			_playerContainers[i].gameObject.SetActive(false);
		}
	}
	
	private void OnPlayerJoinGame(EnumPlayer enumPlayer)
	{
		GameManager.Instance.AddPlayer(enumPlayer);
		GameManager.Instance.LoadMapScene(1);
	}
	
	private void ShowPlayer(EnumPlayer enumPlayer)
	{
		_pressStartImages[(int) enumPlayer].gameObject.SetActive(false);
		_playerContainers[(int) enumPlayer].gameObject.SetActive(true);

		SpawnPlayer(enumPlayer);

	}
	
	
	
}