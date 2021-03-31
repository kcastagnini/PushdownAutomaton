using System.Collections.Generic;

namespace KarimCastagnini.Commons.PushdownAutomaton
{
    public class TransitionTable : ITransitionTable
    {
        private Dictionary<IState, List<ITransition>> _transitions =
            new Dictionary<IState, List<ITransition>>();
                     
        private TransitionTable() { }

        public bool TryGetTransitions(IState source, out IEnumerable<ITransition> transitions)
        {
            transitions = null;

            bool hasTransitions = _transitions.TryGetValue(source, out var t);

            if (hasTransitions)
                transitions = t;

            return hasTransitions;
        }

        /// <summary>
        /// Used to easily define a transition table through method chaining.
        /// </summary>
        public class Builder
        {
            private List<TransitionCollection> _collections = new List<TransitionCollection>();
            private HashSet<IState> _allStates = new HashSet<IState>();

            /// <summary>
            /// TODO
            /// </summary>
            public TransitionCollection From(IState source, params IState[] sources)
            {
                var transition = new TransitionCollection(new HashSet<IState>(sources) { source });
                _collections.Add(transition);
                return transition;
            }

            /// <summary>
            /// TODO
            /// </summary>
            public TransitionCollection FromAny(params IState[] except)
            {
                var transition = new TransitionCollection(_allStates, new HashSet<IState>(except));
                _collections.Add(transition);
                return transition;
            }

            /// <summary>
            /// TODO
            /// </summary>
            public TransitionTable Build()
            {
                //find all the submitted states (needed for AnyTransition)
                foreach (var t in _collections)
                    _allStates.UnionWith(t.States);

                var table = new TransitionTable();

                foreach (var collection in _collections)
                    foreach (var transition in collection)
                        Add(table, transition.Source, transition.Transition);

                return table;
            }

            private void Add(TransitionTable table, IState source, ITransition transition)
            {
                if (!table._transitions.TryGetValue(source, out var transitions))
                {
                    transitions = new List<ITransition>();
                    table._transitions.Add(source, transitions);
                }

                transitions.Add(transition);
            }
        }
    }
}