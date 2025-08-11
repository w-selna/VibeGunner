# VibeGunner - Zombie Survival Shooter

A 2D zombie survival shooter game built with Godot Engine and C#. Control a circle-shaped player and fight off endless waves of zombies with your mouse-aimed projectiles. Features progressive difficulty scaling and a complete death/restart system.

## Description

This is a 2D zombie survival game where you control a circular player character that can move around the screen using arrow keys and shoot projectiles toward the mouse cursor with a left click. Survive as long as possible by eliminating the endless waves of zombies that spawn around you. The game features progressive difficulty with increasing spawn rates and realistic zombie movement patterns.

## Features

- **Player Movement**: Use arrow keys to move the circular player around the screen
- **Mouse-Aimed Shooting**: Click the left mouse button to shoot projectiles toward the cursor position
- **Zombie Enemies**: Red-tinted zombie enemies that move toward the player with realistic jitter movement
- **Progressive Difficulty**: Zombie spawn rate increases linearly over time for escalating challenge
- **Endless Spawning**: Zombies continuously spawn around the player with configurable spawn intervals
- **Smart Spawning**: Zombies spawn at random distances with variation to prevent predictable patterns
- **Collision Detection**: Bullets destroy zombies on contact, player has hitbox for zombie detection
- **Death System**: Player dies in one hit from zombies, triggering a game over screen
- **Game Over Screen**: Displays final kill count with a restart button for new rounds
- **Kill Counter**: Real-time display of zombies eliminated during gameplay
- **Automatic Cleanup**: Projectiles and zombies are automatically removed when off-screen or destroyed
- **Simple Visual Design**: Clean, minimalist graphics with distinct player and zombie sprites
- **Restart Functionality**: Complete game reset with proper spawn rate reset and cleanup

## Prerequisites

- [Godot Engine](https://godotengine.org/) (version 4.x recommended)
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
- **Restart Button**: Click to start a new round after death

## File Structure

```
VibeGunner/
├── Player.cs              # Player character script with death system and restart logic
├── Projectile.cs          # Projectile behavior script with automatic cleanup
├── Basic_Zombie.cs        # Zombie enemy AI with jitter movement and collision
├── ZombieSpawner.cs       # Zombie spawning system with progressive difficulty
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
- Configurable movement speed and health system
- Tracks kill count for eliminated zombies
- **NEW**: Has a hitbox that detects zombie collisions
- **NEW**: Dies in one hit from zombies
- **NEW**: Spawns in the center of the screen at game start and after restart

### Projectiles
- Spawn at the player's position
- Travel in the direction of the mouse cursor when fired
- Destroy zombies on contact
- Automatically delete when they leave the screen
- Configurable projectile speed
- **NEW**: Added to "projectile" group for easy cleanup during game reset

### Zombies
- Red-tinted enemies that move toward the player
- **NEW**: Realistic movement with configurable jitter for more natural behavior
- **NEW**: Jitter strength and change interval can be adjusted
- Spawn at random positions around the player with distance variation
- Have collision detection for bullet hits and player contact
- Destroyed when hit by projectiles
- Configurable movement speed and detection range
- **NEW**: Added to "zombie" group for easy cleanup during game reset

### Spawning System
- **NEW**: Progressive difficulty with spawn rate that increases linearly over time
- **NEW**: Configurable spawn rate increase and minimum spawn interval
- **NEW**: Random spawn distance variation to prevent predictable patterns
- **NEW**: Minimum and maximum spawn distances to ensure off-screen spawning
- Zombies spawn at configurable intervals with distance from the player
- Spawn positions are randomized around the player
- Endless spawning for continuous gameplay
- **NEW**: Complete reset functionality for new rounds

### Death and Restart System
- **NEW**: Player dies immediately upon contact with any zombie
- **NEW**: Game over screen displays final kill count prominently
- **NEW**: Restart button allows starting a new round
- **NEW**: Complete game state reset including:
  - Player health and position reset
  - All zombies and projectiles removed
  - Spawn rate reset to initial values
  - Kill counter reset to zero
- **NEW**: Proper pause/unpause management during death screen

## Customization

You can easily modify the game by adjusting the exported variables in the scripts:

- **Player Speed**: Change the `Speed` variable in `Player.cs`
- **Player Health**: Modify the `MaxHealth` variable in `Player.cs`
- **Projectile Speed**: Change the `Speed` variable in `Projectile.cs`
- **Zombie Speed**: Change the `Speed` variable in `Basic_Zombie.cs`
- **Zombie Jitter**: Adjust `JitterStrength` and `JitterChangeInterval` in `Basic_Zombie.cs`
- **Zombie Detection Range**: Adjust the `DetectionRange` variable in `Basic_Zombie.cs`
- **Spawn Rate**: Modify the `SpawnInterval` variable in `ZombieSpawner.cs`
- **Spawn Rate Increase**: Adjust `SpawnRateIncrease` for difficulty progression in `ZombieSpawner.cs`
- **Minimum Spawn Interval**: Set `MinSpawnInterval` to cap maximum difficulty in `ZombieSpawner.cs`
- **Spawn Distance**: Change the `SpawnDistance` variable in `ZombieSpawner.cs`
- **Spawn Distance Variation**: Adjust `SpawnDistanceVariation` for random spawn patterns in `ZombieSpawner.cs`
- **Visual Design**: Add sprites or custom drawing methods to make the characters look different

## Advanced Features

### Progressive Difficulty
The game automatically becomes more challenging over time:
- Zombie spawn rate decreases linearly based on elapsed time since last reset
- Configurable rate of increase for fine-tuned difficulty progression
- Minimum spawn interval prevents the game from becoming impossible

### Zombie Movement Realism
Zombies now have more realistic movement patterns:
- Configurable jitter strength adds natural movement variation
- Jitter direction changes at regular intervals for dynamic behavior
- Movement remains directed toward the player while appearing more organic

### Smart Spawning System
Enhanced spawning logic prevents predictable patterns:
- Random distance variation ensures zombies don't always spawn at the same distance
- Minimum and maximum spawn distances prevent spawning too close or too far
- Off-screen spawning ensures zombies always appear from outside the viewport

### Robust Game State Management
Complete game state management for seamless restarts:
- All game objects are properly grouped for easy cleanup
- Spawn rate and timers are completely reset between rounds
- Pause states are properly managed during death screens
- Player positioning is consistently centered on screen

## Development Notes

This is a zombie survival shooter implementation that can be extended with:
- Different zombie types (fast, armored, ranged, etc.)
- **NEW**: Progressive difficulty scaling (already implemented)
- **NEW**: Player health system and damage (already implemented)
- **NEW**: Death screen and restart functionality (already implemented)
- Sound effects and music
- Particle effects for kills and impacts
- High score system
- Multiple weapon types and power-ups
- Boss encounters
- Menu system and pause functionality
- Background graphics and visual effects
- **NEW**: Zombie movement patterns and AI improvements

## Recent Updates

### Latest Features Added:
- **Progressive Difficulty**: Spawn rate increases over time for escalating challenge
- **Zombie Movement Jitter**: Realistic movement patterns with configurable jitter
- **Death System**: One-hit death with hitbox collision detection
- **Game Over Screen**: Prominent kill count display with restart functionality
- **Smart Spawning**: Random distance variation and off-screen spawning
- **Complete Reset System**: Full game state reset between rounds
- **Enhanced Debugging**: Comprehensive logging for development and troubleshooting

## License

This project is open source. Feel free to modify and distribute as needed.

## Contributing

Feel free to fork this project and add your own features or improvements! 