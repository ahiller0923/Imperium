VAR evil = false
-> MAIN

=== MAIN ===
Serve me or perish mortal.
    + [What would you have me do?]
        -> MoreInfo
    + [Not a chance demon!]
        -> Rejection
        
=== MoreInfo ===
Kill! Kill them all!
    + [As you wish.]
        -> EvilMode
    + [You foul demon, I will strike you down for the good of all!]
        -> Rejection
        
=== EvilMode ===
Wise choice puny mortal.
~evil = true
    -> END
    
=== Rejection ===
You will pay dearly for that worhtless scum!
    -> END