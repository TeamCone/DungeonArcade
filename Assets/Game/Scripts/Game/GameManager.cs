﻿using System;
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
	
    private void SetGameResult(List<GameResult> gameResults)
    {
        var result = new GameResults(gameResults);
        var json = JsonUtility.ToJson(result);
        PlayerPrefs.SetString("GameResults", json);
        Debug.LogFormat("Game Result in Prefs: {0}", json);
    }

    public void SubmitGameResult()
    {
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
    
    
    
    
}