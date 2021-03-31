using System.Collections.Generic;

namespace KarimCastagnini.Commons.PushdownAutomaton
{
    /// <summary>
    /// Standard .NET stack compatible with Automaton
    /// </summary>
    public class UnlimitedStack : Stack<IState>, IStateStack { }
}