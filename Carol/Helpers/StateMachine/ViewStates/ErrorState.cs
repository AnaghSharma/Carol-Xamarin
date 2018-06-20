/*
 * Helper class corresponding to the error UI state
 * 
 * Author - Anagh Sharma
 * http://www.anaghsharma.com
 * 
 * 2018
 * 
 */

using AppKit;
using Carol.Views;

namespace Carol.Helpers.StateMachine.ViewStates
{
    public class ErrorState : StatefulViewController
    {
        public override void Enter()
        {
            ContainerView = LoadNib.LoadViewFromNib<ErrorView>("ErrorView", CurrentDelegate.controller.View);
            ContainerView.Frame = CurrentDelegate.controller.View.Bounds;
            CurrentDelegate.controller.View.AddSubview(ContainerView, NSWindowOrderingMode.Above, CurrentDelegate.controller.View);
        }

        public override void Exit()
        {
            ContainerView.RemoveFromSuperview();
        }
    }
}
