using Godot;
using System;

public partial class CameraTest : Node3D
{
  private float panSensitivity = 0.05f;
  private float orbitSensitivity = 0.35f;
  private bool panning = false;
  private bool orbiting = false;
  // private bool zooming = false;
  // private bool pivoting = false;

  public override void _Input(InputEvent @event)
  {
    if (@event is InputEventMouseButton buttonEvent)
    {
      if (buttonEvent.ButtonIndex == MouseButton.Middle) panning = buttonEvent.Pressed;
      if (buttonEvent.ButtonIndex == MouseButton.Right) orbiting = buttonEvent.Pressed;
    }

    if (@event is InputEventMouseMotion motionEvent)
    {
      if (panning)
      {
        // Calculate local panning direction
        Vector3 right = -GlobalTransform.Basis.X * motionEvent.Relative.X * panSensitivity;
        Vector3 up = GlobalTransform.Basis.Y * motionEvent.Relative.Y * panSensitivity;
        Position += right + up; // Update position using local axes
      }
      if (orbiting)
      {
        RotationDegrees = new Vector3(
          Mathf.Clamp(RotationDegrees.X - motionEvent.Relative.Y * orbitSensitivity, -90, 90),
          RotationDegrees.Y - motionEvent.Relative.X * orbitSensitivity,
          RotationDegrees.Z
        );
      }
    }
  }
}
