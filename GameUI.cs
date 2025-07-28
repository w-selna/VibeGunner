using Godot;
using System;

public partial class GameUI : Control
{
    private Label killCountLabel;
    private Player player;
    
    public override void _Ready()
    {
        // Find the player
        player = GetNode<Player>("../Player");
        
        // Create kill count label
        killCountLabel = new Label();
        killCountLabel.Text = "Kills: 0";
        killCountLabel.Position = new Vector2(10, 10);
        killCountLabel.AddThemeColorOverride("font_color", new Color(1, 1, 1, 1)); // White text
        AddChild(killCountLabel);
    }
    
    public override void _Process(double delta)
    {
        if (player != null)
        {
            killCountLabel.Text = $"Kills: {player.KillCount}";
        }
    }
} 