# PushdownAutomaton
A lightweight and versatile implementation of a pushdown automaton in C#.

```c#
var builder = new PushdownAutomatonBuilder();

//add transitions from a source state to a target state with one or more predicates
builder.From(standing)
    .To(jumping, isGrounded, jumpRequested)
    .To(crouching, crouchRequested);

//add transitions from any source state to a target state with one or more predicates
builder.FromAny(firing)
    .To(firing, enemyInRange);

//add transitions from a source state to the previous state we were in with one or more predicates
builder.From(firing)
    .ToPrevious(enemyKilled);

//build an instance of a PDA with 'standing' as its initial state
PushdownAutomaton pda = builder.Build(standing);

//...

pda.Tick(); //call repeatedly to perform transitions when the appropriate predicates evaluate to true
```

Note: Even though this library was originally designed for Unity projects in mind, it contains no dependencies with the Unity Engine and it can be used in any .NET projects.
