﻿using System;
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
	private int _timeCounter;
	private bool _isPlayerEntering;
	private bool _hasPlayer1Entered;
	private bool _hasPlayer2Entered;
	private bool _hasPlayer3Entered;
	private bool _hasPlayer4Entered;

	void Start ()
	{
		_isPlayerEntering = false;
	    SoundManager.Instance.PlayBgm("BgmBattle");
	    Init();
	    GetPlayers();
	    _timeController.SetTimeUpCallback(OnTimeUp, OnTimerCount);
	    _timeController.StartTime();
	    SoundManager.Instance.PlayBgm("BgmBattle");
    }

	private void OnTimerCount(int currentTime)
	{
		_timeCounter = currentTime;
		
		if (currentTime == 1)
		{
			//start slow motion
			Time.timeScale = 0.3f;
		}
	}



	//when time up, remove controls of each player and load result screen
	private void OnTimeUp()
	{
		//stop slow motion
		Time.timeScale = 1f;
		
		SoundManager.Instance.PlaySfx("SfxTimesupgong");
		_gameInputController.SetPlayer(EnumPlayer.Player1, null);
		_gameInputController.SetPlayer(EnumPlayer.Player2, null);
		_gameInputController.SetPlayer(EnumPlayer.Player3, null);
		_gameInputController.SetPlayer(EnumPlayer.Player4, null);

		GameManager.Instance.SubmitGameResult();
		GameManager.Instance.LoadResultScene(GameManager.Instance.GetMapName(),"Time's Up!");
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
		if (_timeCounter == 1)
		{
			return;
		}

		if (_isPlayerEntering)
		{
			return;
		}

		_isPlayerEntering = true;
		_timeController.StopTime();


		switch (enumPlayer)
		{
			case EnumPlayer.Player1:
				break;
			case EnumPlayer.Player2:
				break;
			case EnumPlayer.Player3:
				break;
			case EnumPlayer.Player4:
				break;
			case EnumPlayer.None:
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(enumPlayer), enumPlayer, null);
		}
		GameManager.Instance.AddPlayer(enumPlayer);
		GameManager.Instance.LoadMapScene(GameManager.Instance.GetMapNumber(), GameManager.Instance.GetMapName(), "New Challenger");
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