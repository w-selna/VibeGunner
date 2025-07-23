using Godot;
using System;

public partial class Projectile : Node2D
{
	[Export]
	public float Speed = 400f;
	public Vector2 direction = Vector2.Zero;

	public override void _Process(double delta)
	{
		Position += direction * Speed * (float)delta;
		if (!GetViewport().GetVisibleRect().HasPoint(GlobalPosition))
		{
			QueueFree();
		}
	}
}
