VAR resolve = 0

-> ChooseSide

=== ChooseSide ===
Which side will you choose?
    + [I choose left!]
        ~resolve = 3
        -> LeftSider
    + [I choose right!]
        ~resolve = -3
        ->RightSider
        
=== RightSider ===
Those on the right appreciate this.
-> DONE

=== LeftSider ===
Those on the left appreciate this.
-> DONE