﻿using System.Collections.Generic;
using System.Linq;
using Game.Input;
using Game.Player;
using Game.Scripts.Game;
using UnityEngine;
using UnityEngine.UI;

public class MapScreen : MonoBehaviour
{

    [SerializeField] private GameInputController _gameInputController;
	
	[SerializeField] private Image[] _pressStartImages;
	[SerializeField] private Transform[] _playerSpawns;
	[SerializeField] private GameObject[] _playerContainers;
	[SerializeField] private TimeController _timeController;

	private List<EnumPlayer> _players;
	
    void Start () 
    {
	    Init();
	    GetPlayers();
	    
	    _timeController.SetTimeUpCallback(OnTimeUp);
    }

	//when time up, remove controls of each player and load result screen
	private void OnTimeUp()
	{
		_gameInputController.SetPlayer(EnumPlayer.Player1, null);
		_gameInputController.SetPlayer(EnumPlayer.Player2, null);
		_gameInputController.SetPlayer(EnumPlayer.Player3, null);
		_gameInputController.SetPlayer(EnumPlayer.Player4, null);
		GameManager.Instance.LoadResultScene();
	}

    private void SpawnPlayer(EnumPlayer enumPlayer)
    {
        var player = GameManager.Instance.SpawnPlayer(enumPlayer, _playerSpawns[(int)enumPlayer]);
        _gameInputController.SetPlayer(enumPlayer, player);

    }
	
	private void GetPlayers()
	{
		var players = GameManager.Instance.GetPlayers();

		if (players.list.Any(player => player == EnumPlayer.Player1))
		{
			if (_players.Any(player => player == EnumPlayer.Player1) == false)
			{
				_players.Add(EnumPlayer.Player1);
				ShowPlayer(EnumPlayer.Player1);
			}
		}
		
		if (players.list.Any(player => player == EnumPlayer.Player2))
		{
			if (_players.Any(player => player == EnumPlayer.Player2) == false)
			{
				_players.Add(EnumPlayer.Player2);
				ShowPlayer(EnumPlayer.Player2);
			}
		}
		
		if (players.list.Any(player => player == EnumPlayer.Player3))
		{
			if (_players.Any(player => player == EnumPlayer.Player3) == false)
			{
				_players.Add(EnumPlayer.Player3);
				ShowPlayer(EnumPlayer.Player3);
			}
		}
		
		if (players.list.Any(player => player == EnumPlayer.Player4))
		{
			if (_players.Any(player => player == EnumPlayer.Player4) == false)
			{
				_players.Add(EnumPlayer.Player4);
				ShowPlayer(EnumPlayer.Player4);
			}
		}
	}
	
	private void Init()
	{
		_players = new List<EnumPlayer>();
		for (var i = 0; i < 4; i++)
		{
			_pressStartImages[i].gameObject.SetActive(true);
			_playerContainers[i].gameObject.SetActive(false);
		}
		
	}
	
	private void ShowPlayer(EnumPlayer enumPlayer)
	{
		_pressStartImages[(int) enumPlayer].gameObject.SetActive(false);
		_playerContainers[(int) enumPlayer].gameObject.SetActive(true);

		SpawnPlayer(enumPlayer);

	}
	
	
	
}