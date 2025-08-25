using Godot;
using System;

public partial class Player : Node2D
{
	[Export]
	public PackedScene ProjectileScene;
	
	[Export]
	public float Speed = 150f;
	
	[Export]
	public int MaxHealth = 1; // Player dies in 1 hit
	
	public int KillCount = 0;
	public int CurrentHealth;
	
	private Area2D hitbox;
	private CollisionShape2D hitboxShape;
	private Control gameOverScreen;
	private Label killCountLabel;
	private Button restartButton;
	private Button exitButton;
	private bool isDead = false;
	
	public override void _Ready()
	{
		GD.Print("[Player] _Ready() called - Initializing player");
		
		// Add this player to the "player" group so ZombieSpawner can find it
		AddToGroup("player");
		
		// Initialize health
		CurrentHealth = MaxHealth;
		GD.Print($"[Player] Health initialized: {CurrentHealth}/{MaxHealth}");
		
		// Create hitbox for the player
		hitbox = new Area2D();
		hitboxShape = new CollisionShape2D();
		var circleShape = new CircleShape2D();
		circleShape.Radius = 15f; // Collision radius for the player
		hitboxShape.Shape = circleShape;
		hitbox.AddChild(hitboxShape);
		AddChild(hitbox);
		GD.Print("[Player] Hitbox created with radius 25");
		
		// Connect hitbox signal
		hitbox.AreaEntered += OnHitboxEntered;
		GD.Print("[Player] Hitbox signal connected");
		
		// Center player on screen at start
		CenterPlayerOnScreen();
		
		GD.Print("[Player] Player initialization complete");
	}
	
	private void CenterPlayerOnScreen()
	{
		// Get the viewport size and center the player
		var viewport = GetViewport();
		if (viewport != null)
		{
			Vector2 screenCenter = viewport.GetVisibleRect().Size / 2;
			GlobalPosition = screenCenter;
			GD.Print($"[Player] Centered on screen at position: {GlobalPosition}");
		}
		else
		{
			GD.Print("[Player] Failed to get viewport for centering");
		}
	}
	
	private void CreateGameOverScreen()
	{
		GD.Print("[Player] Creating game over screen");
		
		// Create the main container
		gameOverScreen = new Control();
		gameOverScreen.SetAnchorsPreset(Control.LayoutPreset.FullRect);
		gameOverScreen.Visible = false;
		gameOverScreen.ProcessMode = ProcessModeEnum.Always;
		
		// Create background
		var background = new ColorRect();
		background.SetAnchorsPreset(Control.LayoutPreset.FullRect);
		background.Color = new Color(0, 0, 0, 0.8f);
		gameOverScreen.AddChild(background);
		
		// Create kill count label
		killCountLabel = new Label();
		killCountLabel.Text = "GAME OVER";
		killCountLabel.AddThemeColorOverride("font_color", new Color(1, 0, 0, 1));
		killCountLabel.AddThemeFontSizeOverride("font_size", 48);
		killCountLabel.HorizontalAlignment = HorizontalAlignment.Center;
		killCountLabel.VerticalAlignment = VerticalAlignment.Center;
		killCountLabel.SetAnchorsPreset(Control.LayoutPreset.Center);
		killCountLabel.Size = new Vector2(400, 100); // Give it a fixed size for proper centering
		killCountLabel.Position = new Vector2(-200, -50); // Center horizontally and vertically by offsetting half width/height
		gameOverScreen.AddChild(killCountLabel);
		
		// Create restart button
		restartButton = new Button();
		restartButton.Text = "Start New Round";
		restartButton.AddThemeFontSizeOverride("font_size", 24);
		restartButton.SetAnchorsPreset(Control.LayoutPreset.Center);
		restartButton.Size = new Vector2(200, 50); // Give it a fixed size for proper centering
		restartButton.Position = new Vector2(-100, 80); // Center horizontally, offset below label
		restartButton.ProcessMode = ProcessModeEnum.Always;
		
		// Connect the restart button signal
		restartButton.Pressed += OnRestartPressed;
		gameOverScreen.AddChild(restartButton);
		
		// Create main menu button (top left)
		var mainMenuButton = new Button();
		mainMenuButton.Text = "Main Menu";
		mainMenuButton.AddThemeFontSizeOverride("font_size", 18);
		mainMenuButton.Size = new Vector2(120, 40);
		mainMenuButton.SetAnchorsPreset(Control.LayoutPreset.TopLeft);
		mainMenuButton.Position = new Vector2(20, 20);
		mainMenuButton.ProcessMode = ProcessModeEnum.Always;
		
		// Connect the main menu button signal
		mainMenuButton.Pressed += OnMainMenuPressed;
		gameOverScreen.AddChild(mainMenuButton);
		
		// Create exit button (top right)
		exitButton = new Button();
		exitButton.Text = "EXIT";
		exitButton.AddThemeFontSizeOverride("font_size", 18);
		exitButton.Size = new Vector2(120, 40);
		exitButton.SetAnchorsPreset(Control.LayoutPreset.TopRight);
		exitButton.Position = new Vector2(-140, 20); // Position from right edge
		exitButton.ProcessMode = ProcessModeEnum.Always;
		
		// Style the exit button with red color
		exitButton.AddThemeColorOverride("font_color", new Color(1, 1, 1, 1));
		exitButton.AddThemeColorOverride("bg_color", new Color(0.8f, 0.2f, 0.2f, 1.0f));
		exitButton.AddThemeColorOverride("bg_color_pressed", new Color(0.6f, 0.1f, 0.1f, 1.0f));
		exitButton.AddThemeColorOverride("bg_color_hover", new Color(1.0f, 0.3f, 0.3f, 1.0f));
		exitButton.AddThemeConstantOverride("corner_radius", 4);
		
		// Connect the exit button signal
		exitButton.Pressed += OnExitPressed;
		gameOverScreen.AddChild(exitButton);
		
		// Add to the scene tree
		GetTree().Root.AddChild(gameOverScreen);
		
		GD.Print("[Player] Game over screen created and added to scene tree");
	}
	
	public override void _Process(double delta)
	{
		if (isDead) return; // Don't process input if dead
		
		Vector2 velocity = new Vector2();
		if (Input.IsActionPressed("ui_right")) velocity.X += 1;
		if (Input.IsActionPressed("ui_left")) velocity.X -= 1;
		if (Input.IsActionPressed("ui_down")) velocity.Y += 1;
		if (Input.IsActionPressed("ui_up")) velocity.Y -= 1;
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			Position += velocity * (float)delta;
		}
		if (Input.IsActionJustPressed("mouse_left"))
		{
			ShootProjectile();
		}
		
		// Test key for debugging restart functionality
		if (Input.IsActionJustPressed("ui_accept")) // Press Enter/Space
		{
			GD.Print("[Player] Test key pressed - testing restart button");
			TestRestartButton();
		}
	}
	
	private void OnHitboxEntered(Area2D area)
	{
		GD.Print($"[Player] Hitbox entered by: {area.Name}");
		
		// Check if the entering area belongs to a zombie
		if (area.GetParent() is Basic_Zombie)
		{
			GD.Print("[Player] Zombie detected! Taking damage");
			TakeDamage();
		}
		else
		{
			GD.Print($"[Player] Non-zombie object entered hitbox: {area.GetParent()?.GetType().Name ?? "Unknown"}");
		}
	}
	
	private void TakeDamage()
	{
		GD.Print($"[Player] Taking damage! Health before: {CurrentHealth}");
		CurrentHealth--;
		GD.Print($"[Player] Health after damage: {CurrentHealth}");
		
		if (CurrentHealth <= 0)
		{
			GD.Print("[Player] Health reached 0, calling Die()");
			Die();
		}
	}
	
	private void Die()
	{
		GD.Print("[Player] Die() method called");
		isDead = true;
		
		// Create game over screen if it doesn't exist
		if (gameOverScreen == null)
		{
			CreateGameOverScreen();
		}
		
		// Update the kill count display
		killCountLabel.Text = $"GAME OVER\nFinal Kill Count: {KillCount}";
		
		// Show the game over screen
		gameOverScreen.Visible = true;
		
		// Pause the game
		GetTree().Paused = true;
		
		// Manually pause the spawner to ensure it stops completely
		var spawner = GetTree().GetFirstNodeInGroup("spawner") as ZombieSpawner;
		if (spawner != null)
		{
			GD.Print("[Player] Manually pausing spawner");
			spawner.Pause();
		}
		
		GD.Print($"[Player] Player died! Final Kill Count: {KillCount}");
	}
	
	private void OnRestartPressed()
	{
		GD.Print("[Player] Restart button pressed - starting new game");
		
		// Hide the game over screen
		gameOverScreen.Visible = false;
		
		// Unpause the game
		GetTree().Paused = false;
		
		// Manually unpause the spawner
		var spawner = GetTree().GetFirstNodeInGroup("spawner") as ZombieSpawner;
		if (spawner != null)
		{
			GD.Print("[Player] Manually unpausing spawner");
			spawner.Unpause();
		}
		
		// Reset the player
		ResetPlayer();
		
		// Reset the game state
		ResetGame();
		
		GD.Print("[Player] Game restart complete");
	}
	
	private void OnMainMenuPressed()
	{
		GD.Print("[Player] Main Menu button pressed - returning to main menu");
		
		// Reset all game metrics (same as restart)
		ResetPlayer();
		ResetGame();
		
		// Hide the game over screen
		gameOverScreen.Visible = false;
		
		// Unpause the game
		GetTree().Paused = false;
		
		// Change to main menu scene
		var mainMenuScene = GD.Load<PackedScene>("res://MainMenu.tscn");
		if (mainMenuScene != null)
		{
			GetTree().ChangeSceneToPacked(mainMenuScene);
		}
		else
		{
			GD.PrintErr("[Player] MainMenu.tscn not found! Cannot return to main menu.");
		}
	}
	
	private void OnExitPressed()
	{
		GD.Print("[Player] Exit button pressed - exiting game");
		GetTree().Quit();
	}
	
	// Test method to verify button connection
	public void TestRestartButton()
	{
		GD.Print("[Player] Testing restart button connection");
		if (restartButton != null)
		{
			GD.Print($"[Player] Button exists: {restartButton.Name}");
			GD.Print($"[Player] Button visible: {restartButton.Visible}");
			GD.Print($"[Player] Button process mode: {restartButton.ProcessMode}");
			
			// Manually trigger the restart
			GD.Print("[Player] Manually triggering restart for testing");
			OnRestartPressed();
		}
		else
		{
			GD.Print("[Player] Restart button is null!");
		}
	}
	
	private void ResetPlayer()
	{
		GD.Print("[Player] Resetting player");
		
		// Reset health
		CurrentHealth = MaxHealth;
		GD.Print($"[Player] Health reset to: {CurrentHealth}");
		
		isDead = false;
		GD.Print("[Player] Player marked as alive");
		
		// Center player on screen
		CenterPlayerOnScreen();
		
		// Reset kill count
		KillCount = 0;
		GD.Print("[Player] Kill count reset to 0");
	}
	
	private void ResetGame()
	{
		GD.Print("[Player] Resetting game state");
		
		// Remove all existing zombies
		var zombies = GetTree().GetNodesInGroup("zombie");
		GD.Print($"[Player] Found {zombies.Count} zombies to remove");
		foreach (var zombie in zombies)
		{
			GD.Print($"[Player] Removing zombie: {zombie.Name}");
			zombie.QueueFree();
		}
		
		// Remove all existing projectiles
		var projectiles = GetTree().GetNodesInGroup("projectile");
		GD.Print($"[Player] Found {projectiles.Count} projectiles to remove");
		foreach (var projectile in projectiles)
		{
			GD.Print($"[Player] Removing projectile: {projectile.Name}");
			projectile.QueueFree();
		}
		
		// Reset spawner timer
		var spawner = GetTree().GetFirstNodeInGroup("spawner") as ZombieSpawner;
		if (spawner != null)
		{
			GD.Print("[Player] Resetting spawner");
			spawner.Reset(); // This will also set isPaused = false
			
			// Double-check that spawner is unpaused
			spawner.Unpause();
		}
		else
		{
			GD.Print("[Player] No spawner found in 'spawner' group");
		}
		
		GD.Print("[Player] Game state reset complete");
	}
	
	public void AddKill()
	{
		KillCount++;
		GD.Print($"[Player] Kill added! Total kills: {KillCount}");
	}
	
	private void ShootProjectile()
	{
		if (ProjectileScene == null)
		{
			GD.Print("[Player] Cannot shoot: ProjectileScene is null");
			return;
		}
		
		GD.Print("[Player] Shooting projectile");
		var projectile = (Node2D)ProjectileScene.Instantiate();
		var mousePos = GetViewport().GetMousePosition();
		var direction = (mousePos - GlobalPosition).Normalized();
		projectile.Position = GlobalPosition;
		((Projectile)projectile).direction = direction;
		
		// Add projectile to a group for easy cleanup during reset
		projectile.AddToGroup("projectile");
		
		GetParent().AddChild(projectile);
		GD.Print($"[Player] Projectile created at position {GlobalPosition} with direction {direction}");
	}
} 
