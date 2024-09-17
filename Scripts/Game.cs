using Godot;

public partial class Game : Node3D
{
  public override void _Process(double delta)
  {
	if (Input.IsActionJustPressed("Escape"))
	{
	  if (Input.MouseMode == Input.MouseModeEnum.Visible) GetTree().Quit();
	  Input.MouseMode = Input.MouseModeEnum.Visible;
	}
  }
}
