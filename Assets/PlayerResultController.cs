using System;
using Game.Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResultController : MonoBehaviour
{

	[SerializeField] private Image _characterImage;
	[SerializeField] private Image _skullImage;
	[SerializeField] private Image _crownImage;
	
	[SerializeField] private Text _killsText;
	[SerializeField] private Text _deathsText;
	[SerializeField] private Image _deathsCounterImage;
	[SerializeField] private Image _killsCounterImage;
	
	[SerializeField] private Sprite _player1Sprite;
	[SerializeField] private Sprite _player2Sprite;
	[SerializeField] private Sprite _player3Sprite;
	[SerializeField] private Sprite _player4Sprite;
	
	
	
	private void Awake()
	{
		_skullImage.gameObject.SetActive(false);
		_crownImage.gameObject.SetActive(false);
		_killsText.text = "";
		_deathsText.text = "";
	}

	private void SetUIColors(Color32 color32)
	{
		_killsText.color = color32;
		_deathsText.color = color32;
		_deathsCounterImage.color = color32;
		_killsCounterImage.color = color32;
	}
	

	public void SetPlayerResults(GameResult gameResult)
	{
		
		switch ((EnumPlayer)gameResult.Player)
		{
			case EnumPlayer.Player1:
				
				_characterImage.sprite = _player1Sprite;
				SetUIColors(new Color32(143, 151, 74,255));
				break;
			case EnumPlayer.Player2:
				_characterImage.sprite = _player2Sprite;
				SetUIColors(new Color32(91, 110, 225,255));
				break;
			case EnumPlayer.Player3:
				_characterImage.sprite = _player3Sprite;
				SetUIColors(new Color32(251, 242, 54,255));
				break;
			case EnumPlayer.Player4:
				_characterImage.sprite = _player4Sprite;
				SetUIColors(new Color32(217, 87, 99,255));
				break;
			case EnumPlayer.None:
				break;
		}
		
		_killsText.text = gameResult.Kills.ToString();
		_deathsText.text = gameResult.Deaths.ToString();
		
		if (gameResult.IsWinner)
		{
			GameManager.Instance.AddWinner((EnumPlayer)gameResult.Player);
			_crownImage.gameObject.SetActive(true);
		}
		else
		{
			_skullImage.gameObject.SetActive(true);
		}
	}

	
}
