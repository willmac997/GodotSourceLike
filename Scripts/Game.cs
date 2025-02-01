using Godot;

public partial class Game : Node3D
{
  private static bool _console = false;
  public static bool Console
  {
	get { return _console; }
	set { _console = value; }
  }

  public override void _Process(double delta)
  {
	if (Input.IsActionJustPressed("Escape")) GetTree().Quit();
  }
}
