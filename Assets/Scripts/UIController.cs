using System;
using UnityEngine;
using UnityEngine.UI;

namespace BallsFall
{
    public class UIController : MonoBehaviour
    {
        public event Action OnStartClick;
        public event Action OnResetClick;
        [SerializeField] private Slider hpBar;
        [SerializeField] private Text rewardLable;
        [SerializeField] private Text rewardHistoryLable;
        [SerializeField] private Animator failScreen;
        [SerializeField] private Animator winScreen;
        [SerializeField] private GameObject speedUp;
        [SerializeField] private Text speedLabel;

        public void SetHpBar(Vector2 health)
        {
            hpBar.value = (health.x / health.y);
        }

        public void SetReward(int reward)
        {
            rewardLable.text = "Current score: " + reward;
        }

        public void SetHistoryReward(int reward)
        {
            rewardHistoryLable.text = "History score " + reward;
        }

        public void StartIntro(bool state)
        {
            winScreen.SetBool("IsActive", state);
        }

        public void StartFail((int, int) getHistoryReward, bool state)
        {
            failScreen.SetBool("IsActive", state);
            SetReward(getHistoryReward.Item1);
            SetHistoryReward(getHistoryReward.Item2);
        }

        public void StartButton(bool state)
        {
            winScreen.SetBool("IsActive", state);
            OnStartClick?.Invoke();
        }

        public void ResetButton(bool state)
        {
            failScreen.SetBool("IsActive", state);
            OnResetClick?.Invoke();
        }

        public void SpeedUp(float speed)
        {
            speedUp.SetActive(false);
            speedLabel.text = "SPEED UP! " + speed;
            speedUp.SetActive(true);
        }
    }
}