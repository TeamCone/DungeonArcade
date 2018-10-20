
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Game.Input;
using Game.Player;
using Game.Scripts.Game;

public class WaitingRoomScreen : MonoBehaviour
{
	[SerializeField] private Image[] _pressStartImages;
	[SerializeField] private Transform[] _playerSpawns;
	[SerializeField] private GameObject[] _playerContainers;
	[SerializeField] private TimeController _timeController;
	[SerializeField] private GameInputController _gameInputController;

	private List<EnumPlayer> _players;
	private bool _hasPlayer4Entered;
	private bool _hasPlayer2Entered;
	private bool _hasPlayer1Entered;
	private bool _hasPlayer3Entered;
	private int _timeCounter;

	// Use this for initialization
	private void Start ()
	{
		Init();
		GetPlayers();
		_timeController.SetTimeUpCallback(OnTimeUp, OnTimeCount);
		_timeController.StartTime();
	}

	private void OnTimeCount(int timeCounter)
	{
		_timeCounter = timeCounter;
	}

	private void OnTimeUp()
	{
		SoundManager.Instance.PlaySfx("SfxTimesupgong");
	    _timeController.StopTime();
		LoadMapScene();
	}

	private void LoadMapScene()
	{
		_timeController.StopTime();
		GameManager.Instance.LoadMapScene(1, "WaitingRoomScene", "Loading Arena");
	}
	
	// Update is called once per frame
	private void Update()
	{
		if (_timeCounter == 1)
		{
			return;
		}
		
		if (UnityEngine.Input.GetButtonDown("P1Submit"))
		{
			if (_hasPlayer1Entered)
			{
				return;
			}

			SoundManager.Instance.PlaySfx("SfxTimechime1");
			GameManager.Instance.AddPlayer(EnumPlayer.Player1);
			GetPlayers();
		}
		
		if (UnityEngine.Input.GetButtonDown("P2Submit"))
		{
			if (_hasPlayer2Entered)
			{
				return;
			}

			SoundManager.Instance.PlaySfx("SfxTimechime1");
			GameManager.Instance.AddPlayer(EnumPlayer.Player2);
			GetPlayers();
		}
		
		if (UnityEngine.Input.GetButtonDown("P3Submit"))
		{
			if (_hasPlayer3Entered)
			{
				return;
			}

			SoundManager.Instance.PlaySfx("SfxTimechime1");
			GameManager.Instance.AddPlayer(EnumPlayer.Player3);
			GetPlayers();
		}
		
		if (UnityEngine.Input.GetButtonDown("P4Submit"))
		{
			if (_hasPlayer4Entered)
			{
				return;
			}

			
			SoundManager.Instance.PlaySfx("SfxTimechime1");
			GameManager.Instance.AddPlayer(EnumPlayer.Player4);
			GetPlayers();
		}
	}
	
	private void GetPlayers()
	{
		var players = GameManager.Instance.GetPlayers();

		if (players.list.Any(player => player == EnumPlayer.Player1))
		{
			if (_players.Any(player => player == EnumPlayer.Player1) == false)
			{
				_hasPlayer1Entered = true;
				_players.Add(EnumPlayer.Player1);
				ShowPlayer(EnumPlayer.Player1);
			}
		}
		
		if (players.list.Any(player => player == EnumPlayer.Player2))
		{
			if (_players.Any(player => player == EnumPlayer.Player2) == false)
			{
				_hasPlayer2Entered = true;
				_players.Add(EnumPlayer.Player2);
				ShowPlayer(EnumPlayer.Player2);
			}
		}
		
		if (players.list.Any(player => player == EnumPlayer.Player3))
		{
			if (_players.Any(player => player == EnumPlayer.Player3) == false)
			{
				_hasPlayer3Entered = true;
				_players.Add(EnumPlayer.Player3);
				ShowPlayer(EnumPlayer.Player3);
			}
		}
		
		if (players.list.Any(player => player == EnumPlayer.Player4))
		{
			if (_players.Any(player => player == EnumPlayer.Player4) == false)
			{
				_hasPlayer4Entered = true;
				_players.Add(EnumPlayer.Player4);
				ShowPlayer(EnumPlayer.Player4);
			}
		}

		if (_players.Count == 4)
		{
			_timeController.StopTime();
			LoadMapScene();
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
		
		var player = GameManager.Instance.SpawnPlayer(enumPlayer, _playerSpawns[(int)enumPlayer]);
		_gameInputController.SetPlayer(enumPlayer, player);
		
	}
}
