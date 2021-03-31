using System;

namespace KarimCastagnini.Commons.PushdownAutomaton
{
    /// <summary>
    /// Models behaviour as transitions between a finite set of states.
    /// </summary>
    public class Automaton
    {
        /// <summary>
        /// Raised before transitioning from the current state to the next one.
        /// </summary>
        public event Action OnTransitioning = delegate { };

        /// <summary>
        /// Raised after transitioning from the current state to the next one.
        /// </summary>
        public event Action OnTransitioned = delegate { };

        /// <summary>
        /// The initial state of the Automaton
        /// </summary>
        public IState InitialState { get; private set; }

        /// <summary>
        /// The current state of the Automaton.
        /// </summary>
        public IState CurrentState { get; private set; }

        private ITransitionTable _table;
        private IStateStack _stack;

        /// <summary>
        /// Construct a Automaton.
        /// </summary>
        /// <param name="initialState">The initial state.</param>
        /// <param name="transitionFunction">Queried to get the transitions associated to a particular state.</param>
        /// <param name="stack">The stack that holds the history of previous states.</param>
        public Automaton(IState initialState, ITransitionTable transitionFunction, IStateStack stack)
        {
            InitialState = initialState ?? throw new ArgumentNullException("Invalid initialState reference.");
            _table = transitionFunction ?? throw new ArgumentNullException("Invalid transitionTable reference.");
            _stack = stack ?? throw new ArgumentNullException("Invalid stack reference.");
            Reset();
        }
       
        /// <summary>
        /// Set CurrentState back to InitialState and clear the stack.
        /// </summary>
        public void Reset()
        {
            CurrentState = InitialState;
            _stack.Clear();
        }

        /// <summary> 
        /// Try to perform a transition from CurrentState. Tick CurrentState.
        /// </summary>
        public void Tick()
        {
            if (TryTransit(CurrentState, out IState nextState))
                TransitTo(nextState);

            CurrentState.OnTick();
        }

        /// <summary> 
        /// Try to get the state we were in before the last transition.
        /// </summary>
        /// <param name="result">The previous state.</param>
        /// <returns>True if the stack isn't empty.</returns>
        public bool TryGetPreviousState(out IState result)
        {
            result = null;

            if (_stack.Count > 0)
            {
                result = _stack.Peek();
                return true;
            }

            return false;
        }

        private bool TryTransit(IState source, out IState target)
        {
            target = null;

            //no transitions found for source state
            if (!_table.TryGetTransitions(source, out var transitions))
                return false;

            foreach (ITransition t in transitions)
                if (t.IsValid)
                {                  
                    if (t.TryGetTarget(out target))
                    {
                        _stack.Push(source);
                        return true;
                    }
                    else if (_stack.Count != 0)
                    {
                        target = _stack.Pop();
                        return true;
                    }
                }

            return false;
        }
      
        private void TransitTo(IState nextState)
        {
            CurrentState.OnExit();
            OnTransitioning();
            CurrentState = nextState;
            OnTransitioned();
            CurrentState.OnEntry();
        }      
    }
}