using Godot;
using System;

public partial class Infinite_Scrolling : Camera2D
{
	public override void _Process(double delta)
	{
		float n = 1;
		this.Position += new Vector2(n, 0);
	}
}
