using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{

	[SerializeField] private Text _winnerText;
	[SerializeField] private Text _killsText;
	[SerializeField] private Text _deathsText;
	void Start()
	{
		//get game result and show stats
		var gameResult = GameManager.Instance.GetGameResult();

		_winnerText.text = gameResult.Winner.ToString();
		_killsText.text = gameResult.Winner.ToString();
		_deathsText.text = gameResult.Winner.ToString();
		
	}
}
