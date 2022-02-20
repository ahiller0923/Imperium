-> TestKnot

=== TestKnot ===
Hello!
I am a test NPC to see how well Ink works!
Please be nice to me!
    + [Um... No, you suck!]
        -> BadOutcome
    + [Ok, you are looking mighty handsome my friend!]
        -> GoodOutcome

=== BadOutcome ===
You sir are a most rude fellow! Begone with thee!
    + [I shall strike thee down for thoust are unholy!]
        -> Fight
    + [Oh ok]
        -> DONE

=== GoodOutcome ===
Your sir are a fine fellow!
-> DONE

=== Fight ===
Do your worst villain!#hostile
    -> END