using System;
using BallsFall.Model;
using BallsFall.View;

namespace BallsFall.Controller
{
    public class BallController
    {
        public event Action<DestroyModel> OnDestroy;
        private BallModel BallModel;
        private BallView BallView;

        public BallController(BallModel ballModel, BallView ballView)
        {
            BallModel = ballModel;
            BallView = ballView;
            
            BallView.OnDestroy += Destroy;

            BallView.Init(ballModel);
        }

        public void Destroy(StateDestroyebl stateDestroyebl)
        {
            OnDestroy?.Invoke(new DestroyModel( this, stateDestroyebl, BallModel));
        }
    }
}