# VibeGunner - Zombie Survival Shooter

A 2D zombie survival shooter built with Godot Engine and C#. Fight endless waves of zombies with mouse-aimed projectiles. Features progressive difficulty, death/restart system, and a professional landing page with settings management.

## Quick Start

1. **Prerequisites**: Godot Engine 4.x with C# support, .NET SDK
2. **Setup**: Copy scripts to project, add assets, set `MainMenu.tscn` as main scene
3. **Run**: Launch and use the main menu to start playing

## Controls

- **Arrow Keys**: Move player
- **Left Mouse**: Shoot toward cursor
- **Menu Navigation**: Use buttons to navigate between main menu, settings, and game

## Features

- **Landing Page**: Professional main menu with PLAY, SETTINGS, and EXIT
- **Settings System**: Customize player speed, zombie spawns, projectile speed, and more (Under Construction)
- **Progressive Difficulty**: Zombie spawn rate increases over time
- **Death System**: One-hit death with restart and main menu options
- **Settings Persistence**: All settings automatically saved between sessions

## File Structure

```
VibeGunner/
├── MainMenu.cs            # Landing page script
├── Settings.cs             # Settings menu script  
├── GameSettings.cs         # Global settings singleton
├── Player.cs               # Player character script
├── Projectile.cs           # Projectile behavior
├── Basic_Zombie.cs         # Zombie AI
├── ZombieSpawner.cs        # Spawning system
├── GameUI.cs               # UI and kill counter
├── *.tscn                  # Scene files
├── *.png                   # Sprites
└── README.md               # This file
```

## Customization

### In-Game Settings Menu
- Under Construction

## License

Open source - modify and distribute as needed. 