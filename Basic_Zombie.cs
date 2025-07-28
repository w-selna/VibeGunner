using Godot;
using System;

public partial class Basic_Zombie : Node2D
{
	[Export]
	public float Speed = 50f; // Slow movement speed
	
	[Export]
	public float DetectionRange = 1000f; // How far the zombie can "see" the player
	
	private Node2D player;
	private Sprite2D sprite;
	private Area2D collisionArea;
	
	public override void _Ready()
	{
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
	}
	
	public override void _Process(double delta)
	{
		if (player == null) return;
		
		// Calculate direction to player
		Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
		float distance = GlobalPosition.DistanceTo(player.GlobalPosition);
		
		// Only move if player is within detection range
		if (distance <= DetectionRange)
		{
			// Move towards player
			Position += direction * Speed * (float)delta;
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
