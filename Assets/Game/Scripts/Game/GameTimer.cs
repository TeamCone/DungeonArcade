using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Game
{
    public class GameTimer
    {
        private int _time = 90;

        public async void StartTime(int timeInSeconds, Action<int> currentTimeLeftCallback)
        {
            _time = timeInSeconds;
            while (_time >= 0)
            {
                currentTimeLeftCallback.Invoke(_time);
                await DeductTimeByOneSecond();
                _time--;
            }      
        }
        
        private IEnumerator DeductTimeByOneSecond()
        {
            yield return new WaitForSeconds(1);
        }
    }
}