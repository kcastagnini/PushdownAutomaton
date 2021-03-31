namespace KarimCastagnini.Commons.PushdownAutomaton
{
    /// <summary>
    /// Helper class for defining transition tables and building automatons.
    /// </summary>
    public class AutomatonBuilder
    {
        private TransitionTable.Builder _tableBuilder = new TransitionTable.Builder();

        public TransitionCollection From(IState source, params IState[] sources)
            => _tableBuilder.From(source, sources);

        public TransitionCollection FromAny(params IState[] except)
            => _tableBuilder.FromAny(except);

        /// <summary>
        /// Build a Automaton using the previously defined transitions.
        /// </summary>
        /// <param name="initialState">The initial state of the created Automaton.</param>
        public Automaton Build(IState initialState)
            => new Automaton(initialState, _tableBuilder.Build(), new UnlimitedStack());

        /// <summary>
        /// Build a Automaton using the previously defined transitions.
        /// </summary>
        /// <param name="initialState">The initial state of the created Automaton.</param>
        /// <param name="stackCapacity">The maximum capacity of the stack of the created Automaton.</param>
        public Automaton Build(IState initialState, int stackCapacity)
            => new Automaton(initialState, _tableBuilder.Build(), new DropOutStack(stackCapacity));
    }
}