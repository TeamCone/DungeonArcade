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

        private Action OnTimeUpCallback;
        
        private void Start()
        {
            _sprites = Resources.LoadAll<Sprite>("dungeon-tileset 1");
            _timer = new GameTimer();
            _timer.StartTime(_gameTime, PrintTimer);
            
            //Sample Callback for times up
            SetTimeUpCallback(() =>
            {
                Debug.Log("Times Up!");
            });
        }

        public void SetTimeUpCallback(Action onTimeUpCallback)
        {
            OnTimeUpCallback += onTimeUpCallback;
        }

        private async void PrintTimer(int currentTime)
        {
            int[] values =
            {
                currentTime / 100 % 10,
                currentTime / 10 % 10,
                currentTime % 10
            };

            for (int i = 0; i < _timeImages.Length; i++)
            {
                var value = values[i] + 32;
                Sprite sprite = _sprites.Single(s =>
                {
                    return s.name == BaseFilename + value;
                });
                _timeImages[i].sprite = sprite;
            }

            if (currentTime == 0)
            {
                await DelayWithSeconds(0);
                OnTimeUpCallback?.Invoke();
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