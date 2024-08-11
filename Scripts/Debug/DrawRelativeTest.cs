using Godot;

public partial class DrawRelativeTest : RigidBody3D
{
  public override void _Process(double delta)
  {
    DebugDraw._Instance.EnableXRayMode();
    DebugDraw._Instance.DrawLineRelative(GlobalPosition, Vector3.Right * 2, Colors.Green);
  }
}
