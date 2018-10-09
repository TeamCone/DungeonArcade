using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts.Game
{
    public class TimeController : MonoBehaviour
    {
        private GameTimer _timer;
        [SerializeField]
        private int _gameTime = 90;
        
        [SerializeField]
        private Image[] _timeImages = new Image[3];
        private Sprite[] _sprites;
        private const string BaseFilename = "dungeon-tileset 1_";

        private bool _isPaused;

        private Action _onTimeUpCallback;
        
        private void Start()
        {
            
            
            //Main Timer
            //StartTime();

            //To Pause Timer
            //PauseTime();

            //To Resume Timer
            //ResumeTime();

        }

        public void StartTime()
        {
            _sprites = Resources.LoadAll<Sprite>("dungeon-tileset 1");
            _timer = new GameTimer();
            _timer.StartTime(_gameTime, PrintTimer, _onTimeUpCallback);
        }

        public void SetTimeUpCallback(Action onTimeUpCallback)
        {
            _onTimeUpCallback = onTimeUpCallback;
        }

        public void StopTime()
        {
           _timer.StopTime();
           _isPaused = true;
        }

        public void PauseTime()
        {
            _timer.PauseTime();
            _isPaused = true;
        }
        
        public void ResumeTime()
        {
            _timer.ResumeTime();
            _isPaused = false;
        }

        private void PrintTimer(int currentTime)
        {
            int[] values =
            {
                currentTime / 100 % 10,
                currentTime / 10 % 10,
                currentTime % 10
            };

            if (_isPaused == false)
            {
                for (var i = 0; i < _timeImages.Length; i++)
                {
                    var value = values[i] + 32;
                    var sprite = _sprites.Single(s =>
                    {
                        return s.name == BaseFilename + value;
                    });
                    _timeImages[i].sprite = sprite;
                }
            }
        }
        
        //Sample Delay Coroutine
        private IEnumerator DelayWithSeconds(int n)
        {
            yield return new WaitForSeconds(n);
        }

        
        private void FixedUpdate()
        {
            //throw new System.NotImplementedException();
        }

    }
}