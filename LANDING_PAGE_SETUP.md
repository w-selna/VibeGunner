# VibeGunner Landing Page System

This document explains the new landing page system that has been added to VibeGunner, providing a professional menu interface with settings management.

## Overview

The landing page system consists of three main components:
1. **Main Menu** - The initial landing page with Play and Settings buttons
2. **Settings Menu** - A comprehensive settings interface for game customization
3. **Game Settings Manager** - A global singleton that handles settings persistence

## New Files Created

### Scripts
- `MainMenu.cs` - Handles the landing page UI and scene transitions
- `Settings.cs` - Manages the settings menu with various controls
- `GameSettings.cs` - Global settings singleton for data persistence

### Scenes
- `MainMenu.tscn` - The landing page scene
- `Settings.tscn` - The settings menu scene

## Features

### Main Menu (Landing Page)
- **Retro-futuristic logo** displaying "VIBE GUNNER"
- **Play Button** - Starts the game with smooth transition
- **Settings Button** - Opens the settings menu
- **Same background** as the game for visual consistency
- **Smooth transitions** between scenes

### Settings Menu
- **Player Settings**
  - Player Speed (50-300)
  - Player Name input field
- **Gameplay Settings**
  - Zombie Spawn Rate (0.5-10.0 seconds)
  - Spawn Distance (100-800 pixels)
  - Projectile Speed (200-800)
  - Zombie Speed (20-150)
- **Audio Settings**
  - Sound Effects toggle
  - Background Music toggle
- **Reset Button** (top right) - Restores default values
- **Back & Save Button** (bottom right) - Saves settings and returns to main menu

### Game Over Screen Updates
- **Main Menu Button** (top left) - Returns to landing page
- **Start New Round Button** - Restarts the current game
- Both buttons reset all game metrics and state

## Setup Instructions

### 1. Project Configuration
The `project.godot` file has been updated with:
- Main scene changed to `MainMenu.tscn`
- `GameSettings` added as an autoload singleton

### 2. Scene Hierarchy
```
MainMenu.tscn (Landing Page)
├── MainMenu.cs
└── Background, Title, Buttons

Settings.tscn (Settings Menu)
├── Settings.cs
└── Various Settings Controls

node_2d.tscn (Game Scene)
├── Player.cs (updated with main menu button)
├── ZombieSpawner.cs
└── Other game components
```

### 3. Settings Integration
The `GameSettings` singleton automatically:
- Loads saved settings on startup
- Saves settings when modified
- Applies settings to game objects
- Persists data between game sessions

## How It Works

### Scene Transitions
1. **Main Menu → Game**: Loads `node_2d.tscn` with fade transition
2. **Main Menu → Settings**: Loads `Settings.tscn` with fade transition
3. **Settings → Main Menu**: Saves settings and returns with fade transition
4. **Game Over → Main Menu**: Resets game state and returns to landing page

### Settings Persistence
- Settings are saved to `user://game_settings.cfg`
- Default values are restored when reset button is pressed
- Settings are automatically loaded when the game starts

### Game State Management
- Going to main menu from game over resets all metrics
- Game restart preserves current settings
- Settings changes take effect immediately when applied

## Customization

### Adding New Settings
1. Add the setting property to `GameSettings.cs`
2. Add the UI control in `Settings.cs`
3. Update the save/load methods
4. Apply the setting in the game logic

### Modifying UI Layout
- Edit the positioning in the respective `.cs` files
- Adjust colors, sizes, and spacing as needed
- The UI is built programmatically for easy modification

### Changing Default Values
- Update the `DEFAULT_*` constants in `Settings.cs`
- Update the corresponding values in `GameSettings.cs`
- Ensure game components use these default values

## Troubleshooting

### Common Issues
1. **Scene not found errors**: Ensure all `.tscn` files are in the project root
2. **Settings not saving**: Check that `GameSettings` is properly loaded as autoload
3. **UI not displaying**: Verify that the scripts are attached to the correct scene nodes

### Debug Information
All major operations log to the console with `[ComponentName]` prefixes for easy debugging.

## Future Enhancements

Potential improvements for the landing page system:
- Background music and sound effects
- Animated UI elements
- Save game slots
- Achievements display
- Credits and about pages
- Language localization support
