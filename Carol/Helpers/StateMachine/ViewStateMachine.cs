using System;
using Carol.Helpers.StateMachine.ViewStates;

namespace Carol.Helpers.StateMachine
{
    public enum States
    {
        Idle,
        Loading,
        Content,
        Empty,
        Error
    }

    public enum Triggers
    {
        Load,
        ShowContent,
        ShowEmpty,
        ThrowError
    }

    public class ViewStateMachine
    {
        public StatefulViewController currentState;

        public ViewStateMachine(States initialState)
        {
            if (initialState == States.Idle)
                currentState = new IdleState();
        }

        public void SetupInitialView()
        {
            currentState.Handle(this);
        }
    }
}
