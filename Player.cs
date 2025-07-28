using Godot;
using System;

public partial class Player : Node2D
{
	[Export]
	public PackedScene ProjectileScene;
	
	[Export]
	public float Speed = 200f;
	
	public int KillCount = 0;

	public override void _Process(double delta)
	{
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
	}
	
	public void AddKill()
	{
		KillCount++;
		GD.Print($"Kill Count: {KillCount}");
	}
	
	private void ShootProjectile()
	{
		if (ProjectileScene == null) return;
		var projectile = (Node2D)ProjectileScene.Instantiate();
		var mousePos = GetViewport().GetMousePosition();
		var direction = (mousePos - GlobalPosition).Normalized();
		projectile.Position = GlobalPosition;
		((Projectile)projectile).direction = direction;
		GetParent().AddChild(projectile);
	}

} 
