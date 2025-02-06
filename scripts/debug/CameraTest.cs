using Godot;
using System;

public partial class CameraTest : Node3D
{
  private bool dragging = false;
  // private bool panning = false;
  // private bool orbiting = false;
  private float sensitivity = 0.5f;

  public override void _Input(InputEvent @event)
  {
    if (@event is InputEventMouseButton buttonEvent && buttonEvent.ButtonIndex == MouseButton.Right) dragging = buttonEvent.Pressed;
    // if (@event is InputEventMouseButton buttonEvent && buttonEvent.ButtonIndex == MouseButton.Right) panning = buttonEvent.Pressed;
    // if (@event is InputEventMouseButton buttonEvent && buttonEvent.ButtonIndex == MouseButton.Middle) orbiting = buttonEvent.Pressed;
    if (@event is InputEventMouseMotion motionEvent && dragging)
    {
      RotationDegrees = new Vector3(
        Mathf.Clamp(RotationDegrees.X - motionEvent.Relative.Y * sensitivity, -90, 90),
        RotationDegrees.Y - motionEvent.Relative.X * sensitivity,
        RotationDegrees.Z
      );
    }
  }
}
