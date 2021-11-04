using System;
using UnityEngine;

namespace BallsFall
{
    public class RepositoryAwards : MonoBehaviour
    {
        public event Action<int> OnChangeRewards;
        private int _historyReward;
        private int _currentReward;

        public (int, int) GetReward()
        {
            if (_currentReward > _historyReward)
                _historyReward = _currentReward;
            return (_currentReward, _historyReward);
        }

        public void SetReward(int reward)
        {
            _currentReward += reward;
            OnChangeRewards?.Invoke(_currentReward);
        }

        public void ResetCurrentReward()
        {
            _currentReward = 0;
            OnChangeRewards?.Invoke(_currentReward);
        }
    }
}