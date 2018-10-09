using Game.Player;
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
	
	//add the scene to map scene to still show map while showing result
	public void LoadResultScene()
	{
		SceneManager.LoadScene("ResultScene", LoadSceneMode.Additive);
	}

	public PlayerController SpawnPlayer(EnumPlayer enumPlayer, Transform parent)
	{
		var player = Instantiate(Resources.Load("Player" + ((int)enumPlayer + 1)), parent, false) as GameObject;
		return player.GetComponent<PlayerController>();
	}


	public GameResult GetGameResult()
	{
		var json = PlayerPrefs.GetString("GameResult", "");
		var gameResult = JsonUtility.FromJson<GameResult>(json);
		return gameResult;
	}
	
	public void SetGameResult(GameResult gameResult)
	{
		var json = JsonUtility.ToJson(gameResult);
		PlayerPrefs.SetString("GameResult", json);
	}
}
