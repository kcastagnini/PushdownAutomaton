namespace KarimCastagnini.Commons.PushdownAutomaton
{
    /// <summary>
    /// Defines functionalities for a state
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Called when entering the state
        /// </summary>
        void OnEntry();

        /// <summary>
        /// Called when exiting the state
        /// </summary>
        void OnExit();

        /// <summary>
        /// Called to update the state
        /// </summary>
        void OnTick();
    }
}