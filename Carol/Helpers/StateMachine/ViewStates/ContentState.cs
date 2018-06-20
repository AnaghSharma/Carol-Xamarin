/*
 * Helper class corresponding to the content showing UI state
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
    public class ContentState : StatefulViewController
    {
        public override void Enter()
        {
            ContainerView = LoadNib.LoadViewFromNib<ContentView>("ContentView", CurrentDelegate.controller.View);
            ContainerView.Frame = CurrentDelegate.controller.View.Bounds;
            CurrentDelegate.controller.View.AddSubview(ContainerView, NSWindowOrderingMode.Above, CurrentDelegate.controller.View);
        }

        public override void Exit()
        {
            ContainerView.RemoveFromSuperview();
        }
    }
}
