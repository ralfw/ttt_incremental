# Solution design
Solution design refines the findings from the analysis phase so that implementation becomes pretty straightforward.

Also solution design slices requirements so finely that feedback can be gathered quickly. For that to be possible it focuses on a single interaction or even feature.

# Increment #1
Starting the application seems a natural starting point for incremental development.

The following figure shows the start interaction refined into fundamental features:

![](images/incr01.png)

Feature #1 is the dialog. Feature #2 is the functional unit representing the logic needed to produce the desired behavior.

The logic does not need any data to operate on. That's represented by the () on the data flow arrow pointingt to _Start_. But the logic delivers a _board_ to the dialog.

This _board_ contains the data to be displayed as a 2D matrix in a manner to make it most easy for the dialog to render it and still be flexible.

In order to determine the best data structure for _board_ thus the API behind the dialog needs to be taken into account. I choose it to be WinForms.

Since data binding is not that easy with WinForms, I guess a board could be an array of characters, e.g. _char[] board = new char[9]_ containing a ' ' or 'X' or 'O' for each field. Or it could be a string of a fixed length, e.g. "X   O   X ".

Or a board could be an array of enum values, e.g.

```
enum Fieldvalues {
 Empty,
 X,
 O
};

Fieldvalues[] board;
```

The char-based solution would leave more room for errors; a wrong char could end up in the array/string. So I go with the enum-based solution.

A 1D-array should be fine to pass into the dialog. It contains an element for each field indexed from 0..8. Fields are numbered from left top to right bottom in the dialog.

A 2D-array would be more specific for the current layout. But then it's also a tad more limiting the display. A 1D-array also makes it easier to address individual fields with just one index.

## Functions
The functions of the first increment can be derived from the above drawing:

```
class Interactions {
  public Board Start();
}

class Dialog {
  public void Display(Board board)
}
```

The classes they reside on are pretty obvious, I think. Also the additional data classes/types:

```
enum Fieldvalues {...} // see above

class Board {
  public Fieldvalues[] Fields;
}

```

This increment should only "prove" I can start the application and a board gets displayed. Since an empty board is not very exciting, I'll fill it with some random data; not much logic should be required for that.

# Increment #2
Since what happens upon startup is the same as what's expected to happen by starting a new game, the new game interaction is next.

![](images/incr02.png)

It's obvious, the dialog needs a button for that as depicted in the requirements sketch.

But there also needs to be a new functional unit _New game_ since each interaction is translated to a function (which in the diagram is represented by a "bubble").

To depict that upon starting the application the same is happening as when requesting a new game, the _Start_ interaction can be refined like this:

![](images/incr02b.png)

The arc beneath _Start_ denotes "refinement"; it's like a pinch gesture on a smartphone to zoom in on a map.

## Functions
A function needs to be set up for the interaction. It's reused as a feature function within the previous interaction function _Start()_.

But how to signal the user requested a new game? The dialog needs a way to trigger the interaction function. This is best done with an event:

```
class Interactions {
  public Board Start();
  public Board New_game();
}

class Dialog {
  public void Display(Board board)
  
  public event Action On_new_game_requested;
}
```

I'll keep the random board generation for this increment to make it easy for the user to see there is actually something happening when she presses the button.

# Increment #3
There is a feature missing from the interactions so far: a message. The players should be informed whose turn it is.

No new function is necessary for that, but instead I change the type of the data flowing to the dialog. So far it's only a board, but from now on it's a _Gamestate_:

```
class Gamestate {
	public Board Board;
	public string Message;
}
```

The message informs about the current player, a win, the end of game, or an error. There are only a couple of different messages, so they could be encoded as an enum. But I'll go for the simpler _string_.

# Increment #4
Except for the random board field occupation the first two interactions are finished, I'd say. Now on to the main interaction: a player making drawing, making his move, placing his symbol on one of the board fields.

This interation consists of several features. But before I turn to them I just want to insert the interaction itself into the design. It's another functional unit to be triggered by some user input on the dialog.

The data flowing from the dialog into this functional unit can be the coordinate of the field the current player wants to put his symbol into. A simple index in the range 0..8 will do, I guess.

And what's the output of this interaction function? Again a game state. Then no change to the dialog is necessary. The game state contains the updated board as well as a description of the game status.

![](images/incr04.png)

Initially making a move generates an empty board and places an X into the addressed field. To this end I'll remove the random field content generation in _New_game()_.

## Functions
Another function on class _Interactions_ is needed. And another event on the dialog to trigger it.

```
class Interactions {
  public Gamestate Start();
  public Gamestate New_game();
  public Gamestate Draw(int coordinate)
}

class Dialog {
  public void Display(Gamestate board)
  
  public event Action On_new_game_requested;
  public event Action<int> On_player_drew;
}
```

An _int_ for the coordinate leaves room for error. But I'll live with that ;-) Too bad C# does not support range types.