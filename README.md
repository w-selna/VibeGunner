# Vibe Game - Circle Shooter

A simple 2D shooter game built with Godot Engine and C#. Control a circle-shaped player and shoot projectiles toward your mouse cursor.

## Description

This is a basic 2D game where you control a circular player character that can move around the screen using arrow keys and shoot projectiles toward the mouse cursor with a left click.

## Features

- **Player Movement**: Use arrow keys to move the circular player around the screen
- **Mouse-Aimed Shooting**: Click the left mouse button to shoot projectiles toward the cursor position
- **Automatic Cleanup**: Projectiles automatically delete themselves when they go off-screen
- **Simple Visual Design**: Clean, minimalist circle-based graphics

## Prerequisites

- [Godot Engine](https://godotengine.org/) (version 3.x or 4.x)
- C# support enabled in Godot
- .NET SDK (for C# compilation)

## Setup Instructions

1. **Create a new Godot project** with C# support enabled
2. **Copy the C# scripts** (`Player.cs` and `Projectile.cs`) into your project's scripts folder
3. **Follow the setup steps** in `TODO_GODOT_GAME.txt` to configure the game in the Godot editor
4. **Run the game** and test the controls

## Controls

- **Arrow Keys**: Move the player (up, down, left, right)
- **Left Mouse Button**: Shoot projectile toward cursor position

## File Structure

```
vibe-game/
├── Player.cs              # Player character script
├── Projectile.cs          # Projectile behavior script
├── TODO_GODOT_GAME.txt    # Setup instructions for Godot editor
└── README.md              # This file
```

## Game Mechanics

### Player
- Circular character that moves with arrow key input
- Shoots projectiles toward the mouse cursor position
- Configurable movement speed

### Projectiles
- Spawn at the player's position
- Travel in the direction of the mouse cursor when fired
- Automatically delete when they leave the screen
- Configurable projectile speed

## Customization

You can easily modify the game by adjusting the exported variables in the scripts:

- **Player Speed**: Change the `Speed` variable in `Player.cs`
- **Projectile Speed**: Change the `Speed` variable in `Projectile.cs`
- **Visual Design**: Add sprites or custom drawing methods to make the circles look different

## Development Notes

This is a basic implementation that can be extended with:
- Enemy spawning and AI
- Collision detection and damage system
- Sound effects and music
- Particle effects
- Score system
- Multiple weapon types
- Power-ups

## License

This project is open source. Feel free to modify and distribute as needed.

## Contributing

Feel free to fork this project and add your own features or improvements! 