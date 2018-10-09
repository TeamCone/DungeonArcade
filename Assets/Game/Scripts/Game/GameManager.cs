using System.Collections.Generic;
using Game.Player;
using Game.Scripts.Player;
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

	public void LoadWaitingRoomScene()
	{
		SceneManager.LoadScene("WaitingRoomScene");
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

	public void AddPlayer(EnumPlayer player)
	{
		var players = GetPlayers();
		players.list.Add(player);
		var json = JsonUtility.ToJson(players);
		PlayerPrefs.SetString("Players", json);
	}

	public Players GetPlayers()
	{
		var json = PlayerPrefs.GetString("Players", "");
		if (string.IsNullOrEmpty(json))
		{
			return new Players();
		}
			
		var players = JsonUtility.FromJson<Players>(json);
		return players;
	}

	public void ClearPlayers()
	{
		var players = GetPlayers();
		players.list.Clear();
		var json = JsonUtility.ToJson(players);
		PlayerPrefs.SetString("Players", json);
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
