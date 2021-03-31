using System;

namespace KarimCastagnini.Commons.PushdownAutomaton
{
    /// <summary>
    /// A LIFO data structure using a ring buffer/circular array.
    /// When the client will try to push a state to a already full DropOutStack,
    /// the new state will replace the less recently added state.
    /// </summary>
    public class DropOutStack : IStateStack
    {
        public int Count { get; private set; }

        private IState[] _states;
        private int _topIndex;

        /// <summary>
        /// Construct a DropOutStack.
        /// </summary>
        /// <param name="maxSize">Max number of states that can be stored before starting
        /// to replace the older ones.</param>
        public DropOutStack(int maxSize)
        {
            _states = new IState[Math.Max(0, maxSize)];
            Count = 0;
            _topIndex = 0;
        }
              
        public void Push(IState state)
        {
            if (_states.Length == 0)
                return;

            _states[_topIndex] = state;
            _topIndex = (_topIndex + 1) % _states.Length;

            if (Count != _states.Length)
                Count++;
        }

        public IState Pop()
        {
            if (Count == 0)
                throw new Exception("Trying to pop but DropOutStack is empty.");

            _topIndex = (_topIndex + _states.Length - 1) % _states.Length;
            IState result = _states[_topIndex];

            Count--;
            return result;
        }

        public IState Peek()
        {
            if (Count == 0)
                throw new Exception("Trying to peek but DropOutStack is empty.");

            return _states[_topIndex - 1];
        }

        public void Clear() => _topIndex = 0;
    }
}