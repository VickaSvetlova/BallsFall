using BallsFall.Controller;
using BallsFall.Model;
using BallsFall.View;

namespace BallsFall
{
    public enum StateDestroyebl
    {
        damage,
        reward,
        selfDestruction
    }
    public class DestroyModel
    {
        public BallController ballController { get; private set; }

        public StateDestroyebl stateDestroyable { get; private set; }
        public BallModel ballModel { get; private set; }

        public DestroyModel(BallController ballController, StateDestroyebl stateDestroyable, BallModel ballModel)
        {
            this.ballController = ballController;
            this.stateDestroyable = stateDestroyable;
            this.ballModel = ballModel;
        }
    }
}