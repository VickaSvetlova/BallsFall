using System;
using System.Collections;
using System.Collections.Generic;
using BallsFall.Controller;
using BallsFall.Model;
using BallsFall.Settings;
using BallsFall.View;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BallsFall
{
    public class BallsCreator : MonoBehaviour
    {
        public event Action<DestroyModel> OnDestroyBall;
        [SerializeField] private GameSetting gameSetting;
        [SerializeField] private GameObject prefabBall;
        private IEnumerator _spawnLoop;

        public void Enable()
        {
            _spawnLoop = Spawner();
            StartCoroutine(_spawnLoop);
        }

        public void Disable()
        {
            StopAllCoroutines();
        }


        private IEnumerator Spawner()
        {
            while (true)
            {
                yield return new WaitForSeconds(gameSetting.frequencyCreationBallsSeconds);
                CreateBall(new BallModel((int)
                    GetRandomRage(new Vector2(gameSetting.rangeRewardValuesMinMax.x,
                        gameSetting.rangeRewardValuesMinMax.y)),
                    GetRandomColor(),
                    GetRandomRage(new Vector2(gameSetting.rangeSpeedValuesMinMax.x + gameSetting.ProgressRuntime,
                        gameSetting.rangeSpeedValuesMinMax.y + gameSetting.ProgressRuntime)),
                    GetRandomRage(new Vector2(gameSetting.rangeDamageValuesMinMax.x,
                        gameSetting.rangeDamageValuesMinMax.x))));
            }
        }

        private void CreateBall(BallModel model)
        {
            var position =
                new Vector3(
                    GetRandomRage(new Vector2(gameSetting.screenBorder.leftBorder,
                        gameSetting.screenBorder.rightBorder)), gameSetting.screenBorder.upBorder,
                    gameSetting.screenBorder.downBorder);

            var ballClon = Instantiate(prefabBall, position, Quaternion.identity);

            ballClon.transform.localScale =
                new Vector3(gameSetting.ballSize, gameSetting.ballSize, gameSetting.ballSize);

            var ballController = new BallController(model, ballClon.GetComponent<BallView>());
            ballController.OnDestroy += ControllerDestroy;
        }

        private void ControllerDestroy(DestroyModel destroyModel)
        {
            OnDestroyBall?.Invoke(destroyModel);
            destroyModel.ballController.OnDestroy -= ControllerDestroy;
        }

        private float GetRandomRage(Vector2 rage)
        {
            return Random.Range(rage.x, rage.y);
        }

        private Color GetRandomColor()
        {
            return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

        public void DestroyAllBall()
        {
            var views = FindObjectsOfType<BallView>();
            if (views.Length > 0)
            {
                foreach (var view in views)
                {
                    view.SelfDestruction();
                }
            }
        }
    }
}