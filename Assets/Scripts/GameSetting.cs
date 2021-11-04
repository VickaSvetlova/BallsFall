using System;
using UnityEngine;

namespace BallsFall.Settings
{
    [Serializable]
    public struct ScreenModel
    {
        public float leftBorder;
        public float rightBorder;
        public float upBorder;
        public float downBorder;
    }

    [CreateAssetMenu(fileName = "SettingsGame", menuName = "SettingsGame", order = 0)]
    public class GameSetting : ScriptableObject
    {
        public event Action<float> OnSpeedUp;
        public Vector2 rangeRewardValuesMinMax;
        public Vector2 rangeDamageValuesMinMax;
        public Vector2 rangeSpeedValuesMinMax;
        public float accelerationStep;
        public float accelerationStepTime;
        public float frequencyCreationBallsSeconds;
        [Range(0.1f, 1)] public float ballSize;
        public ScreenModel screenBorder;
        [NonSerialized] public float ProgressRuntime;
        public float playerHealth;

        public void ProgressUp()
        {
            OnSpeedUp?.Invoke(ProgressRuntime);
        }
    }
}