# PushdownAutomaton
A lightweight and versatile implementation of a pushdown automaton in C#.

```c#
var builder = new PushdownAutomatonBuilder();

//add transitions from 'standing' state...
builder.From(standing)
    .To(jumping, isGrounded, jumpRequested) //...to 'jumping' when 'isGrounded' and 'jumpRequested' evaluate to true
    .To(crouching, crouchRequested); //...to 'crouching' when 'crouchRequested' evaluates to true

//add transitions from any source state (except 'firing')...
builder.FromAny(firing)
    .To(firing, enemyInRange); //...to 'firing' when 'enemyInRange' evaluates to true

//add transitions from 'firing' state...
builder.From(firing)
    .ToPrevious(enemyKilled); //...to the previous state we were in when 'enemyKilled' evaluates to true

//build an instance of a PDA with 'standing' as its initial state
PushdownAutomaton pda = builder.Build(standing);

//...

pda.Tick(); //call repeatedly to perform transitions when the defined predicates evaluate to true 
```

Note: Even though this library was originally designed for Unity projects in mind, it contains no dependencies with the Unity Engine and it can be used in any .NET projects.
