/*
 * Helper class corresponding to the loading UI state
 * 
 * Author - Anagh Sharma
 * http://www.anaghsharma.com
 * 
 * 2018
 * 
 */

using System;
using AppKit;
using Carol.Views;

namespace Carol.Helpers.StateMachine.ViewStates
{
    public class LoadingState : StatefulViewController
    {
        public override void Enter(ViewStateMachine stateMachine)
        {
            ContainerView = LoadNib.LoadViewFromNib<LoadingView>("LoadingView", CurrentDelegate.controller.View);
            ContainerView.Frame = CurrentDelegate.controller.View.Bounds;
            CurrentDelegate.controller.View.AddSubview(ContainerView, NSWindowOrderingMode.Above, CurrentDelegate.controller.View);
        }

        public override void Exit(ViewStateMachine stateMachine) => ContainerView.RemoveFromSuperview();
    }
}
