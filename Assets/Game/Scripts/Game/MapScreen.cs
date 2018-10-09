using Game.Input;
using Game.Player;
using Game.Scripts.Game;
using UnityEngine;

public class MapScreen : MonoBehaviour
{

    [SerializeField] private Transform[] _playerSpawns;
    [SerializeField] private GameInputController _gameInputController;
	[SerializeField] private TimeController _timeController;
	
	
    void Start () 
    {
        SpawPlayer(EnumPlayer.Player1);
        SpawPlayer(EnumPlayer.Player2);
        SpawPlayer(EnumPlayer.Player3);
        SpawPlayer(EnumPlayer.Player4);
	    
	    _timeController.SetTimeUpCallback(OnTimeUp);
    }

	//when time up, remove controls of each player and load result screen
	private void OnTimeUp()
	{
		_gameInputController.SetPlayer(EnumPlayer.Player1, null);
		_gameInputController.SetPlayer(EnumPlayer.Player2, null);
		_gameInputController.SetPlayer(EnumPlayer.Player3, null);
		_gameInputController.SetPlayer(EnumPlayer.Player4, null);
		GameManager.Instance.LoadResultScene();
	}

    private void SpawPlayer(EnumPlayer enumPlayer)
    {
        var player = GameManager.Instance.SpawnPlayer(enumPlayer, _playerSpawns[(int)enumPlayer]);
        _gameInputController.SetPlayer(enumPlayer, player);

    }
	
	
	
}