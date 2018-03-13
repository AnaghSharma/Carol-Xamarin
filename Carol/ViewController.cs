﻿﻿using System;
using AppKit;
using Carol.Helpers.StateMachine;
using Foundation;

namespace Carol
{
    public partial class ViewController : NSViewController
    {
        ViewStateMachine stateMachine;

        public ViewController(IntPtr handle) : base(handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.Appearance = new NSAppearance(@"Dark", null);
            stateMachine = new ViewStateMachine(States.Idle);
		}

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        public override void ViewDidAppear()
        {
            base.ViewDidAppear();

            stateMachine.SetupInitialView();
            stateMachine.StartLoading();
        }
    }
}
