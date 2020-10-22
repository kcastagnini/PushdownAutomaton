# PushdownAutomaton
A lightweight and versatile implementation of a pushdown automaton in C#.

```c#
var builder = new PushdownAutomatonBuilder();

//add transitions from a source to a target state when the predicates are true 
builder.From(standing)
    .To(jumping, isGrounded, jumpRequested)
    .To(crouching, crouchRequested);

//add transitions from any source state to a target state when the predicates are true 
builder.FromAny()
    .To(firing, enemyInRange);

//add transitions from a source state to the previous state we were in
builder.From(firing)
    .ToPrevious(enemyKilled);

//build an instance of a PDA with 'standing' as its initial state
PushdownAutomaton pda = builder.Build(standing);

//...

pda.Tick(); //call repeatedly to evalute predicates and perform transitions 
```

Note: Even though this library was originally designed for Unity projects it contains no dependencies with the Unity Engine and it can be used in any .NET projects.
