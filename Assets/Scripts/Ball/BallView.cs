using System;
using BallsFall.Model;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BallsFall.View
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Fall))]
    public class BallView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject fxPrefab;
        [SerializeField] private GameObject scorePrefab;
        public event Action<StateDestroyebl> OnDestroy;
        private SpriteRenderer spriteRenderer;
        private Fall fall;

        public void Init(BallModel model)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            fall = GetComponent<Fall>();
            Settings(model);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Slammed();
        }

        public void SelfDestruction()
        {
            OnDestroy?.Invoke(StateDestroyebl.selfDestruction);
            CreateFx();
            DestroyThis();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var destructionProvider = other.GetComponent<DestructionProvider>();
            if (destructionProvider != null)
            {
                OnDestroy?.Invoke(StateDestroyebl.damage);
                DestroyThis();
            }
        }

        private void Settings(BallModel ballModel)
        {
            fall.speed = ballModel.Speed;
            spriteRenderer.color = ballModel.Color;
            _ballModel = ballModel;
        }

        private BallModel _ballModel;

        private void Slammed()
        {
            OnDestroy?.Invoke(StateDestroyebl.reward);
            CreateFx();
            CreateScoreFx(_ballModel.Reward);
            DestroyThis();
        }

        private void CreateScoreFx(int ballModelReward)
        {
            var scoreFx = Instantiate(scorePrefab, transform.position, quaternion.identity);
            scoreFx.GetComponent<UIScore>().score = ballModelReward.ToString();
        }

        private void CreateFx()
        {
            var fxClon = Instantiate(fxPrefab, transform.position, quaternion.identity);
            var cont = fxClon.GetComponent<ParticleFx>();

            cont.Init(_ballModel.Color);
        }

        private void DestroyThis()
        {
            Destroy(gameObject);
        }
    }
}