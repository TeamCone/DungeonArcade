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
			LoadWaitingRoom(EnumPlayer.Player1);
		}
		
		if (Input.GetButtonDown("P2Submit"))
		{
			LoadWaitingRoom(EnumPlayer.Player2);
		}
		
		if (Input.GetButtonDown("P3Submit"))
		{
			LoadWaitingRoom(EnumPlayer.Player3);
		}
		
		if (Input.GetButtonDown("P4Submit"))
		{
			LoadWaitingRoom(EnumPlayer.Player4);
		}
	}

	private void LoadWaitingRoom(EnumPlayer enumPlayer)
	{
		GameManager.Instance.AddPlayer(enumPlayer);
		_hasPlayerStarted = true;
		GameManager.Instance.LoadWaitingRoomScene("TitleScene");
	}
	
}
