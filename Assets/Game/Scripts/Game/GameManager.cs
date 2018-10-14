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

    public void LoadTitleScene(string unloadSceneName = "", string transitionText = "Loading")
    {
        PlayerPrefs.SetString("TransitionText", transitionText);
        PlayerPrefs.SetString("UnloadScene", unloadSceneName);
        PlayerPrefs.SetString("LoadScene", "TitleScene");
        
        SceneManager.LoadScene("TransitionScene", LoadSceneMode.Additive);
    }

    public void LoadWaitingRoomScene(string unloadSceneName = "", string transitionText = "Loading")
    {
        PlayerPrefs.SetString("TransitionText", transitionText);
        PlayerPrefs.SetString("UnloadScene", unloadSceneName);
        PlayerPrefs.SetString("LoadScene", "WaitingRoomScene");
        
        SceneManager.LoadScene("TransitionScene", LoadSceneMode.Additive);
    }
	
    public void LoadMapScene(int mapNumber, string unloadSceneName = "", string transitionText = "Loading")
    {
        PlayerPrefs.SetString("TransitionText", transitionText);
        PlayerPrefs.SetString("UnloadScene", unloadSceneName);
        PlayerPrefs.SetString("LoadScene", "Map" +mapNumber+ "Scene");
        
        SceneManager.LoadScene("TransitionScene", LoadSceneMode.Additive);
    }
	
    //add the scene to map scene to still show map while showing result
    public void LoadResultScene(string unloadSceneName = "", string transitionText = "Loading")
    {
        PlayerPrefs.SetString("TransitionText", transitionText);
        PlayerPrefs.SetString("UnloadScene", unloadSceneName);
        PlayerPrefs.SetString("LoadScene", "ResultScene");
        
        SceneManager.LoadScene("TransitionScene", LoadSceneMode.Additive);
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
        var player = Instantiate(ResourceFacade.LoadPrefab("Player" + ((int)enumPlayer + 1)), parent, false);
        return player.GetComponent<PlayerController>();
    }


    public List<GameResult> GetGameResult()
    {
        var json = PlayerPrefs.GetString("GameResults", "");
        var gameResults = JsonUtility.FromJson<GameResults>(json);
        return gameResults.Results;
    }
	
    public void SetGameResult(List<GameResult> gameResults)
    {
        var result = new GameResults(gameResults);
        var json = JsonUtility.ToJson(result);
        PlayerPrefs.SetString("GameResults", json);
        Debug.LogFormat("Game Result in Prefs: {0}", json);
    }
}