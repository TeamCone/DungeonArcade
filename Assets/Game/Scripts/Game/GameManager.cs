using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void LoadTitleScene()
	{
		SceneManager.LoadScene("TitleScene");
	}

	public void LoadSelectionScene()
	{
		SceneManager.LoadScene("SelectionScene");
	}
	
	public void LoadMapScene(int mapNumber)
	{
		SceneManager.LoadScene("Map" +mapNumber+ "Scene");
	}
	
	//add the sene to map scene to still show map while showing result
	public void LoadResultScene()
	{
		SceneManager.LoadScene("ResultScene", LoadSceneMode.Additive);
	}


	public GameResult GetGameResult()
	{
		var json = PlayerPrefs.GetString("GameResult", "");
		
		if (string.IsNullOrEmpty(json))
		{
			return null;
		}
		
		var gameResult = JsonUtility.FromJson<GameResult>(json);
		return gameResult;
	}
	
	public void SetGameResult(GameResult gameResult)
	{
		var json = JsonUtility.ToJson(gameResult);
		PlayerPrefs.SetString("GameResult", json);
	}
}
