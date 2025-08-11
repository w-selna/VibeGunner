using Godot;
using System;

public partial class Basic_Zombie : Node2D
{
	[Export]
	public float Speed = 50f; // Slow movement speed
	
	[Export]
	public float DetectionRange = 1000f; // How far the zombie can "see" the player
	
	[Export]
	public float JitterStrength = 55f; // How much random movement to add
	
	[Export]
	public float JitterChangeInterval = 0.7f; // How often to change jitter direction
	
	private Node2D player;
	private Sprite2D sprite;
	private Area2D collisionArea;
	private Vector2 jitterDirection;
	private float jitterTimer;
	
	public override void _Ready()
	{
		// Add zombie to group for easy identification
		AddToGroup("zombie");
		
		// Find the player node
		player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		
		// Create a simple zombie sprite (red circle for now)
		sprite = new Sprite2D();
		sprite.Texture = GD.Load<Texture2D>("res://zomb_sprite.png");
		sprite.Modulate = new Color(0.8f, 0.2f, 0.2f, 1.0f); // Red tint for zombie
		sprite.Scale = new Vector2(0.8f, 0.8f); // Slightly smaller than player
		AddChild(sprite);
		
		// Create collision area for the zombie
		collisionArea = new Area2D();
		var collisionShape = new CollisionShape2D();
		var circleShape = new CircleShape2D();
		circleShape.Radius = 20f; // Collision radius for the zombie
		collisionShape.Shape = circleShape;
		collisionArea.AddChild(collisionShape);
		AddChild(collisionArea);
		
		// Initialize jitter
		UpdateJitterDirection();
	}
	
	private void UpdateJitterDirection()
	{
		// Generate random direction for jitter
		float randomAngle = GD.Randf() * Mathf.Tau; // Random angle between 0 and 2π
		jitterDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
	}
	
	public override void _Process(double delta)
	{
		if (player == null) return;
		
		// Update jitter timer and direction
		jitterTimer += (float)delta;
		if (jitterTimer >= JitterChangeInterval)
		{
			UpdateJitterDirection();
			jitterTimer = 0f;
		}
		
		// Calculate direction to player
		Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
		float distance = GlobalPosition.DistanceTo(player.GlobalPosition);
		
		// Only move if player is within detection range
		if (distance <= DetectionRange)
		{
			// Combine player direction with jitter
			Vector2 finalDirection = (direction + jitterDirection * JitterStrength * 0.01f).Normalized();
			
			// Move towards player with jitter
			Position += finalDirection * Speed * (float)delta;
		}
	}
	
	// Method to handle being hit by a bullet
	public void TakeDamage()
	{
		// Notify the player of the kill
		if (player != null && player is Player playerScript)
		{
			playerScript.AddKill();
		}
		
		// Destroy the zombie
		QueueFree();
	}
} 
