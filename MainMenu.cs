using Godot;
using System;

public partial class MainMenu : Control
{

    
    private Button playButton;
    private Button settingsButton;
    private Button exitButton;
    private Label titleLabel;
    private TextureRect background;
    
    public override void _Ready()
    {
        // Create background (same as game)
        CreateBackground();
        
        // Create title
        CreateTitle();
        
        // Create buttons
        CreateButtons();
        
        // Load saved settings
        LoadSettings();
    }
    
    private void CreateBackground()
    {
        background = new TextureRect();
        background.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        background.Texture = GD.Load<Texture2D>("res://icon.svg"); // Using default icon as background
        background.Modulate = new Color(0.2f, 0.2f, 0.3f, 1.0f); // Dark blue-gray tint
        AddChild(background);
    }
    
    private void CreateTitle()
    {
        titleLabel = new Label();
        titleLabel.Text = "VIBE GUNNER";
        titleLabel.AddThemeColorOverride("font_color", new Color(0.8f, 0.9f, 1.0f, 1.0f)); // Light blue
        titleLabel.AddThemeFontSizeOverride("font_size", 72);
        titleLabel.HorizontalAlignment = HorizontalAlignment.Center;
        titleLabel.VerticalAlignment = VerticalAlignment.Center;
        titleLabel.SetAnchorsPreset(Control.LayoutPreset.Center);
        titleLabel.Size = new Vector2(600, 120);
        titleLabel.Position = new Vector2(-300, -200); // Center horizontally, above buttons
        
        // Add retro-futuristic styling
        titleLabel.AddThemeColorOverride("font_shadow_color", new Color(0.0f, 0.5f, 1.0f, 0.8f));
        titleLabel.AddThemeConstantOverride("shadow_offset_x", 3);
        titleLabel.AddThemeConstantOverride("shadow_offset_y", 3);
        
        AddChild(titleLabel);
    }
    
    private void CreateButtons()
    {
        // Create container for buttons
        var buttonContainer = new VBoxContainer();
        buttonContainer.SetAnchorsPreset(Control.LayoutPreset.Center);
        buttonContainer.Size = new Vector2(300, 200);
        buttonContainer.Position = new Vector2(-150, 50); // Center horizontally, below title
        buttonContainer.AddThemeConstantOverride("separation", 30);
        
        // Play button
        playButton = new Button();
        playButton.Text = "PLAY";
        playButton.AddThemeFontSizeOverride("font_size", 32);
        playButton.Size = new Vector2(250, 60);
        playButton.Pressed += OnPlayPressed;
        StyleButton(playButton, new Color(0.2f, 0.8f, 0.3f, 1.0f)); // Green
        
        // Settings button
        settingsButton = new Button();
        settingsButton.Text = "SETTINGS";
        settingsButton.AddThemeFontSizeOverride("font_size", 32);
        settingsButton.Size = new Vector2(250, 60);
        settingsButton.Pressed += OnSettingsPressed;
        StyleButton(settingsButton, new Color(0.8f, 0.6f, 0.2f, 1.0f)); // Orange
        
        // Exit button
        exitButton = new Button();
        exitButton.Text = "EXIT";
        exitButton.AddThemeFontSizeOverride("font_size", 32);
        exitButton.Size = new Vector2(250, 60);
        exitButton.Pressed += OnExitPressed;
        StyleButton(exitButton, new Color(0.8f, 0.2f, 0.2f, 1.0f)); // Red
        
        buttonContainer.AddChild(playButton);
        buttonContainer.AddChild(settingsButton);
        buttonContainer.AddChild(exitButton);
        
        AddChild(buttonContainer);
    }
    
    private void StyleButton(Button button, Color color)
    {
        button.AddThemeColorOverride("font_color", new Color(1, 1, 1, 1));
        button.AddThemeColorOverride("bg_color", color);
        button.AddThemeColorOverride("bg_color_pressed", color.Darkened(0.3f));
        button.AddThemeColorOverride("bg_color_hover", color.Lightened(0.2f));
        button.AddThemeConstantOverride("corner_radius", 8);
    }
    
    private void OnPlayPressed()
    {
        GD.Print("[MainMenu] Play button pressed - transitioning to game");
        
        // Create transition effect
        var transition = CreateTransition();
        AddChild(transition);
        
        // Wait a frame then change scene
        GetTree().ProcessFrame += () => ChangeToGameScene();
    }
    
    private void OnSettingsPressed()
    {
        GD.Print("[MainMenu] Settings button pressed - transitioning to settings");
        
        // Create transition effect
        var transition = CreateTransition();
        AddChild(transition);
        
        // Wait a frame then change scene
        GetTree().ProcessFrame += () => ChangeToSettingsScene();
    }
    
    private void OnExitPressed()
    {
        GD.Print("[MainMenu] Exit button pressed - quitting game");
        GetTree().Quit();
    }
    
    private Control CreateTransition()
    {
        var transition = new Control();
        transition.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        transition.ZIndex = 1000; // Ensure it's on top
        
        var background = new ColorRect();
        background.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        background.Color = new Color(0, 0, 0, 0);
        background.Modulate = new Color(0, 0, 0, 0);
        transition.AddChild(background);
        
        // Animate the transition
        var tween = CreateTween();
        tween.TweenProperty(background, "modulate", new Color(0, 0, 0, 1), 0.3f);
        
        return transition;
    }
    
    private void ChangeToGameScene()
    {
        // Load the game scene directly
        var gameScene = GD.Load<PackedScene>("res://node_2d.tscn");
        if (gameScene != null)
        {
            GetTree().ChangeSceneToPacked(gameScene);
        }
        else
        {
            GD.PrintErr("[MainMenu] node_2d.tscn not found! Cannot transition to game.");
        }
    }
    
    private void ChangeToSettingsScene()
    {
        // Load the settings scene directly
        var settingsScene = GD.Load<PackedScene>("res://Settings.tscn");
        if (settingsScene != null)
        {
            GetTree().ChangeSceneToPacked(settingsScene);
        }
        else
        {
            GD.PrintErr("[MainMenu] Settings.tscn not found! Cannot transition to settings.");
        }
    }
    
    private void LoadSettings()
    {
        // Load any saved settings here
        GD.Print("[MainMenu] Loading saved settings");
        
        // Ensure GameSettings singleton is available
        if (GameSettings.Instance == null)
        {
            GD.Print("[MainMenu] GameSettings singleton not found, creating one");
            var gameSettings = new GameSettings();
            GetTree().Root.AddChild(gameSettings);
        }
    }
}
