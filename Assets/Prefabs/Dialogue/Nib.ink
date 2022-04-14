VAR resolve = 0
VAR agro = false

-> MAIN

=== MAIN ===
Greetings traveler! What brings you to the grand village of Magia?
+ [Magia?]
    -> MagiaExplained
+ [Murder!]
    -> Fight
    
=== MagiaExplained ===
Why only the greatest township east of the Taric Sea! Certainly much better than those skitterbrooks over in Sanctus, or whatever they may call their wretched civilization these days!
+ [What's the problem with Sanctus?]
    -> SanctusExplained
+ [You fool! The Sanctus Empire sends their best!]
    -> Fight
    
=== SanctusExplained ===
Why they would throw away the gifts we have been bequeathed from the gods and say it is in respect of those very deities! We have been given the power of life beyond death, yet they would throw it away at the slight prospect of failure!

+ [I do not understand why you would ignore such a power.]
    -> GoodEnd

+ [They make a good point. You should not mess with the balance of life and death.]
    -> BadEnd
    
=== GoodEnd ===
I am glad you see it as I do my friend. 
~ resolve = 3
-> END

=== BadEnd ===
That is.. truly unfortunate..
~ resolve = -3
-> END

=== Fight ===
Ah but it is not I who is the fool for you shall be the one who bites the dust! And at your corpse I shall bite my thumb!
~agro = true
-> END

