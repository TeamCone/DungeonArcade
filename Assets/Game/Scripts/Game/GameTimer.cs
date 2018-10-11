using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Game
{
    public class GameTimer: ITimer
    {
        private int _time = 90;
        private bool _isPaused;
        private bool _isTimerStopped;

        public async void StartTime(int timeInSeconds, Action<int> currentTimeLeftCallback, Action onTimeUpCallback)
        {
            _time = timeInSeconds;
            while (_time > 0)
            {   
                currentTimeLeftCallback?.Invoke(_time);
                await DeductTimeByOneSecond();
                if (_isPaused)
                {
                    continue;
                }
                _time--;
            }

            if (_isTimerStopped)
            {
                return;
            }
            onTimeUpCallback?.Invoke();
        }

        public void StopTime()
        {
            _time = 0;
            _isTimerStopped = true;
        }

        public void PauseTime()
        {
            _isPaused = true;
        }
        
        public void ResumeTime()
        {
            _isPaused = false;
        }
        
        private IEnumerator DeductTimeByOneSecond()
        {
            yield return new WaitForSeconds(1);
        }
        
        private IEnumerator SkipFrame()
        {
            yield return new WaitForSeconds(0);
        }
    }
}