# VibeGunner - Zombie Survival Shooter

A 2D zombie survival shooter game built with Godot Engine and C#. Control a circle-shaped player and fight off endless waves of zombies with your mouse-aimed projectiles.

## Description

This is a 2D zombie survival game where you control a circular player character that can move around the screen using arrow keys and shoot projectiles toward the mouse cursor with a left click. Survive as long as possible by eliminating the endless waves of zombies that spawn around you.

## Features

- **Player Movement**: Use arrow keys to move the circular player around the screen
- **Mouse-Aimed Shooting**: Click the left mouse button to shoot projectiles toward the cursor position
- **Zombie Enemies**: Red-tinted zombie enemies that slowly move toward the player
- **Endless Spawning**: Zombies continuously spawn around the player at regular intervals
- **Collision Detection**: Bullets destroy zombies on contact
- **Kill Counter**: Real-time display of zombies eliminated
- **Automatic Cleanup**: Projectiles automatically delete themselves when they go off-screen
- **Simple Visual Design**: Clean, minimalist graphics with distinct player and zombie sprites

## Prerequisites

- [Godot Engine](https://godotengine.org/) (version 3.x or 4.x)
- C# support enabled in Godot
- .NET SDK (for C# compilation)

## Setup Instructions

1. **Create a new Godot project** with C# support enabled
2. **Copy the C# scripts** (`Player.cs`, `Projectile.cs`, `Basic_Zombie.cs`, `ZombieSpawner.cs`, and `GameUI.cs`) into your project's scripts folder
3. **Add the required assets** (`cir_sprite.png`, `zomb_sprite.png`, `bullet.png`)
4. **Follow the setup steps** in `TODO_GODOT_GAME.txt` to configure the game in the Godot editor
5. **Run the game** and test the controls

## Controls

- **Arrow Keys**: Move the player (up, down, left, right)
- **Left Mouse Button**: Shoot projectile toward cursor position

## File Structure

```
VibeGunner/
├── Player.cs              # Player character script
├── Projectile.cs          # Projectile behavior script
├── Basic_Zombie.cs        # Zombie enemy AI and behavior
├── ZombieSpawner.cs       # Zombie spawning system
├── GameUI.cs              # User interface and kill counter
├── node_2d.tscn           # Main game scene
├── Basic_Zombie.tscn      # Zombie enemy scene
├── Projectile.tscn        # Projectile scene
├── cir_sprite.png         # Player sprite
├── zomb_sprite.png        # Zombie sprite
├── bullet.png             # Bullet sprite
├── TODO_GODOT_GAME.txt    # Setup instructions and development roadmap
└── README.md              # This file
```

## Game Mechanics

### Player
- Circular character that moves with arrow key input
- Shoots projectiles toward the mouse cursor position
- Configurable movement speed
- Tracks kill count for eliminated zombies

### Projectiles
- Spawn at the player's position
- Travel in the direction of the mouse cursor when fired
- Destroy zombies on contact
- Automatically delete when they leave the screen
- Configurable projectile speed

### Zombies
- Red-tinted enemies that slowly move toward the player
- Spawn at random positions around the player
- Have collision detection for bullet hits
- Destroyed when hit by projectiles
- Configurable movement speed and detection range

### Spawning System
- Zombies spawn every 3 seconds by default
- Spawn at a configurable distance from the player
- Spawn positions are randomized around the player
- Endless spawning for continuous gameplay

## Customization

You can easily modify the game by adjusting the exported variables in the scripts:

- **Player Speed**: Change the `Speed` variable in `Player.cs`
- **Projectile Speed**: Change the `Speed` variable in `Projectile.cs`
- **Zombie Speed**: Change the `Speed` variable in `Basic_Zombie.cs`
- **Zombie Detection Range**: Adjust the `DetectionRange` variable in `Basic_Zombie.cs`
- **Spawn Rate**: Modify the `SpawnInterval` variable in `ZombieSpawner.cs`
- **Spawn Distance**: Change the `SpawnDistance` variable in `ZombieSpawner.cs`
- **Visual Design**: Add sprites or custom drawing methods to make the characters look different

## Development Notes

This is a zombie survival shooter implementation that can be extended with:
- Different zombie types (fast, armored, ranged, etc.)
- Progressive difficulty scaling
- Player health system and damage
- Sound effects and music
- Particle effects for kills and impacts
- High score system
- Multiple weapon types and power-ups
- Boss encounters
- Menu system and pause functionality
- Background graphics and visual effects

## License

This project is open source. Feel free to modify and distribute as needed.

## Contributing

Feel free to fork this project and add your own features or improvements! 