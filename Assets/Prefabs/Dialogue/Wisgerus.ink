VAR resolve = 0
VAR agro = false

-> MAIN

=== MAIN ===
~resolve = 0
Hello there!
+ [Who are you?]
    -> WhoAmI
+ [Where am I?]
    -> LocationInfo
+ [Why do they call you Wisgarus the Ugly?]
    -> Ugly

=== WhoAmI ===
My name is Wisgarus! I am but a humble knight of the Sanctus Empire! Now, who are you may I ask? I pray you do not hail from Magia my friend!
+ [Magia? What is that?]
    -> MagiaExplained
+ [Ah but I do!]
    -> MagiaEncountered

=== MagiaExplained ===
They are most vile creatures who would defile the very sanctity of life by using the body as a playground for their wicked experiments!
+ [That sounds terrible!]
    ~resolve = 3
    Oh you have no idea my friend, the only thing worse than those nasty Magians are the undead wretches they have created!
    -> MAIN
+ [Ok...]
    -> MAIN

=== MagiaEncountered ===
You fiend! How dare you tresspass into our land! Leave or I will skewer that excuse for a heart with my Shivrian steel!
+ [(Yikes, my bad I guess..)]
    ~resolve = -3
    -> END
+ [Bring it on you levereter! (Did I really just say that?)]
    ~resolve = -5
    ~agro = true
    -> END
    
=== Ugly ===
Ugly?! Who called me Ugly?! I'll kill 'em!
+ [Oh, it's just.. your name.. never mind...]
    -> MAIN
+ [I did you fool, bring it on!]
    ~agro = true
    -> END
    
=== LocationInfo ===
This is Farnfoss, the western-most village in the entire Sanctus Empire! The village folk here primarily work to protect the border from any attacks.
    + [Who are you protecting against?]
        -> Enemies
    + [Sounds lame]
        ~resolve = -1
        -> MAIN
        
=== Enemies ===
Well we primarily defend against the undead which plague the Uada woods, however, we also stand in the way of any Magian advance.
    -> MAIN
