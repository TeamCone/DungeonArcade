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
			// compare only if more than 1 player
			if (gameResults.Count > 1)
			{
				// Check most kills amount and find players with most kills
				var maxKills = gameResults.Max(g => g.Kills);
				var playersWithMostKills = gameResults.Where(g => g.Kills == maxKills).ToList();
				
				//check if only one player has more kills
				if (playersWithMostKills.Count == 1)
				{
					GameManager.Instance.AddWinner((EnumPlayer) playersWithMostKills[0].Player);
				}
				else
				{
					var minDeaths = playersWithMostKills.Min(p => p.Deaths);
					var playersWithLeastDeaths = playersWithMostKills.Where(g => g.Deaths == minDeaths).ToList();

					if (playersWithLeastDeaths.Count == 1)
					{
						GameManager.Instance.AddWinner((EnumPlayer) playersWithLeastDeaths[0].Player);
					}
					else
					{
						GameManager.Instance.AddWinner(EnumPlayer.None);
					}
				}
			}
			else
			{				
				GameManager.Instance.AddWinner(EnumPlayer.None);
			}
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
