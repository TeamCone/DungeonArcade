using Game.Player;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{

	private bool _hasPlayerStarted;

	private void Start()
	{
		GameManager.Instance.ClearPlayers();
	}

	private void Update()
	{
		if (_hasPlayerStarted)
		{
			return;
		}
		
		if (Input.GetButtonDown("P1Submit"))
		{
			_hasPlayerStarted = true;
			LoadWaitingRoom(EnumPlayer.Player1);
		}
		
		else if (Input.GetButtonDown("P2Submit"))
		{
			_hasPlayerStarted = true;
			LoadWaitingRoom(EnumPlayer.Player2);
		}
		
		else if (Input.GetButtonDown("P3Submit"))
		{
			_hasPlayerStarted = true;
			LoadWaitingRoom(EnumPlayer.Player3);
		}
		
		else if (Input.GetButtonDown("P4Submit"))
		{
			_hasPlayerStarted = true;
			LoadWaitingRoom(EnumPlayer.Player4);
		}
	}

	private void LoadWaitingRoom(EnumPlayer enumPlayer)
	{
		GameManager.Instance.AddPlayer(enumPlayer);
		
		GameManager.Instance.LoadWaitingRoomScene("TitleScene");
	}
	
}
