# BobC# (Beta)
A high-level C# library for computing methods, touches, extents and other bellringing computations.


## !! Important !!
Internally, BobC# references all bells and places as indices starting at zero.

Therefore, the Treble is bell **#0**, the two is bell **#1**, the three is bell **#2**, etc.

Likewise, firsts place/leading is place **#0**, seconds place is place **#1**, thirds place is place **#2**, etc.

When the library converts anything to a string, it looks at these indices in a customisable string `Constants.bell_names`, which defaults to `"1234567890ETABCDFGHIJKLMNOPQRSUVWXYZ"`.

And so bell **#0** (the Treble) comes out as `"1"`, bell **#1** (the two) comes out as `"2"`, bell **#2** (the three) comes out as `"3"`.

## Quickstart
First things first, import the library to your code: 
```C#
using Bob;
```



### Changes and Place Notations
Let's create a new change:
```C#
Change change = new Change ("13524");

int third_bell = change [2]; // ==> 4 (because this treble is bell #0, bell #4 is the 5)
Parity parity = change.parity; // ==> Parity.Odd
int order = change.order; // ==> 4
```

. . . or some place notation:
```C#
PlaceNotation notation = new PlaceNotation ("145", Stage.Doubles);
```

Implicit places are automatically filled in:
```C#
PlaceNotation lazy_notation = new PlaceNotation ("4", Stage.Doubles); ==> 145
```

Stages work up to twenty-two:
```C#
Change rounds = Change.Rounds (Stage.Doubles); // ==> 12345
Change big_rounds = Change.Rounds (Stage.Nonuples); // ==> 1234567890ETABCDFGH
Change bigger_rounds = Change.Rounds (Stage.TwentyTwo); // ==> 1234567890ETABCDFGHIJK
```

Suppose you wanted to transpose a change by another change, or by some place notation:
```C#
Change plain = new Change ("15738264"); // Cambridge Major's lead end
Change bob = new Change ("13578264"); // Cambridge Major's lead end after a bob

Change plain_then_bob = plain * bob; // ==> 17864523
Change bob_then_plain = bob * plain; // ==> 18654327

PlaceNotation notation = new PlaceNotation ("14", Stage.Major);
Change transposed_change = rounds * notation; // ==> 17532846
```



### Methods
Let's create a new method from its place notation
```C#
Method plain_bob_major = new Method ("Plain", Classification.Bob, Stage.Major, "x18x18x18x18,12");
```

You don't even need to specify the classification, and BobC# will classify it for you
```C#
Method cambridge_surprise_minor = new Method ("Cambridge", "x3x4x2x3x4x5,2", Stage.Minor);

Classification classification = cambridge_surprise_minor.classification; // ==> Classification.Surprise
```

Standard bobs and singles and plain calls are created automagically.

**(TODO)** You can get any method from the CCCBR's method library (also comes with the standard calls, even for awkward methods like Stedman and Erin):
```C#
Method stedman_triples = Method.GetMethod ("Stedman Triples");
```





### Touches
Any piece of ringing is a **Touch** object:
```C#
Touch touch = new Touch ();
```

Let's suppose we wanted to look at a plain course of Plain Bob Doubles ('cos we we've all been there at some point):
```C#
Touch plain_course = Method.GetMethod ("Plain Bob Doubles").plain_course;

Change change_no_3 = plain_course [2]; // ==> 24153

int length = plain_course.length; // ==> 40
bool is_true = plain_course.is_true; // ==> true
bool is_extent = plain_course.is_extent; // ==> false
```

. . . or a basic 120 of plain bob doubles:
```C#
Touch touch = Method.GetMethod ("Plain Bob Doubles").TouchFromCallList ("MMMB");

int length = touch.length; // ==> 120
bool is_true = touch.is_true; // ==> true
bool is_extent = touch.is_extent; // ==> true
```

. . . or even a peal (composition #1068 by Don Morrison):
```C#
Touch touch = Method.GetMethod ("Plain Bob Triples").TouchFromCallingPositions ("OHHH sWHHH WFHHH IH");

int length = touch.length; // ==> 5040
bool is_true = touch.is_true; // ==> true
bool is_extent = touch.is_extent; // ==> true
```
