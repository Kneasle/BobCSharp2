# BobC# (Discontinued)
A high-level, high performance C# library for computing methods, touches, peals and other bellringing computations.



## Interesting News:
BobC# has passed 5040 lines of C# code (including whitespace, comments and documentation).




## !! Important !!
Internally, BobC# references all bells and places as indices starting at zero.

Therefore, the Treble is bell **#0**, the two is bell **#1**, the three is bell **#2**, etc.

Likewise, firsts place/leading is place **#0**, seconds place is place **#1**, thirds place is place **#2**, etc.

When the library converts anything to a string, it looks at these indices in a customisable string `Constants.bell_names`, which defaults to `"1234567890ETABCDFGHIJKLMNOPQRSUVWXYZ"`.

And so bell **#0** (the Treble) comes out as `"1"`, bell **#1** (the two) comes out as `"2"`, bell **#2** (the three) comes out as `"3"`.

So from the outside, everything works as expected.








## Table of Contents
- [Features List](#features-list)
  - [Finished Features](#finished-features)
  - [Features in Development](#features-in-development)
  - [Roadmapped Features](#roadmapped-features)
- [Quickstart](#quickstart)
  - [Changes and Place Notations](#changes-and-place-notations)
  - [Methods](#methods)
  - [Touches](#touches)









## Features List

### Finished Features:
- Touches with any calls are fully supported:
  - Fast touch computing engine, including customisable call logic (e.g. calling by bells leading before the treble, etc.).
  - Touch proving, including checking if a touch is:
    - true (i.e. no change repeated more than once).
    - an extent (i.e. every possible change repeated exactly once).
    - a multiple extent (e.g. 240 of Doubles)
    - a legitimate quarter peal (i.e. with some changes repeated no more than once more than any other).
  - Highly optimised function to generate extents of any method (though giving this any method on triples and above will probably roast your computer - anyone fancy a BBQ silicon sandwich?).
  - Shortcuts to generate touches of a single method by calling positions and from a list of calls.
  - Shortcuts to generate spliced touches from a list of methods rung at each successive lead end.
  - Touch.ToString () generates a string of the entire touch, showing all calls, changes, lead ends, change count and falseness.
  - Touches of called changes can also be computed.
  - BobC# will detect touches which never come round as soon as it is apparent, rather than blasting out millions of changes and running out of memory.

- Splicing is fully supported:
  - Splices don't have to happen on a lead end (e.g. splicing to Plain Hunt in half a course of Cambridge).
  - New methods don't have to start at a lead end (thus supporting half-lead splices, etc.).
  - Touches can splice between methods of different stages, e.g. spliced Triples and Major, or even Doubles and Major.

- Method-related features:
  - Methods can be loaded from the CCCBR Method Library by title or place notation, with the standard calls provided for awkward methods such as Stedman and Erin.
  - Standard calls are generated by default for every treble-hunt method, along with the calling positions up to Maximus. 
  - Custom calls (e.g. Extremes in Grandsire) can be added to any method, and used in touches.
  - Standard titles are generated by default, but can be overridden.
  - Methods can be automatically classified (including adding `Little` and `Differential` tags) if the classification is not set when constructing a method.

- Place notation:
  - Automatic conversion of a string of place notations (e.g. `x3x4x25x36x4x5x6x7,2` for Cambridge Major), including expanding lead-end symmetries.
  - Implicit places are always filled in automatically.
  - All common notations are supported (e.g. `LE` instead of `,` and `-` instead of `X`).  These alternatives are customisable.
  - Place notation conversions are not case sensitive.

- Transpositions:
  - Called changes can be generated and used as a transposition, and dictated by either:
    - Place called up
    - Bell called up
    - Bell called down
  - Shorthand `*` operator for transpositions of anything implement `ITransposable`.
  - `Change * PlaceNotation[]` transposes the given change by every PlaceNotation in turn.
  - Objects which implement `ITransposable` include:
    - `Change`
    - `PlaceNotation`
    - `CalledChange`

- Performance features:
  - BobC# uses lazy evaluation on all properties (so values are only calculated when they are requested).
  - Values are also stored so if a values is requested twice, the second call is basically instant.
  - The touch prover runs in O(n log (n)) time, which is (I think) the best possible complexity for truth checking.  If anyone knows of a faster method, please tell me and I'll implement it.

### Features in Development:
- A more efficient way to create peal compositions, using blocks of calls as variables.

### Roadmapped Features:
- Allow custom ruleoffs for methods like Stedman
- Code in symmetry computations.









## Quickstart
First things first, import the library to your code: 
```C#
using Bob;
```

### Changes and Place Notations
Let's create a new change:
```C#
Change change = new Change ("13524");

int third_bell = change [2]; // ==> 4 (place #2 is thirds place, and the 5 is bell #4)
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
Change transposed_change = plain * notation; // ==> 17532846
```



### Methods
Let's create a new method from its place notation
```C#
Method plain_bob_major = new Method ("x18x18x18x18,12", "Plain", Classification.Bob, Stage.Major);
```

You don't even need to specify the classification, and BobC# will classify it for you
```C#
Method cambridge_surprise_minor = new Method ("x3x4x2x3x4x5,2", "Cambridge", Stage.Minor);

Classification classification = cambridge_surprise_minor.classification; // ==> Classification.Surprise
```

Standard bobs and singles and plain calls are created automagically.

You can get any method from the CCCBR's method library (also comes with the standard calls for treble hunt methods).
```C#
Method single_oxford_bob_triples = Method.GetMethod ("Single Oxford Bob Triples");
```


### Touches
Any piece of ringing is a `Touch` object:
```C#
Touch touch = new Touch ();
```

Let's suppose we wanted to look at a plain course of Plain Bob Doubles ('cos we've all been there at some point):
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
