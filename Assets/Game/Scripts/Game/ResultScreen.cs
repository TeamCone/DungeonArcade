using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{

	[SerializeField] private Image _player1Image;
	[SerializeField] private Text _player1killsText;
	[SerializeField] private Text _player1deathsText;
	
	[SerializeField] private Image _player2Image;
	[SerializeField] private Text _player2killsText;
	[SerializeField] private Text _player2deathsText;
	
	[SerializeField] private Image _player3Image;
	[SerializeField] private Text _player3killsText;
	[SerializeField] private Text _player3deathsText;
	
	[SerializeField] private Image _player4Image;
	[SerializeField] private Text _player4killsText;
	[SerializeField] private Text _player4deathsText;
	
	void Start()
	{
		//get game result and show stats
		var gameResults = GameManager.Instance.GetGameResult();
		Debug.Log(gameResults);
		/*_winnerText.text = gameResult.Winner.ToString();
		_killsText.text = gameResult.Kills.ToString();
		_deathsText.text = gameResult.Deaths.ToString();*/

		foreach (var gameResult in gameResults)
		{
			Debug.LogFormat("Player {0}, Winner: {1}, Kills: {2}, Deaths: {3}", 
				gameResult.Player, gameResult.IsWinner, gameResult.Kills, gameResult.Deaths);

			_player1killsText.text = gameResult.Kills.ToString();
			_player1deathsText.text = gameResult.Deaths.ToString();
		}
		
	}
}
