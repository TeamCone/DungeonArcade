using System;
using System.Collections;
using System.Linq;
using Game.Player;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{

	[SerializeField] private Transform _resultContainer;
	async void Start()
	{
		var gameResults = GameManager.Instance.GetGameResult();

		//Check if someone has the idol
		var isThereWinner = gameResults.Any(x => x.IsWinner);
		if (isThereWinner == false)
		{		
			GameManager.Instance.AddWinner(EnumPlayer.None);
		}
		
		foreach (var gameResult in gameResults)
		{
			var playerResult = Instantiate(ResourceFacade.LoadPrefab("PlayerResult"), _resultContainer).GetComponent<PlayerResultController>();
			playerResult.SetPlayerResults(gameResult);
		}

		GameManager.Instance.ClearGameResult();
		await TitleScreenDelay();
	}

	private IEnumerator TitleScreenDelay()
	{
		yield return new WaitForSeconds(2);
		if (GameManager.Instance.GetMapNumber() < 5)
		{
			GameManager.Instance.LoadMapScene(GameManager.Instance.GetMapNumber() + 1, "ResultScene", "Loading Next Arena");
			yield break;
		}
		
		GameManager.Instance.LoadOverallResultScene("ResultScene");
	} 
}
