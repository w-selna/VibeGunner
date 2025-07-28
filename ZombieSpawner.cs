using Godot;
using System;

public partial class ZombieSpawner : Node2D
{
    [Export]
    public PackedScene ZombieScene;
    
    [Export]
    public float SpawnInterval = 3.0f; // Spawn a zombie every 3 seconds
    
    [Export]
    public float SpawnDistance = 300f; // Distance from player to spawn zombies
    
    private Node2D player;
    private float spawnTimer = 0f;
    
    	public override void _Ready()
	{
		// Find the player node
		player = GetTree().GetFirstNodeInGroup("player") as Node2D;
	}
    
    public override void _Process(double delta)
    {
        if (player == null || ZombieScene == null) return;
        
        spawnTimer += (float)delta;
        
        if (spawnTimer >= SpawnInterval)
        {
            SpawnZombie();
            spawnTimer = 0f;
        }
    }
    
    private void SpawnZombie()
    {
        // Get a random angle around the player
        float randomAngle = (float)GD.RandRange(0, Mathf.Tau);
        
        // Calculate spawn position
        Vector2 spawnDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        Vector2 spawnPosition = player.GlobalPosition + (spawnDirection * SpawnDistance);
        
        // Create the zombie
        var zombie = (Basic_Zombie)ZombieScene.Instantiate();
        zombie.GlobalPosition = spawnPosition;
        GetParent().AddChild(zombie);
    }
} 