using Godot;
using System;

public partial class ZombieSpawner : Node2D
{
	[Export]
	public PackedScene ZombieScene;
	
	[Export]
	public float SpawnInterval = 3.0f; // Initial spawn interval (seconds)
	
	[Export]
	public float SpawnDistance = 300f; // Base distance from player to spawn zombies
	
	[Export]
	public float MinSpawnInterval = 0.5f; // Minimum spawn interval (fastest spawning)
	
	[Export]
	public float SpawnRateIncrease = 0.05f; // How much to decrease interval per second
	
	[Export]
	public float SpawnDistanceVariation = 100f; // Random variation in spawn distance
	
	[Export]
	public float MinSpawnDistance = 200f; // Minimum distance to prevent spawning too close
	
	[Export]
	public float MaxSpawnDistance = 800f; // Maximum distance to ensure off-screen spawning
	
	private Node2D player;
	private float spawnTimer = 0f;
	private float currentSpawnInterval;
	private float elapsedTimeSinceReset = 0f; // Time since last reset, not absolute game time
	private bool isPaused = false; // Manual pause state for the spawner
	private bool justReset = false; // Flag to prevent immediate spawn rate update after reset
	private Random random = new Random();
	
	public override void _Ready()
	{
		// Find the player node
		player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		
		// Initialize current spawn interval
		currentSpawnInterval = SpawnInterval;
		
		// Ensure this node respects the game's pause state
		ProcessMode = ProcessModeEnum.Inherit;
		
		// Add this spawner to the "spawner" group so Player can find it
		AddToGroup("spawner");
		
		GD.Print($"[ZombieSpawner] Ready - ProcessMode set to Inherit to respect game pause state");
		GD.Print($"[ZombieSpawner] Ready - Initial values: SpawnInterval: {SpawnInterval:F2}s, currentSpawnInterval: {currentSpawnInterval:F2}s, justReset: {justReset}");
		GD.Print("[ZombieSpawner] Ready - Added to 'spawner' group");
	}
	
	public override void _Process(double delta)
	{
		// Log EVERY single _Process call to see what's happening
		GD.Print($"[ZombieSpawner] _Process called - delta: {delta:F3}s, isPaused: {isPaused}, GetTree().Paused: {GetTree().Paused}, elapsedTime: {elapsedTimeSinceReset:F1}s, spawnInterval: {currentSpawnInterval:F2}s, justReset: {justReset}");
		
		if (player == null || ZombieScene == null) return;
		
		// Check both manual pause state and game pause state
		if (isPaused || GetTree().Paused)
		{
			// Debug: Log when paused to show processing is completely stopped
			GD.Print($"[ZombieSpawner] PAUSED - EXITING _Process. Manual: {isPaused}, Game: {GetTree().Paused}");
			return; // Exit completely - no time accumulation, no spawning
		}
		
		// Only update elapsed time and spawn when game is running
		elapsedTimeSinceReset += (float)delta;
		GD.Print($"[ZombieSpawner] UPDATING TIME - elapsedTime: {elapsedTimeSinceReset:F1}s");
		
		// Only update spawn rate if we haven't just reset and enough time has passed
		if (!justReset && elapsedTimeSinceReset > 0.1f) // Wait 0.1 seconds before updating spawn rate
		{
			GD.Print($"[ZombieSpawner] CALLING UpdateSpawnRate - justReset: {justReset}, elapsedTime: {elapsedTimeSinceReset:F1}s");
			UpdateSpawnRate();
		}
		else if (justReset && elapsedTimeSinceReset > 0.1f)
		{
			// Clear the reset flag after enough time has passed
			justReset = false;
			GD.Print("[ZombieSpawner] Reset flag cleared, spawn rate updates will resume");
		}
		else
		{
			// Log when we're skipping spawn rate updates
			GD.Print($"[ZombieSpawner] SKIPPING spawn rate update - justReset: {justReset}, elapsedTime: {elapsedTimeSinceReset:F1}s");
		}
		
		spawnTimer += (float)delta;
		
		if (spawnTimer >= currentSpawnInterval)
		{
			SpawnZombie();
			spawnTimer = 0f;
		}
	}
	
	private void UpdateSpawnRate()
	{
		// Store old value for comparison
		float oldSpawnInterval = currentSpawnInterval;
		
		// Linearly decrease spawn interval based on time since last reset
		currentSpawnInterval = SpawnInterval - (SpawnRateIncrease * elapsedTimeSinceReset);
		
		// Clamp to minimum spawn interval
		if (currentSpawnInterval < MinSpawnInterval)
		{
			currentSpawnInterval = MinSpawnInterval;
		}
		
		// Debug logging for spawn rate changes
		GD.Print($"[ZombieSpawner] UpdateSpawnRate - elapsedTime: {elapsedTimeSinceReset:F1}s, oldInterval: {oldSpawnInterval:F2}s, newInterval: {currentSpawnInterval:F2}s");
		
		// If the interval changed, log the calculation
		if (Math.Abs(oldSpawnInterval - currentSpawnInterval) > 0.01f)
		{
			GD.Print($"[ZombieSpawner] INTERVAL CHANGED! Calculation: {SpawnInterval:F2}s - ({SpawnRateIncrease:F2} * {elapsedTimeSinceReset:F1}s) = {currentSpawnInterval:F2}s");
		}
	}
	
	private void SpawnZombie()
	{
		// Get a random angle around the player
		float randomAngle = (float)(random.NextDouble() * Math.Tau);
		
		// Calculate spawn distance with random variation
		float randomDistance = SpawnDistance + (float)(random.NextDouble() * 2 - 1) * SpawnDistanceVariation;
		
		// Clamp distance to ensure off-screen but not too close
		randomDistance = Mathf.Clamp(randomDistance, MinSpawnDistance, MaxSpawnDistance);
		
		// Calculate spawn position
		Vector2 spawnDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
		Vector2 spawnPosition = player.GlobalPosition + (spawnDirection * randomDistance);
		
		// Create the zombie
		var zombie = ZombieScene.Instantiate() as Basic_Zombie;
		if (zombie != null)
		{
			zombie.GlobalPosition = spawnPosition;
			GetParent().AddChild(zombie);
		}
	}
	
	public void Reset()
	{
		// Debug: Log state before reset
		GD.Print($"[ZombieSpawner] ===== RESET CALLED =====");
		GD.Print($"[ZombieSpawner] BEFORE RESET - elapsedTime: {elapsedTimeSinceReset:F1}s, spawnInterval: {currentSpawnInterval:F2}s, isPaused: {isPaused}, justReset: {justReset}");
		GD.Print($"[ZombieSpawner] BEFORE RESET - SpawnInterval: {SpawnInterval:F2}s, SpawnRateIncrease: {SpawnRateIncrease:F2}");
		
		// Reset spawner state for new round
		spawnTimer = 0f;
		elapsedTimeSinceReset = 0f; // Reset elapsed time
		currentSpawnInterval = SpawnInterval;
		isPaused = false; // Ensure spawner is unpaused
		justReset = true; // Set flag to prevent immediate update
		
		// Debug: Log state after reset
		GD.Print($"[ZombieSpawner] AFTER RESET - elapsedTime: {elapsedTimeSinceReset:F1}s, spawnInterval: {currentSpawnInterval:F2}s, isPaused: {isPaused}, justReset: {justReset}");
		GD.Print($"[ZombieSpawner] ===== RESET COMPLETE =====");
	}
	
	// Method to manually pause the spawner
	public void Pause()
	{
		GD.Print($"[ZombieSpawner] ===== PAUSE CALLED =====");
		GD.Print($"[ZombieSpawner] BEFORE PAUSE - isPaused: {isPaused}, GetTree().Paused: {GetTree().Paused}");
		isPaused = true;
		GD.Print($"[ZombieSpawner] AFTER PAUSE - isPaused: {isPaused}, GetTree().Paused: {GetTree().Paused}");
		GD.Print($"[ZombieSpawner] ===== PAUSE COMPLETE =====");
	}
	
	// Method to manually unpause the spawner
	public void Unpause()
	{
		GD.Print($"[ZombieSpawner] ===== UNPAUSE CALLED =====");
		GD.Print($"[ZombieSpawner] BEFORE UNPAUSE - isPaused: {isPaused}, GetTree().Paused: {GetTree().Paused}");
		isPaused = false;
		GD.Print($"[ZombieSpawner] AFTER UNPAUSE - isPaused: {isPaused}, GetTree().Paused: {GetTree().Paused}");
		GD.Print($"[ZombieSpawner] ===== UNPAUSE COMPLETE =====");
	}
} 
