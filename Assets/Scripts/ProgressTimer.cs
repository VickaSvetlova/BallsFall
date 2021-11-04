using System;
using BallsFall.Settings;
using UnityEngine;

namespace BallsFall
{
    public class ProgressTimer : MonoBehaviour
    {
        [SerializeField] private GameSetting _gameSetting;

        public bool enable
        {
            set
            {
                Reset();
            }
            get { return enabled; }
        }

        private Vector2 progressRange;
        private float _timer;

        private void Awake()
        {
            Reset();
        }

        private void Update()
        {
            if (enable)
            {
                if (_timer > 0)
                {
                    _timer -= Time.deltaTime;
                }
                else
                {
                    UpLevelProgress(_gameSetting.accelerationStep);
                }
            }
        }

        private void UpLevelProgress(float gameSettingAccelerationStep)
        {
            _gameSetting.ProgressRuntime += gameSettingAccelerationStep;
            _timer = _gameSetting.accelerationStepTime;
            _gameSetting.ProgressUp();
        }

        public void Reset()
        {
            _gameSetting.ProgressRuntime = 0;
            _timer = _gameSetting.accelerationStepTime;
        }
    }
}