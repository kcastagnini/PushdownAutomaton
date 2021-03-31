using System.Collections.Generic;

namespace KarimCastagnini.Commons.PushdownAutomaton
{
    /// <summary>
    /// Stores a map between source states and transitions.
    /// </summary>
    public interface ITransitionTable
    {
        /// <summary>
        /// Tries to get the transitions associated to a source state.
        /// </summary>
        /// <param name="source">The source state.</param>
        /// <param name="transitions">Resulting transitions.</param>
        /// <returns>True if there are transitions associated to the source state.</returns>
        bool TryGetTransitions(IState source, out IEnumerable<ITransition> transitions);
    }
}