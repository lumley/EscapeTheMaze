# Escape the Maze

Welcome to Escape the Maze! This is simply a testing project, there is no aim for releasing this product to release, just to learn some basics about Unity3D.

We are not accepting pull requests at the moment, since we are learning even the most basics of C#. In any case, if you are curious enough, feel free to check out the goals of this project and fork it if you want (but remember the [license section](#license)!).



## Game Aim
The game consists of a player that wants to escape a room that is set in a labyrinth fashion

### Player
 * This is the main character of the game, it is controlled by the user (See  [controls section](#controls))
 * A player has a maximum amount of health
 * A player can attack enemies
 * A player can equip consumable items (e.g. Life potion)
 * A player can equip weapons
 * A player can equip storyline items (e.g. keys for opening doors)
 * A player can carry gold

### Winning conditions
 * The player touches the ending point

### Loosing conditions
 * The player looses all of his health

### Map
 * A map should be based on a grid system, where a single cell can have a walking space or not
 * A map could have different floors, accessible through stairs or ramps
 * A map should consist of a starting point and an ending point
 * An ending point should always be accessible from a starting point (ignoring scene doodads or obstacles)

### Characters
 * There should be enemies that wander randomly in the dungeon
 * When an enemy is hit, he should stop moving for a very short period
 * When the enemy collides with the player, the player should suffer some damage

### Camera
 * The game should be set in first person, so the body of the player's body is not visible to the camera.

### Controls
 * A player should be controlled using a keyboard and a mouse
 * The mouse should control the player's horizontal orientation
 * The keyboard should control the player's movement
    * 'W' should move forwards, to the tile that is closest to 0° from the character's orientation
    * 'A' should strafe left, to the tile that is closest to -90° from the character's orientation
    * 'S' should move backwards, to the tile that is closest to 180° from the character's orientation
    * 'D' should strafe right, to the tile that is closest to 90° from the character's orientation

## License

> The MIT License (MIT)

> Copyright (c) 2015 lumley, PixelBumper

> Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

> The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

