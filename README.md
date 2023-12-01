# GROD - Game Resource Overlay Dictionary

This is a special pair of dictionaries, a base and an overlay,
for holding key/value pairs which can be modified from the base
value. It is especially designed for games where progress can
be saved into a save file and restored later.

The base dictionary is filled with the starting values at the
beginning of a game, and the overlay holds all the changes as
the game is played.

Getting a value during the game will return the overlay value
if found, or else the base value if found, or "". Setting a
value during the game always writes to the overlay. No errors
are thrown if the key doesn't exist yet.

When saving the game progress, only the overlay values need to
be stored into a save file. When restoring a save file, the
overlay would be cleared and loaded with the saved values,
returning to the game state at the time of the save.

Set UseOverlay = false to load the base values, and then set
UseOverlay = true to play the game.

Keys are strings and cannot be null, empty, or only whitespace,
but all others are valid. Keys are case-sensitive! Use Keys()
to get a list of all keys in the base or overlay, with an
optional prefix to get a subset of keys in a group.

Values are strings. If value = null it is changed to "", but
no other modifications are performed. Other data types can be
converted to and from string values by the calling program.