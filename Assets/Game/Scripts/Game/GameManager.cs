using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Game.Player;
using Game.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private List<GameResult> _gameResults = new List<GameResult>();

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
        PlayerPrefs.SetInt("MapNumber", mapNumber);
        
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
    
    public void LoadOverallResultScene(string unloadSceneName = "", string transitionText = "Loading")
    {
        
        PlayerPrefs.SetString("TransitionText", transitionText);
        PlayerPrefs.SetString("UnloadScene", unloadSceneName);
        PlayerPrefs.SetString("LoadScene", "OverallResultScene");
        
        SceneManager.LoadScene("TransitionScene", LoadSceneMode.Additive);
    }
    
 
    public void ResetMap()
    {
        PlayerPrefs.SetInt("MapNumber", 1);
    }
    
    public string GetMapName()
    {
        return "Map" +PlayerPrefs.GetInt("MapNumber", 1)+ "Scene";
    }
    
    
    public int GetMapNumber()
    {
        return PlayerPrefs.GetInt("MapNumber", 1);
    }

    
    
    
    
    public void AddPlayer(EnumPlayer player)
    {
        var players = GetPlayers();

        if (players.list.Contains(player))
        {
            return;
        }
        
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
        if (string.IsNullOrEmpty(json))
        {
            return new List<GameResult>();
        }
        var gameResults = JsonUtility.FromJson<GameResults>(json);
        return gameResults.Results;
    }
	
    private void SetGameResult(List<GameResult> gameResults)
    {
        var result = new GameResults(gameResults);
        var json = JsonUtility.ToJson(result);
        PlayerPrefs.SetString("GameResults", json);
        Debug.LogFormat("Game Result in Prefs: {0}", json);
    }

    public void SubmitGameResult()
    {
	    // compare only if more than 1 player
	    if (_gameResults.Count > 1)
	    {
		    // Check most kills amount and find players with most kills
		    var maxKills = _gameResults.Max(g => g.Kills);
		    var playersWithMostKills = _gameResults.Where(g => g.Kills == maxKills).ToList();
				
		    //check if only one player has more kills
		    if (playersWithMostKills.Count == 1)
		    {
			    //GameManager.Instance.AddWinner((EnumPlayer) playersWithMostKills[0].Player);
			    for(var i = 0; i < _gameResults.Count; i++)
			    {
				    if (_gameResults[i].Player == playersWithMostKills[0].Player)
				    {
					    _gameResults[i].IsWinner = true;
				    }
			    }
		    }
		    else
		    {
			    var minDeaths = playersWithMostKills.Min(p => p.Deaths);
			    var playersWithLeastDeaths = playersWithMostKills.Where(g => g.Deaths == minDeaths).ToList();

			    if (playersWithLeastDeaths.Count == 1)
			    {
				    for(var i = 0; i < _gameResults.Count; i++)
				    {
					    if (_gameResults[i].Player == playersWithLeastDeaths[0].Player)
					    {
						    _gameResults[i].IsWinner = true;
					    }
				    }
			    }
		    }
	    }
        SetGameResult(_gameResults);
    }
    public void ClearGameResult()
    {
        _gameResults.Clear();
        var result = GetGameResult();
        result.Clear();
        SetGameResult(result);
    }
    

    public void InitGameResults()
    {
        var players = GetPlayers();
        _gameResults.Clear();
        foreach (var player in players.list)
        {
            _gameResults.Add(new GameResult
            {
                Player = (int)player,
                IsWinner = false,
                Deaths = 0,
                Kills = 0
            });
        }
    }

    public void AddKills(EnumPlayer enumPlayer)
    {
        
        try
        {
            var gameResult = _gameResults.First(x => x.Player == (int) enumPlayer);
            gameResult.Kills++;
        }
        catch (Exception e)
        {
        }
      
    }
    
    public void AddDeaths(EnumPlayer enumPlayer)
    {
        try
        {
            var gameResult = _gameResults.First(x => x.Player == (int) enumPlayer);
            gameResult.Deaths++;
        }
        catch (Exception e)
        {
        }
     
    }

    public void HasTreasure(EnumPlayer enumPlayer, bool hasTreasure)
    {
        try
        {
            var gameResult = _gameResults.First(x => x.Player == (int) enumPlayer);
            gameResult.IsWinner = hasTreasure;
        } catch (Exception e)
        {
        }
        
    }


    public EnumPlayer GetOverallWinner()
    {
        return GetOverAllResult().GetOverAllWinner();
    }
    
    public void AddWinner(EnumPlayer enumPlayer)
    {
        var overAllResult = GetOverAllResult();
        overAllResult.AddWinner(enumPlayer);  
        SetOverAllResult(overAllResult);
    }

    public void ClearOverAllWinner()
    {
        var overAllResult = GetOverAllResult();
        overAllResult = new OverAllResult();
        SetOverAllResult(overAllResult);
    }

    
    private void SetOverAllResult(OverAllResult overAllResult)
    {
        var newJson = JsonUtility.ToJson(overAllResult);
        PlayerPrefs.SetString("OverAllResult", newJson);
    }
    
    private OverAllResult GetOverAllResult()
    {
        var json = PlayerPrefs.GetString("OverAllResult", "");
        var overAllResult = new OverAllResult();
        if (string.IsNullOrEmpty(json) == false)
        {
            overAllResult = JsonUtility.FromJson<OverAllResult>(json);
        }

        return overAllResult;
    }
}