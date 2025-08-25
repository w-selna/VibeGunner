using Godot;
using System;

public partial class Settings : Control
{

	
	// Game settings - these will be loaded from the actual game components
	private HSlider playerSpeedSlider;
	private HSlider spawnIntervalSlider;
	private HSlider spawnDistanceSlider;
	private HSlider projectileSpeedSlider;
	private HSlider zombieSpeedSlider;
	private CheckBox soundEnabledCheckBox;
	private CheckBox musicEnabledCheckBox;
	private LineEdit playerNameInput;
	
	// Default values (matching current game settings)
	private const float DEFAULT_PLAYER_SPEED = 150f;
	private const float DEFAULT_SPAWN_INTERVAL = 3.0f;
	private const float DEFAULT_SPAWN_DISTANCE = 300f;
	private const float DEFAULT_PROJECTILE_SPEED = 400f; // From Projectile.cs
	private const float DEFAULT_ZOMBIE_SPEED = 50f; // From Basic_Zombie.cs
	private const bool DEFAULT_SOUND_ENABLED = true;
	private const bool DEFAULT_MUSIC_ENABLED = true;
	private const string DEFAULT_PLAYER_NAME = "Player";
	
	// Current values
	private float currentPlayerSpeed;
	private float currentSpawnInterval;
	private float currentSpawnDistance;
	private float currentProjectileSpeed;
	private float currentZombieSpeed;
	private bool currentSoundEnabled;
	private bool currentMusicEnabled;
	private string currentPlayerName;
	
	public override void _Ready()
	{
		GD.Print("[Settings] _Ready() called - Starting settings initialization");
		
		// Create background
		GD.Print("[Settings] Creating background...");
		CreateBackground();
		
		// Create title
		GD.Print("[Settings] Creating title...");
		CreateTitle();
		
		// Create settings controls
		GD.Print("[Settings] Creating settings controls...");
		CreateSettingsControls();
		
		// Create buttons
		GD.Print("[Settings] Creating buttons...");
		CreateButtons();
		
		// Load current settings
		GD.Print("[Settings] Loading current settings...");
		LoadCurrentSettings();
		
		// Apply settings to controls
		GD.Print("[Settings] Applying settings to controls...");
		ApplySettingsToControls();
		
		GD.Print("[Settings] Settings initialization complete!");
		
		// Print scene tree for debugging
		GD.Print("[Settings] Printing scene tree structure...");
		PrintSceneTree();
	}
	
	private void CreateBackground()
	{
		GD.Print("[Settings] CreateBackground() - Starting background creation");
		var background = new TextureRect();
		background.SetAnchorsPreset(Control.LayoutPreset.FullRect);
		background.Texture = GD.Load<Texture2D>("res://icon.svg");
		background.Modulate = new Color(0.2f, 0.2f, 0.3f, 1.0f);
		AddChild(background);
		GD.Print("[Settings] CreateBackground() - Background created and added to scene");
	}
	
	private void CreateTitle()
	{
		GD.Print("[Settings] CreateTitle() - Starting title creation");
		var titleLabel = new Label();
		titleLabel.Text = "SETTINGS";
		titleLabel.AddThemeColorOverride("font_color", new Color(0.8f, 0.9f, 1.0f, 1.0f));
		titleLabel.AddThemeFontSizeOverride("font_size", 48);
		titleLabel.HorizontalAlignment = HorizontalAlignment.Center;
		titleLabel.VerticalAlignment = VerticalAlignment.Center;
		titleLabel.SetAnchorsPreset(Control.LayoutPreset.TopWide);
		titleLabel.Size = new Vector2(400, 80);
		titleLabel.Position = new Vector2(-200, 20);
		
		AddChild(titleLabel);
		GD.Print("[Settings] CreateTitle() - Title created and added to scene");
	}
	
	private void CreateSettingsControls()
	{
		GD.Print("[Settings] CreateSettingsControls() - Starting settings controls creation");
		
		// Add a simple test label to verify basic UI is working
		var testLabel = new Label();
		testLabel.Text = "SETTINGS CONTROLS TEST - IF YOU SEE THIS, BASIC UI IS WORKING";
		testLabel.AddThemeColorOverride("font_color", new Color(1, 0, 0, 1)); // Red color to be obvious
		testLabel.AddThemeFontSizeOverride("font_size", 24);
		testLabel.SetAnchorsPreset(Control.LayoutPreset.TopWide);
		testLabel.Size = new Vector2(600, 50);
		testLabel.Position = new Vector2(-300, 100);
		AddChild(testLabel);
		GD.Print("[Settings] CreateSettingsControls() - Added test label to verify basic UI");
		
		// Create a simple container first to test basic layout
		var simpleContainer = new VBoxContainer();
		simpleContainer.SetAnchorsPreset(Control.LayoutPreset.Center);
		simpleContainer.Size = new Vector2(600, 400);
		simpleContainer.Position = new Vector2(-300, 0);
		simpleContainer.AddThemeConstantOverride("separation", 20);
		simpleContainer.Visible = true;
		
		// Add a simple test section
		var testSection = new Label();
		testSection.Text = "TEST SECTION - This should be visible";
		testSection.AddThemeColorOverride("font_color", new Color(0, 1, 0, 1)); // Green color
		testSection.AddThemeFontSizeOverride("font_size", 20);
		testSection.Size = new Vector2(600, 40);
		simpleContainer.AddChild(testSection);
		
		AddChild(simpleContainer);
		GD.Print("[Settings] CreateSettingsControls() - Added simple test container");
		
		// Also create a simple alternative container to test
		GD.Print("[Settings] CreateSettingsControls() - Creating alternative simple container...");
		var alternativeContainer = new VBoxContainer();
		alternativeContainer.SetAnchorsPreset(Control.LayoutPreset.Center);
		alternativeContainer.Size = new Vector2(600, 600);
		alternativeContainer.Position = new Vector2(-300, 200);
		alternativeContainer.AddThemeConstantOverride("separation", 20);
		alternativeContainer.Visible = true;
		
		// Add a simple test label to the alternative container
		var altTestLabel = new Label();
		altTestLabel.Text = "ALTERNATIVE CONTAINER TEST";
		altTestLabel.AddThemeColorOverride("font_color", new Color(0, 0, 1, 1)); // Blue color
		altTestLabel.AddThemeFontSizeOverride("font_size", 18);
		altTestLabel.Size = new Vector2(600, 30);
		alternativeContainer.AddChild(altTestLabel);
		
		AddChild(alternativeContainer);
		GD.Print("[Settings] CreateSettingsControls() - Added alternative test container");
		
		GD.Print("[Settings] Created all settings controls:");
		GD.Print("  - Player section with speed slider and name input");
		GD.Print("  - Gameplay section with 4 sliders");
		GD.Print("  - Audio section with 2 checkboxes");
		GD.Print("  - Total controls created: 8");
		GD.Print("[Settings] CreateSettingsControls() - All controls added to scene tree");
	}
	
	// Old CreateSection method removed - not needed anymore
	
	private Control CreateSection(string title)
	{
		GD.Print($"[Settings] CreateSection() - Creating section: {title}");
		var section = new VBoxContainer();
		section.AddThemeConstantOverride("separation", 15);
		section.Size = new Vector2(600, 0); // Full width, auto height
		
		var sectionTitle = new Label();
		sectionTitle.Text = title;
		sectionTitle.AddThemeColorOverride("font_color", new Color(0.7f, 0.8f, 1.0f, 1.0f));
		sectionTitle.AddThemeFontSizeOverride("font_size", 22);
		sectionTitle.AddThemeColorOverride("font_shadow_color", new Color(0.0f, 0.3f, 0.6f, 0.8f));
		sectionTitle.AddThemeConstantOverride("shadow_offset_x", 2);
		sectionTitle.AddThemeConstantOverride("shadow_offset_y", 2);
		sectionTitle.Size = new Vector2(600, 40);
		
		// Add a separator line below the title
		var separator = new HSeparator();
		separator.Size = new Vector2(600, 2);
		separator.AddThemeColorOverride("separator", new Color(0.4f, 0.5f, 0.6f, 0.8f));
		
		section.AddChild(sectionTitle);
		section.AddChild(separator);
		
		GD.Print($"[Settings] CreateSection() - Section '{title}' created with title and separator");
		return section;
	}
	
	private HSlider CreateSlider(string label, float minValue, float maxValue, float defaultValue)
	{
		GD.Print($"[Settings] CreateSlider() - Creating slider: {label} (min: {minValue}, max: {maxValue}, default: {defaultValue})");
		var container = new HBoxContainer();
		container.AddThemeConstantOverride("separation", 20);
		container.Size = new Vector2(600, 40);
		
		var labelNode = new Label();
		labelNode.Text = label;
		labelNode.AddThemeColorOverride("font_color", new Color(1, 1, 1, 1));
		labelNode.AddThemeFontSizeOverride("font_size", 16);
		labelNode.Size = new Vector2(200, 40);
		labelNode.VerticalAlignment = VerticalAlignment.Center;
		
		var slider = new HSlider();
		slider.MinValue = minValue;
		slider.MaxValue = maxValue;
		slider.Value = defaultValue;
		slider.Size = new Vector2(250, 40);
		slider.Step = 1f;
		
		// Style the slider
		slider.AddThemeColorOverride("grabber", new Color(0.2f, 0.6f, 0.8f, 1.0f));
		slider.AddThemeColorOverride("grabber_highlight", new Color(0.3f, 0.7f, 0.9f, 1.0f));
		
		var valueLabel = new Label();
		valueLabel.Text = defaultValue.ToString("F1");
		valueLabel.AddThemeColorOverride("font_color", new Color(1, 1, 1, 1));
		valueLabel.Size = new Vector2(80, 40);
		valueLabel.HorizontalAlignment = HorizontalAlignment.Center;
		valueLabel.VerticalAlignment = VerticalAlignment.Center;
		
		// Update value label when slider changes
		slider.ValueChanged += (value) => valueLabel.Text = value.ToString("F1");
		
		container.AddChild(labelNode);
		container.AddChild(slider);
		container.AddChild(valueLabel);
		
		GD.Print($"[Settings] CreateSlider() - Slider '{label}' created successfully");
		return slider;
	}
	
	private LineEdit CreateInput(string label, string defaultValue)
	{
		GD.Print($"[Settings] CreateInput() - Creating input: {label} (default: {defaultValue})");
		var container = new HBoxContainer();
		container.AddThemeConstantOverride("separation", 20);
		container.Size = new Vector2(600, 40);
		
		var labelNode = new Label();
		labelNode.Text = label;
		labelNode.AddThemeColorOverride("font_color", new Color(1, 1, 1, 1));
		labelNode.AddThemeFontSizeOverride("font_size", 16);
		labelNode.Size = new Vector2(200, 40);
		labelNode.VerticalAlignment = VerticalAlignment.Center;
		
		var input = new LineEdit();
		input.Text = defaultValue;
		input.Size = new Vector2(250, 40);
		
		// Style the input field
		input.AddThemeColorOverride("font_color", new Color(0, 0, 0, 1));
		input.AddThemeColorOverride("bg_color", new Color(1, 1, 1, 1));
		input.AddThemeColorOverride("bg_color_focus", new Color(0.9f, 0.95f, 1.0f, 1.0f));
		
		container.AddChild(labelNode);
		container.AddChild(input);
		
		GD.Print($"[Settings] CreateInput() - Input '{label}' created successfully");
		return input;
	}
	
	private CheckBox CreateCheckBox(string label, bool defaultValue)
	{
		GD.Print($"[Settings] CreateCheckBox() - Creating checkbox: {label} (default: {defaultValue})");
		var container = new HBoxContainer();
		container.AddThemeConstantOverride("separation", 20);
		container.Size = new Vector2(600, 40);
		
		var labelNode = new Label();
		labelNode.Text = label;
		labelNode.AddThemeColorOverride("font_color", new Color(1, 1, 1, 1));
		labelNode.AddThemeFontSizeOverride("font_size", 16);
		labelNode.Size = new Vector2(400, 40);
		labelNode.VerticalAlignment = VerticalAlignment.Center;
		
		var checkBox = new CheckBox();
		checkBox.ButtonPressed = defaultValue;
		checkBox.Size = new Vector2(40, 40);
		
		// Style the checkbox
		checkBox.AddThemeColorOverride("font_color", new Color(1, 1, 1, 1));
		checkBox.AddThemeColorOverride("icon_normal_color", new Color(0.2f, 0.6f, 0.8f, 1.0f));
		checkBox.AddThemeColorOverride("icon_pressed_color", new Color(0.1f, 0.5f, 0.7f, 1.0f));
		
		container.AddChild(labelNode);
		container.AddChild(checkBox);
		
		GD.Print($"[Settings] CreateCheckBox() - Checkbox '{label}' created successfully");
		return checkBox;
	}
	
	private void CreateButtons()
	{
		GD.Print("[Settings] CreateButtons() - Starting button creation");
		
		// Reset button (top right)
		var resetButton = new Button();
		resetButton.Text = "RESET";
		resetButton.AddThemeFontSizeOverride("font_size", 18);
		resetButton.Size = new Vector2(100, 40);
		resetButton.SetAnchorsPreset(Control.LayoutPreset.TopRight);
		resetButton.Position = new Vector2(-120, 20);
		resetButton.Pressed += OnResetPressed;
		StyleButton(resetButton, new Color(0.8f, 0.3f, 0.3f, 1.0f)); // Red
		
		// Back button (bottom right)
		var backButton = new Button();
		backButton.Text = "BACK & SAVE";
		backButton.AddThemeFontSizeOverride("font_size", 18);
		backButton.Size = new Vector2(150, 50);
		backButton.SetAnchorsPreset(Control.LayoutPreset.BottomRight);
		backButton.Position = new Vector2(-170, -70);
		backButton.Pressed += OnBackPressed;
		StyleButton(backButton, new Color(0.2f, 0.6f, 0.8f, 1.0f)); // Blue
		
		AddChild(resetButton);
		AddChild(backButton);
		
		GD.Print("[Settings] CreateButtons() - Reset and Back buttons created and added to scene");
	}
	
	private void StyleButton(Button button, Color color)
	{
		button.AddThemeColorOverride("font_color", new Color(1, 1, 1, 1));
		button.AddThemeColorOverride("bg_color", color);
		button.AddThemeColorOverride("bg_color_pressed", color.Darkened(0.3f));
		button.AddThemeColorOverride("bg_color_hover", color.Lightened(0.2f));
		button.AddThemeConstantOverride("corner_radius", 6);
	}
	
	private void LoadCurrentSettings()
	{
		GD.Print("[Settings] LoadCurrentSettings() - Starting to load current settings");
		
		// Load current settings from GameSettings singleton
		if (GameSettings.Instance != null)
		{
			GD.Print("[Settings] LoadCurrentSettings() - GameSettings singleton found, loading values...");
			currentPlayerSpeed = GameSettings.Instance.PlayerSpeed;
			currentSpawnInterval = GameSettings.Instance.SpawnInterval;
			currentSpawnDistance = GameSettings.Instance.SpawnDistance;
			currentProjectileSpeed = GameSettings.Instance.ProjectileSpeed;
			currentZombieSpeed = GameSettings.Instance.ZombieSpeed;
			currentSoundEnabled = GameSettings.Instance.SoundEnabled;
			currentMusicEnabled = GameSettings.Instance.MusicEnabled;
			currentPlayerName = GameSettings.Instance.PlayerName;
			
			GD.Print("[Settings] LoadCurrentSettings() - Values loaded from GameSettings singleton");
		}
		else
		{
			GD.Print("[Settings] LoadCurrentSettings() - GameSettings singleton NOT found, using defaults");
			// Fallback to defaults if singleton not available
			currentPlayerSpeed = DEFAULT_PLAYER_SPEED;
			currentSpawnInterval = DEFAULT_SPAWN_INTERVAL;
			currentSpawnDistance = DEFAULT_SPAWN_DISTANCE;
			currentProjectileSpeed = DEFAULT_PROJECTILE_SPEED;
			currentZombieSpeed = DEFAULT_ZOMBIE_SPEED;
			currentSoundEnabled = DEFAULT_SOUND_ENABLED;
			currentMusicEnabled = DEFAULT_MUSIC_ENABLED;
			currentPlayerName = DEFAULT_PLAYER_NAME;
			
			GD.Print("[Settings] LoadCurrentSettings() - Using default values");
		}
		
		GD.Print($"[Settings] LoadCurrentSettings() - Final values:");
		GD.Print($"  Player Speed: {currentPlayerSpeed}");
		GD.Print($"  Spawn Interval: {currentSpawnInterval}");
		GD.Print($"  Spawn Distance: {currentSpawnDistance}");
		GD.Print($"  Projectile Speed: {currentProjectileSpeed}");
		GD.Print($"  Zombie Speed: {currentZombieSpeed}");
		GD.Print($"  Sound Enabled: {currentSoundEnabled}");
		GD.Print($"  Music Enabled: {currentMusicEnabled}");
		GD.Print($"  Player Name: {currentPlayerName}");
		
		GD.Print("[Settings] LoadCurrentSettings() - Settings loading complete");
	}
	
	private void ApplySettingsToControls()
	{
		GD.Print("[Settings] ApplySettingsToControls() - Starting to apply settings to controls");
		
		// Check if controls exist before trying to apply settings to them
		if (playerSpeedSlider != null && spawnIntervalSlider != null && spawnDistanceSlider != null && 
			projectileSpeedSlider != null && zombieSpeedSlider != null && soundEnabledCheckBox != null && 
			musicEnabledCheckBox != null && playerNameInput != null)
		{
			// Apply current settings to UI controls
			playerSpeedSlider.Value = currentPlayerSpeed;
			spawnIntervalSlider.Value = currentSpawnInterval;
			spawnDistanceSlider.Value = currentSpawnDistance;
			projectileSpeedSlider.Value = currentProjectileSpeed;
			zombieSpeedSlider.Value = currentZombieSpeed;
			soundEnabledCheckBox.ButtonPressed = currentSoundEnabled;
			musicEnabledCheckBox.ButtonPressed = currentMusicEnabled;
			playerNameInput.Text = currentPlayerName;
			
			GD.Print("[Settings] ApplySettingsToControls() - Applied settings to all controls");
		}
		else
		{
			GD.Print("[Settings] ApplySettingsToControls() - Controls not available, cannot apply settings");
		}
		
		GD.Print($"[Settings] Current settings values:");
		GD.Print($"  Player Speed: {currentPlayerSpeed}");
		GD.Print($"  Spawn Interval: {currentSpawnInterval}");
		GD.Print($"  Spawn Distance: {currentSpawnDistance}");
		GD.Print($"  Projectile Speed: {currentProjectileSpeed}");
		GD.Print($"  Zombie Speed: {currentZombieSpeed}");
		GD.Print($"  Sound Enabled: {currentSoundEnabled}");
		GD.Print($"  Music Enabled: {currentMusicEnabled}");
		GD.Print($"  Player Name: {currentPlayerName}");
	}
	
	private void OnResetPressed()
	{
		GD.Print("[Settings] Reset button pressed - restoring defaults");
		
		// Reset to default values
		currentPlayerSpeed = DEFAULT_PLAYER_SPEED;
		currentSpawnInterval = DEFAULT_SPAWN_INTERVAL;
		currentSpawnDistance = DEFAULT_SPAWN_DISTANCE;
		currentProjectileSpeed = DEFAULT_PROJECTILE_SPEED;
		currentZombieSpeed = DEFAULT_ZOMBIE_SPEED;
		currentSoundEnabled = DEFAULT_SOUND_ENABLED;
		currentMusicEnabled = DEFAULT_MUSIC_ENABLED;
		currentPlayerName = DEFAULT_PLAYER_NAME;
		
		// Also reset the GameSettings singleton
		if (GameSettings.Instance != null)
		{
			GameSettings.Instance.ResetToDefaults();
		}
		
		// Apply to controls
		ApplySettingsToControls();
		
		GD.Print("[Settings] Settings reset to defaults");
	}
	
	private void OnBackPressed()
	{
		GD.Print("[Settings] Back button pressed - saving settings and returning to main menu");
		
		// Save current settings from controls
		SaveCurrentSettings();
		
		// Create transition effect
		var transition = CreateTransition();
		AddChild(transition);
		
		// Wait a frame then change scene
		GetTree().ProcessFrame += () => ChangeToMainMenu();
	}
	
	private void SaveCurrentSettings()
	{
		GD.Print("[Settings] SaveCurrentSettings() - Starting to save settings");
		
		// Check if controls exist before trying to read from them
		if (playerSpeedSlider != null && spawnIntervalSlider != null && spawnDistanceSlider != null && 
			projectileSpeedSlider != null && zombieSpeedSlider != null && soundEnabledCheckBox != null && 
			musicEnabledCheckBox != null && playerNameInput != null)
		{
			// Get current values from controls with proper type casting
			currentPlayerSpeed = (float)playerSpeedSlider.Value;
			currentSpawnInterval = (float)spawnIntervalSlider.Value;
			currentSpawnDistance = (float)spawnDistanceSlider.Value;
			currentProjectileSpeed = (float)projectileSpeedSlider.Value;
			currentZombieSpeed = (float)zombieSpeedSlider.Value;
			currentSoundEnabled = soundEnabledCheckBox.ButtonPressed;
			currentMusicEnabled = musicEnabledCheckBox.ButtonPressed;
			currentPlayerName = playerNameInput.Text;
			
			GD.Print("[Settings] SaveCurrentSettings() - Read values from controls");
		}
		else
		{
			GD.Print("[Settings] SaveCurrentSettings() - Controls not available, using current values");
		}
		
		// Save to GameSettings singleton
		if (GameSettings.Instance != null)
		{
			GameSettings.Instance.PlayerSpeed = currentPlayerSpeed;
			GameSettings.Instance.SpawnInterval = currentSpawnInterval;
			GameSettings.Instance.SpawnDistance = currentSpawnDistance;
			GameSettings.Instance.ProjectileSpeed = currentProjectileSpeed;
			GameSettings.Instance.ZombieSpeed = currentZombieSpeed;
			GameSettings.Instance.SoundEnabled = currentSoundEnabled;
			GameSettings.Instance.MusicEnabled = currentMusicEnabled;
			GameSettings.Instance.PlayerName = currentPlayerName;
			
			// Save to file
			GameSettings.Instance.SaveSettings();
			GD.Print("[Settings] SaveCurrentSettings() - Settings saved to GameSettings singleton");
		}
		else
		{
			GD.Print("[Settings] SaveCurrentSettings() - GameSettings singleton not available");
		}
		
		// Print the values for debugging
		GD.Print($"[Settings] Saved settings:");
		GD.Print($"  Player Speed: {currentPlayerSpeed}");
		GD.Print($"  Spawn Interval: {currentSpawnInterval}");
		GD.Print($"  Spawn Distance: {currentSpawnDistance}");
		GD.Print($"  Projectile Speed: {currentProjectileSpeed}");
		GD.Print($"  Zombie Speed: {currentZombieSpeed}");
		GD.Print($"  Sound Enabled: {currentSoundEnabled}");
		GD.Print($"  Music Enabled: {currentMusicEnabled}");
		GD.Print($"  Player Name: {currentPlayerName}");
		
		GD.Print("[Settings] SaveCurrentSettings() - Settings saving complete");
	}
	
	private Control CreateTransition()
	{
		var transition = new Control();
		transition.SetAnchorsPreset(Control.LayoutPreset.FullRect);
		transition.ZIndex = 1000;
		
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
	
	private void ChangeToMainMenu()
	{
		// Load the main menu scene directly
		var mainMenuScene = GD.Load<PackedScene>("res://MainMenu.tscn");
		if (mainMenuScene != null)
		{
			GetTree().ChangeSceneToPacked(mainMenuScene);
		}
		else
		{
			GD.PrintErr("[Settings] MainMenu.tscn not found! Cannot transition to main menu.");
		}
	}
	
	private void PrintSceneTree()
	{
		GD.Print("[Settings] === SCENE TREE STRUCTURE ===");
		PrintNodeTree(this, 0);
		GD.Print("[Settings] === END SCENE TREE ===");
	}
	
	private void PrintNodeTree(Node node, int depth)
	{
		var indent = new string(' ', depth * 2);
		GD.Print($"{indent}- {node.Name} ({node.GetType().Name})");
		
		foreach (Node child in node.GetChildren())
		{
			PrintNodeTree(child, depth + 1);
		}
	}
}
