using Game.Player;
using UnityEngine;

public class TitleScreen : MonoBehaviour {


	private void Start()
	{
		GameManager.Instance.ClearPlayers();
	}
	//test only
	private void Update()
	{
		if (UnityEngine.Input.GetButtonDown("P1Submit"))
		{
			GameManager.Instance.AddPlayer(EnumPlayer.Player1);
			GameManager.Instance.LoadWaitingRoomScene();
		}
		
		if (UnityEngine.Input.GetButtonDown("P2Submit"))
		{
			GameManager.Instance.AddPlayer(EnumPlayer.Player2);
			GameManager.Instance.LoadWaitingRoomScene();
		}
		
		if (UnityEngine.Input.GetButtonDown("P3Submit"))
		{
			GameManager.Instance.AddPlayer(EnumPlayer.Player3);
			GameManager.Instance.LoadWaitingRoomScene();
		}
		
		if (UnityEngine.Input.GetButtonDown("P4Submit"))
		{
			GameManager.Instance.AddPlayer(EnumPlayer.Player4);
			GameManager.Instance.LoadWaitingRoomScene();
		}
	}
	
}
