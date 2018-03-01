using System;
namespace Carol.Helpers.StateMachine
{
    public enum States
    {
        Idle,
        Loading,
        Content,
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

        public ViewStateMachine()
        {
        }
    }
}
