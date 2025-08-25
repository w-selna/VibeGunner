using Godot;
using System;

public partial class GameSettings : Node
{
    // Singleton instance
    public static GameSettings Instance { get; private set; }
    
    // Player settings
    public float PlayerSpeed { get; set; } = 150f;
    public string PlayerName { get; set; } = "Player";
    
    // Gameplay settings
    public float SpawnInterval { get; set; } = 3.0f;
    public float SpawnDistance { get; set; } = 300f;
    public float ProjectileSpeed { get; set; } = 400f;
    public float ZombieSpeed { get; set; } = 50f;
    
    // Audio settings
    public bool SoundEnabled { get; set; } = true;
    public bool MusicEnabled { get; set; } = true;
    
    // Settings file path
    private const string SETTINGS_FILE = "user://game_settings.cfg";
    
    public override void _Ready()
    {
        // Ensure singleton pattern
        if (Instance == null)
        {
            Instance = this;
            ProcessMode = ProcessModeEnum.Always; // Don't pause with game
            LoadSettings();
        }
        else
        {
            QueueFree(); // Remove duplicate
        }
    }
    
    public void SaveSettings()
    {
        var config = new ConfigFile();
        
        // Player settings
        config.SetValue("Player", "Speed", PlayerSpeed);
        config.SetValue("Player", "Name", PlayerName);
        
        // Gameplay settings
        config.SetValue("Gameplay", "SpawnInterval", SpawnInterval);
        config.SetValue("Gameplay", "SpawnDistance", SpawnDistance);
        config.SetValue("Gameplay", "ProjectileSpeed", ProjectileSpeed);
        config.SetValue("Gameplay", "ZombieSpeed", ZombieSpeed);
        
        // Audio settings
        config.SetValue("Audio", "SoundEnabled", SoundEnabled);
        config.SetValue("Audio", "MusicEnabled", MusicEnabled);
        
        // Save to file
        var error = config.Save(SETTINGS_FILE);
        if (error == Error.Ok)
        {
            GD.Print("[GameSettings] Settings saved successfully");
        }
        else
        {
            GD.PrintErr($"[GameSettings] Failed to save settings: {error}");
        }
    }
    
    public void LoadSettings()
    {
        var config = new ConfigFile();
        var error = config.Load(SETTINGS_FILE);
        
        if (error != Error.Ok)
        {
            GD.Print("[GameSettings] No saved settings found, using defaults");
            return;
        }
        
        // Load player settings with proper type casting
        PlayerSpeed = (float)config.GetValue("Player", "Speed", 150f);
        PlayerName = (string)config.GetValue("Player", "Name", "Player");
        
        // Load gameplay settings with proper type casting
        SpawnInterval = (float)config.GetValue("Gameplay", "SpawnInterval", 3.0f);
        SpawnDistance = (float)config.GetValue("Gameplay", "SpawnDistance", 300f);
        ProjectileSpeed = (float)config.GetValue("Gameplay", "ProjectileSpeed", 400f);
        ZombieSpeed = (float)config.GetValue("Gameplay", "ZombieSpeed", 50f);
        
        // Load audio settings with proper type casting
        SoundEnabled = (bool)config.GetValue("Audio", "SoundEnabled", true);
        MusicEnabled = (bool)config.GetValue("Audio", "MusicEnabled", true);
        
        GD.Print("[GameSettings] Settings loaded successfully");
    }
    
    public void ResetToDefaults()
    {
        // Player settings
        PlayerSpeed = 150f;
        PlayerName = "Player";
        
        // Gameplay settings
        SpawnInterval = 3.0f;
        SpawnDistance = 300f;
        ProjectileSpeed = 400f;
        ZombieSpeed = 50f;
        
        // Audio settings
        SoundEnabled = true;
        MusicEnabled = true;
        
        GD.Print("[GameSettings] Settings reset to defaults");
    }
    
    public void ApplySettingsToGame()
    {
        // Find and update game objects with new settings
        var player = GetTree().GetFirstNodeInGroup("player") as Player;
        if (player != null)
        {
            player.Speed = PlayerSpeed;
        }
        
        var spawner = GetTree().GetFirstNodeInGroup("spawner") as ZombieSpawner;
        if (spawner != null)
        {
            spawner.SpawnInterval = SpawnInterval;
            spawner.SpawnDistance = SpawnDistance;
        }
        
        GD.Print("[GameSettings] Applied settings to game objects");
    }
}
