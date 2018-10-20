using System;
using System.Collections.Generic;
using System.Linq;
using Game.Input;
using Game.Player;
using Game.Scripts.Game;
using UnityEngine;
using UnityEngine.UI;

public class MapScreen : MonoBehaviour
{

	public static MapScreen Instance;

	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
    [SerializeField] private GameInputController _gameInputController;
	private List<IPlayer> _players = new List<IPlayer>();
	
	[SerializeField] private Image[] _pressStartImages;
	[SerializeField] private Transform[] _playerSpawns;
	[SerializeField] private GameObject[] _playerContainers;
	[SerializeField] private TimeController _timeController;

	private List<EnumPlayer> _enumPlayers;
	
    void Start () 
    {
	    SoundManager.Instance.PlayBgm("BgmBattle");
	    Init();
	    GetPlayers();
	    _timeController.SetTimeUpCallback(OnTimeUp);
	    _timeController.StartTime();
	    SoundManager.Instance.PlayBgm("BgmBattle");
    }



	//when time up, remove controls of each player and load result screen
	private void OnTimeUp()
	{
		SoundManager.Instance.PlaySfx("SfxTimesupgong");
		_gameInputController.SetPlayer(EnumPlayer.Player1, null);
		_gameInputController.SetPlayer(EnumPlayer.Player2, null);
		_gameInputController.SetPlayer(EnumPlayer.Player3, null);
		_gameInputController.SetPlayer(EnumPlayer.Player4, null);

		GameManager.Instance.SubmitGameResult();
		GameManager.Instance.LoadResultScene("Map1Scene","Time's Up!");
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
		GameManager.Instance.InitGameResults();
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
		_timeController.StopTime();
		GameManager.Instance.AddPlayer(enumPlayer);
		GameManager.Instance.LoadMapScene(1, "Map1Scene", "New Challenger");
	}
	
	private void ShowPlayer(EnumPlayer enumPlayer)
	{
		_pressStartImages[(int) enumPlayer].gameObject.SetActive(false);
		_playerContainers[(int) enumPlayer].gameObject.SetActive(true);

		SpawnPlayer(enumPlayer);

	}

	public void ScoreKill(EnumPlayer enumPlayer)
	{
		GameManager.Instance.AddKills(enumPlayer);
		if (enumPlayer == EnumPlayer.None)
		{
			return;
		}
		
		Debug.Log("SCORE KILL " +enumPlayer);
		var currentScoreText = _playerContainers[(int) enumPlayer].transform.Find("KillCounter").transform.Find("Text").gameObject.GetComponent<Text>().text;
		var currentScore = Int32.Parse(currentScoreText);
		currentScore += 1;
		
		_playerContainers[(int) enumPlayer].transform.Find("KillCounter").transform.Find("Text").gameObject.GetComponent<Text>().text = (currentScore < 10 && (currentScore % 10) > 0)?"0"+currentScore.ToString():currentScore.ToString();
	}
	
	public void ScoreDeath(EnumPlayer enumPlayer)
	{
		GameManager.Instance.AddDeaths(enumPlayer);
		if (enumPlayer == EnumPlayer.None)
		{
			return;
		}
		
		var currentScoreText = _playerContainers[(int) enumPlayer].transform.Find("DeathCounter").transform.Find("Text").gameObject.GetComponent<Text>().text;
		var currentScore = Int32.Parse(currentScoreText);
		currentScore += 1;
		
		_playerContainers[(int) enumPlayer].transform.Find("DeathCounter").transform.Find("Text").gameObject.GetComponent<Text>().text = (currentScore < 10 && (currentScore % 10) > 0)?"0"+currentScore.ToString():currentScore.ToString();
	}
	
	
}