using System;
using System.Collections;
using System.Collections.Generic;

namespace KarimCastagnini.Commons.PushdownAutomaton
{
    /// <summary>
    /// Helper class for creating transitions using method chaining.
    /// </summary>
    public class TransitionCollection : IEnumerable<(IState Source, ITransition Transition)>
    {
        public ISet<IState> States
        {
            get
            {
                ISet<IState> states = new HashSet<IState>();

                //no well defined transitions.
                if (_transitions.Count == 0)
                    return states;

                states.UnionWith(_sources);
                states.UnionWith(_exclude);
                states.UnionWith(_targets);

                return states;
            }
        }

        private IList<ITransition> _transitions = new List<ITransition>();
        private ISet<IState> _targets = new HashSet<IState>();
        private ISet<IState> _exclude;
        private ISet<IState> _sources;

        public TransitionCollection(ISet<IState> sources)
            => Init(sources, new HashSet<IState>());

        public TransitionCollection(ISet<IState> sources, ISet<IState> exclude)
            => Init(sources, exclude);

        private void Init(ISet<IState> sources, ISet<IState> exclude)
        {
            _sources = sources ?? throw new ArgumentNullException();
            _exclude = exclude ?? throw new ArgumentNullException();
        }

        public TransitionCollection To(IState target, Func<bool> guardCondition, params Func<bool>[] guardConditions)
        {
            _targets.Add(target);
            _transitions.Add(new Transition(target, guardCondition, guardConditions));
            return this;
        }

        public TransitionCollection To(string transitionName, IState target, Func<bool> guardCondition, params Func<bool>[] guardConditions)
        {
            _targets.Add(target);
            _transitions.Add(new Transition(transitionName, target, guardCondition, guardConditions));
            return this;
        }

        public TransitionCollection ToPrevious(Func<bool> guardCondition, params Func<bool>[] guardConditions)
        {
            _transitions.Add(new Transition(guardCondition, guardConditions));
            return this;
        }

        public TransitionCollection ToPrevious(string transitionName, Func<bool> guardCondition, params Func<bool>[] guardConditions)
        {
            _transitions.Add(new Transition(transitionName, guardCondition, guardConditions));
            return this;
        }

        public IEnumerator<(IState Source, ITransition Transition)> GetEnumerator()
        {
            foreach (var sourceState in _sources)
                foreach (var transition in _transitions)
                    if (!_exclude.Contains(sourceState))
                        yield return (sourceState, transition);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}