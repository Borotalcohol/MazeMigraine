![Menu Cover](Project/Resources/MMCover.png)



# MazeMigraine
Maze Migraine is a simple 2D timed maze-escape game with random level generation.


## Context
Maze Migraine is the result of a project assigned by my IT teacher in the last year of high school.
The goal was to create a simple game using C# and WPF covering different aspects such as:
- Graphics
- Music & Audio
- Gameplay
- XML Serialization / File Handling
- Efficiency


## How does it work?
The way it works is that you can choose to generate a new random maze for each of the difficulty levels
(Easy 10x10, Normal 20x20, Difficult 30x30). Whenever you generate a new maze, it gets saved, so you and
other players can choose to replay that particular level.


## How do you generate maze levels?
There are plenty of ways you can [Generate a Maze](https://en.wikipedia.org/wiki/Maze_generation_algorithm).
The one I've decided to implement was the iterative version of the [Randomized Depth-First Search](https://en.wikipedia.org/wiki/Maze_generation_algorithm#Randomized_depth-first_search)

