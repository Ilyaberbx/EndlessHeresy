using System;

namespace EndlessHeresy.Runtime.Timer
{
    public sealed class Timer
    {
        private readonly float _interval;
        private float _currentTime;
        private bool _isRunning;

        public event Action OnTick;

        public Timer(float interval)
        {
            _interval = interval;
            _currentTime = 0f;
            _isRunning = false;
        }

        public void Start()
        {
            _isRunning = true;
            _currentTime = 0f;
        }

        public void Stop()
        {
            _isRunning = false;
            _currentTime = 0f;
        }

        public void Pause()
        {
            _isRunning = false;
        }

        public void Resume()
        {
            _isRunning = true;
        }

        public void Update(float deltaTime)
        {
            if (!_isRunning) return;

            _currentTime += deltaTime;

            if (_currentTime < _interval)
            {
                return;
            }

            OnTick?.Invoke();
            _currentTime = 0;
        }
    }
}