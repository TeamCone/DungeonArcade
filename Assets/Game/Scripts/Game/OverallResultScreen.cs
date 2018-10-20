using System.Collections;
using Game.Player;
using UnityEngine;
using UnityEngine.UI;

public class OverallResultScreen : MonoBehaviour
{
	[SerializeField] private Image _playerImage;
	[SerializeField] private Sprite _player1Sprite;
	[SerializeField] private Sprite _player2Sprite;
	[SerializeField] private Sprite _player3Sprite;
	[SerializeField] private Sprite _player4Sprite;
	[SerializeField] private Sprite _noWinnerSprite;
	[SerializeField] private Text _winnerText;

		
	async void Start()
	{
		//dummy
		SoundManager.Instance.PlayBgm("BgmVictory");
		SetWinner(GameManager.Instance.GetOverallWinner());
		
		await TitleScreenDelay();
	}

	private IEnumerator TitleScreenDelay()
	{
		yield return new WaitForSeconds(3);
		GameManager.Instance.ClearOverAllWinner();
		GameManager.Instance.LoadTitleScene("OverallResultScene");
	} 

	private void SetWinner(EnumPlayer enumPlayer)
	{
		_winnerText.text = "WINNER";
		switch (enumPlayer)
		{
			case EnumPlayer.Player1:
				_playerImage.sprite = _player1Sprite;
				break;
			case EnumPlayer.Player2:
				_playerImage.sprite = _player2Sprite;
				break;
			case EnumPlayer.Player3:
				_playerImage.sprite = _player3Sprite;
				break;
			case EnumPlayer.Player4:
				_playerImage.sprite = _player4Sprite;
				break;
			case EnumPlayer.None:
				_winnerText.text = "NO WINNERS";
				_playerImage.sprite = _noWinnerSprite;
				break;
		}
	}


}
