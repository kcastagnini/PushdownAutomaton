using System;
using System.Collections.Generic;

namespace KarimCastagnini.Commons.PushdownAutomaton
{
    public class Transition : ITransition
    {
        /// <summary>
        /// IsValid returns true if all of the guard functions return true
        /// or if there are no guard functions.
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach (var condition in _conditions)
                    if (condition != null && !condition())
                        return false;

                return true;
            }
        }

        public string Description { get; private set; } = "No description";

        private bool _hasTarget;
        private IState _target;
        private List<Func<bool>> _conditions = new List<Func<bool>>();
      
        public Transition(IState target, Func<bool> condition, params Func<bool>[] conditions)
        {
            _hasTarget = true;
            _target = target;
            AddConditions(condition, conditions);
        }

        public Transition(Func<bool> condition, params Func<bool>[] conditions)
        {
            _hasTarget = false;
            AddConditions(condition, conditions);       
        }

        public Transition(string description, IState target, Func<bool> condition, params Func<bool>[] conditions) : this(target, condition, conditions)
            => Description = description;

        public Transition(string description, Func<bool> condition, params Func<bool>[] conditions) : this(condition, conditions)
            => Description = description;

        public bool TryGetTarget(out IState result)
        {
            result = null;

            if (_hasTarget)
            {
                result = _target;
                return true;
            }

            return false;
        }

        private void AddConditions(Func<bool> condition, params Func<bool>[] conditions)
        {
            _conditions.Add(condition);

            for (int i = 0; i < conditions.Length; i++)
                _conditions.Add(conditions[i]);
        }

        //TODO, useful when printing a transition table
        //public override string ToString() => "To [target] when [condition description]";
    }
}