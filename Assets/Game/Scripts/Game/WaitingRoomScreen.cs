
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Game.Player;
using Game.Scripts.Game;

public class WaitingRoomScreen : MonoBehaviour
{
	[SerializeField] private Image[] _pressStartImages;
	[SerializeField] private Transform[] _playerSpawns;
	[SerializeField] private GameObject[] _playerContainers;
	[SerializeField] private TimeController _timeController;

	private List<EnumPlayer> _players;
	
	// Use this for initialization
	private void Start ()
	{
		Init();
		GetPlayers();
		_timeController.SetTimeUpCallback(OnTimeUp);
		_timeController.StartTime();
	}
	
	private void OnTimeUp()
	{
	    _timeController.StopTime();
		LoadMapScene();
	}

	private void LoadMapScene()
	{
		_timeController.StopTime();
		GameManager.Instance.LoadMapScene(1);
	}
	
	// Update is called once per frame
	private void Update()
	{
		if (UnityEngine.Input.GetButtonDown("P1Submit"))
		{
			GameManager.Instance.AddPlayer(EnumPlayer.Player1);
			GetPlayers();
		}
		
		if (UnityEngine.Input.GetButtonDown("P2Submit"))
		{
			GameManager.Instance.AddPlayer(EnumPlayer.Player2);
			GetPlayers();
		}
		
		if (UnityEngine.Input.GetButtonDown("P3Submit"))
		{
			GameManager.Instance.AddPlayer(EnumPlayer.Player3);
			GetPlayers();
		}
		
		if (UnityEngine.Input.GetButtonDown("P4Submit"))
		{
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
		
		GameManager.Instance.SpawnPlayer(enumPlayer, _playerSpawns[(int)enumPlayer]);
		
	}
}
