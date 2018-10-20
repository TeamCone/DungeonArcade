using Game.Player;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{

	private bool _hasPlayerStarted;

	private void Start()
	{
		SoundManager.Instance.PlayBgm("BgmMenu");
		GameManager.Instance.ClearGameResult();
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
		
		else if (Input.GetButtonDown("P2Submit"))
		{
			LoadWaitingRoom(EnumPlayer.Player2);
		}
		
		else if (Input.GetButtonDown("P3Submit"))
		{
			LoadWaitingRoom(EnumPlayer.Player3);
		}
		
		else if (Input.GetButtonDown("P4Submit"))
		{
			LoadWaitingRoom(EnumPlayer.Player4);
		}
	}

	private void LoadWaitingRoom(EnumPlayer enumPlayer)
	{
		_hasPlayerStarted = true;
		SoundManager.Instance.PlaySfx("SfxTimechime1");
		GameManager.Instance.AddPlayer(enumPlayer);
		
		GameManager.Instance.LoadWaitingRoomScene("TitleScene");
	}
	
}
