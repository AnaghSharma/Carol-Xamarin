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

        public void SetupInitialView() => currentState.Enter(this);

        void TransitionToState(States _currentState, Triggers _trigger)
        {
            currentState.Exit(this);
            switch(_currentState)
            {
                case States.Idle:
                    if (_trigger == Triggers.Load)
                        currentState = new LoadingState();
                    break;
                case States.Loading:
                    if (_trigger == Triggers.ShowContent)
                        currentState = new ContentState();
                    else if (_trigger == Triggers.ShowEmpty)
                        currentState = new EmptyState();
                    else if (_trigger == Triggers.ThrowError)
                        currentState = new ErrorState();
                    break;
            }
        }

        public void StartLoading()
        {
            TransitionToState(States.Idle, Triggers.Load);
            currentState.Enter(this);
        }

        public void ShowContent()
        {
            TransitionToState(States.Loading, Triggers.ShowContent);
            currentState.Enter(this);
        }

        public void ShowEmpty()
        {
            TransitionToState(States.Loading, Triggers.ShowEmpty);
            currentState.Enter(this);
        }

        public void ShowError()
        {
            TransitionToState(States.Loading, Triggers.ThrowError);
            currentState.Enter(this);
        }
    }
}
