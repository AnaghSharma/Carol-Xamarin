using System;
using AppKit;

namespace Carol.Helpers.StateMachine
{
    public abstract class StatefulViewController
    {
        public NSView ContainerView;
        public AppDelegate CurrentDelegate = NSApplication.SharedApplication.Delegate as AppDelegate;
        public abstract void Handle(ViewStateMachine stateMachine);
    }
}
