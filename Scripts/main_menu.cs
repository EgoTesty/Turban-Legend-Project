using Godot;
using System;

public partial class main_menu : Control
{
	private void _on_start_pressed()
	{
		GetTree().ChangeSceneToFile("res://Player.tscn");
	}


	private void _on_continue_pressed()
	{
		// Replace with function body.
	}


	private void _on_settings_pressed()
	{
		// Replace with function body.
	}


	private void _on_quit_pressed()
	{
		GetTree().Quit();
	}
}
