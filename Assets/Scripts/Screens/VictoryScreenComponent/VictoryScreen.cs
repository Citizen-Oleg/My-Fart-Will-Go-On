using Assets.Scripts.Managers.ScreensManager;
using ResourceSystem;
using TMPro;
using UnityEngine;

namespace Screens.VictoryScreenComponent
{ 
    public class VictoryScreen : BaseScreenWithContext<ResultScreenContext>
    {
        [SerializeField]
        private ResultScreenAnimationController _resultScreenAnimationController;

        public override void ApplyContext(ResultScreenContext context)
        {
            _resultScreenAnimationController.PlayAnimation(context.Cash);
        }
    }
}