namespace KarimCastagnini.Commons.PushdownAutomaton
{
    /// <summary>
    /// Defines the basic operations needed for a Automaton to store and retrieve states
    /// </summary>
    public interface IStateStack
    {
        /// <summary>
        /// Number of states stored
        /// </summary>
        int Count { get; }
        IState Peek();
        IState Pop();
        void Push(IState state);
        void Clear();
    }
}