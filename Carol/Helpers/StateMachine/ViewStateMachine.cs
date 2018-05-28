/*
 * Helper class which acts as a state machine to handle different UI cases
 * 
 * Author - Anagh Sharma
 * http://www.anaghsharma.com
 * 
 * 2018
 * 
 */

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
        private StatefulViewController currentState;
        
        public ViewStateMachine(States initialState)
        {
            if (initialState == States.Idle)
                currentState = new IdleState();
        }

        public void SetupInitialView() => currentState.Enter(this);

        //Basic idea is to transition to a particular state from current state whenever triggered
        //and then calling the Enter method which loads the required view from a .xib file (from 
		//Views folder) into the superview. Just before the transition we call the Exit method of
		//current state so that the previous view is removed from the superview.

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
        
		//These are the methods which are called from outside to load the required view. Outside world
        //does not know anything about the state machine or the states apart from these methods.

        //Loading method which can be called whenever a Loading View is required
        public void StartLoading()
        {
            TransitionToState(States.Idle, Triggers.Load);
            currentState.Enter(this);
        }

        //Method to load a view when there is content
        public void ShowContent()
        {
            TransitionToState(States.Loading, Triggers.ShowContent);
            currentState.Enter(this);
        }

        //Method to load a view when there is no content
        public void ShowEmpty()
        {
            TransitionToState(States.Loading, Triggers.ShowEmpty);
            currentState.Enter(this);
        }

        //Method to load a view whenever there is error
        public void ShowError()
        {
            TransitionToState(States.Loading, Triggers.ThrowError);
            currentState.Enter(this);
        }
    }
}
