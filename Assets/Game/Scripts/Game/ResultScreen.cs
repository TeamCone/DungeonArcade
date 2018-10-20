using System.Collections;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{

	[SerializeField] private Transform _resultContainer;
	async void Start()
	{
		var gameResults = GameManager.Instance.GetGameResult();

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
		yield return new WaitForSeconds(3);
		if (GameManager.Instance.GetMapNumber() < 5)
		{
			GameManager.Instance.LoadMapScene(GameManager.Instance.GetMapNumber() + 1, "ResultScene", "Loading Next Arena");
			yield break;
		}
		
		GameManager.Instance.LoadTitleScene("ResultScene");
	} 
}
