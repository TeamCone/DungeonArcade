using System;

namespace Game.Scripts.Game
{
    public interface ITimer
    {
        void StartTime(int timeInSeconds, Action<int> currentTimeLeftCallback, Action onTimeUpCallback);
        void StopTime();
        void PauseTime();
        void ResumeTime();
    }
}