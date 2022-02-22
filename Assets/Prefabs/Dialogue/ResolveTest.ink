VAR resolve = 0

-> ChooseSide

=== ChooseSide ===
Which side will you choose?
    + [I choose left!]
        ~resolve = 1
        -> LeftSider
    + [I choose right!]
        ~resolve = -1
        ->RightSider
        
=== RightSider ===
Those on the right appreciate this.
-> DONE

=== LeftSider ===
Those on the left appreciate this.
-> DONE