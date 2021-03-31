namespace KarimCastagnini.Commons.PushdownAutomaton
{
    public interface ITransition
    {
        /// <summary>
        /// True when the conditions associated to this transition are met.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Tries to get the target state of the transition.
        /// </summary>
        /// <param name="result">The resulting state target of the transition (if available).</param>
        /// <returns>True if this transition has an explicit state target.</returns>
        bool TryGetTarget(out IState result);
    }
}