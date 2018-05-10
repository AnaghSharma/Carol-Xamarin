/*
 * Abstract base class for the different states of state machine
 * 
 * Author - Anagh Sharma
 * http://www.anaghsharma.com
 * 
 * 2018
 * 
 */

using AppKit;

namespace Carol.Helpers.StateMachine
{
    public abstract class StatefulViewController
    {
        public NSView ContainerView;
        public AppDelegate CurrentDelegate = NSApplication.SharedApplication.Delegate as AppDelegate;
        public abstract void Enter(ViewStateMachine stateMachine);
        public abstract void Exit(ViewStateMachine stateMachine);
    }
}
