using Godot;
using System;

public partial class Projectile : Node2D
{
	[Export]
	public float Speed = 400f;
	public Vector2 direction = Vector2.Zero;
	
	private Area2D collisionArea;

	public override void _Ready()
	{
		// Create collision area for the projectile
		collisionArea = new Area2D();
		var collisionShape = new CollisionShape2D();
		var circleShape = new CircleShape2D();
		circleShape.Radius = 5f; // Small collision radius
		collisionShape.Shape = circleShape;
		collisionArea.AddChild(collisionShape);
		AddChild(collisionArea);
		
		// Connect collision signal
		collisionArea.BodyEntered += OnBodyEntered;
		collisionArea.AreaEntered += OnAreaEntered;
	}

	public override void _Process(double delta)
	{
		Position += direction * Speed * (float)delta;
		if (!GetViewport().GetVisibleRect().HasPoint(GlobalPosition))
		{
			QueueFree();
		}
	}
	
	private void OnBodyEntered(Node2D body)
	{
		HandleCollision(body);
	}
	
	private void OnAreaEntered(Area2D area)
	{
		HandleCollision(area.GetParent() as Node2D);
	}
	
	private void HandleCollision(Node2D other)
	{
		// Check if we hit a zombie
		if (other is Basic_Zombie zombie)
		{
			zombie.TakeDamage();
			QueueFree(); // Destroy the projectile
		}
	}
}
