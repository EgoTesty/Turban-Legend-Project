using Godot;
using System;

public partial class Player : Area2D
{
	private void _on_body_entered(Node2D body)
	{
		EmitSignal(SignalName.Hit);
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}
	public void Start(Vector2 position)
	{
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
	[Signal]
	public delegate void HitEventHandler(); // approaching an enemy
	
	[Export]
	public int speed = 120;
	
	public Vector2 ScreenSize;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	//Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero;
		if (Input.IsActionPressed("move_up"))
			velocity.Y -= 1;
		if (Input.IsActionPressed("move_down"))
			velocity.Y += 1;
		if (Input.IsActionPressed("move_left"))
			velocity.X -= 1;
		if (Input.IsActionPressed("move_right"))
			velocity.X += 1;
		
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}
		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
		// diagonal sprites
		if (velocity.X < 0 && velocity.Y < 0)
			animatedSprite2D.Animation = "north west walk";
		else if (velocity.X > 0 && velocity.Y < 0)
			animatedSprite2D.Animation = "north east walk";
		else if (velocity.X > 0 && velocity.Y > 0)
			animatedSprite2D.Animation = "south east walk";
		else if (velocity.X < 0 && velocity.Y > 0)
			animatedSprite2D.Animation = "south west walk";
		//cardinal sprites
		if (velocity.X < 0 && velocity.Y == 0)
			animatedSprite2D.Animation = "west walk";
		if (velocity.X > 0 && velocity.Y == 0)
			animatedSprite2D.Animation = "east walk";
		if (velocity.Y < 0 && velocity.X == 0)
			animatedSprite2D.Animation = "north walk";
		if (velocity.Y > 0 && velocity.X == 0)
			animatedSprite2D.Animation = "south walk";
	}
}
