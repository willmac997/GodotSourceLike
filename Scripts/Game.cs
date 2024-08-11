using Godot;
using System;

public partial class Game : Node3D
{
  public override void _Process(double delta)
  {
	if (Input.IsActionJustPressed("Escape")) GetTree().Quit();
  }
}
