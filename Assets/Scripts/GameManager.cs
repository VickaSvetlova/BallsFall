using BallsFall.Settings;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BallsFall
{
    enum GameState
    {
        intro,
        game,
        fail,
        reset,
        pause,
        replay,
        pocced
    }

    [RequireComponent(typeof(UIController))]
    [RequireComponent(typeof(BallsCreator))]
    [RequireComponent(typeof(EventSystem))]
    [RequireComponent(typeof(RepositoryAwards))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(ProgressTimer))]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameSetting _gameSetting;
        private UIController _uiController;
        private BallsCreator _ballsCreator;
        private RepositoryAwards _repositoryAwards;
        private Health _health;
        private ProgressTimer _progressTimer;

        private void Awake()
        {
            _ballsCreator = GetComponent<BallsCreator>();
            _uiController = GetComponent<UIController>();
            _repositoryAwards = GetComponent<RepositoryAwards>();
            _health = GetComponent<Health>();
            _progressTimer = GetComponent<ProgressTimer>();
            Subscriptions();
        }

        private void Subscriptions()
        {
            _ballsCreator.OnDestroyBall += DataDestroySelector;
            _health.OnChangeHealth += (health) => { _uiController.SetHpBar(new Vector2(health.Item1, health.Item2)); };
            _repositoryAwards.OnChangeRewards += _uiController.SetReward;
            _health.OnDead += () => { SetState(GameState.fail); };
            _gameSetting.OnSpeedUp += _uiController.SpeedUp;

            _uiController.OnStartClick += () => { SetState(GameState.game); };
            _uiController.OnResetClick += () => { SetState(GameState.reset); };
            _uiController.OnReplayClick += () => { SetState(GameState.replay); };
            _uiController.OnPauseClick += () => { SetState(GameState.pause); };
            _uiController.OnPoceedClick += () => { SetState(GameState.pocced); };
        }

        private void Start()
        {
            SetState(GameState.intro);
        }

        private void SetState(GameState state)
        {
            switch (state)
            {
                case GameState.intro:
                    _uiController.StartIntro(true);
                    _health.SetHealth((_gameSetting.playerHealth, _gameSetting.playerHealth));
                    break;
                case GameState.game:
                    Time.timeScale = 1;
                    _ballsCreator.Enable();
                    _progressTimer.enable = true;
                    break;
                case GameState.fail:
                    _ballsCreator.Disable();
                    _uiController.StartFail(_repositoryAwards.GetReward(), true);
                    _ballsCreator.DestroyAllBall();
                    _progressTimer.enable = false;
                    break;
                case GameState.reset:
                    _repositoryAwards.ResetCurrentReward();
                    _progressTimer.Reset();
                    Time.timeScale = 1;
                    SetState(GameState.game);
                    break;
                case GameState.pause:
                    Time.timeScale = 0;
                    break;
                case GameState.replay:
                    Time.timeScale = 1;
                    _ballsCreator.DestroyAllBall();
                    SetState(GameState.reset);
                    break;
                case GameState.pocced:
                    Time.timeScale = 1;
                    break;
            }
        }

        private void DataDestroySelector(DestroyModel model)
        {
            switch (model.stateDestroyable)
            {
                case StateDestroyebl.damage:
                    _health.ExtactLife(model.ballModel.Damage);
                    break;
                case StateDestroyebl.reward:
                    _repositoryAwards.SetReward(model.ballModel.Reward);
                    break;
            }
        }
    }
}