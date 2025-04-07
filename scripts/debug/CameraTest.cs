using Godot;
using System;

public partial class CameraTest : Node3D
{
  private Camera3D camera;
  [Export] private float panSensitivity = 0.05f;
  [Export] private float orbitSensitivity = 0.35f;
  [Export] private float zoomStep = 0.5f;
  [Export] private float zoomLevel = 10f;
  private bool panning = false;
  private bool orbiting = false;

  public override void _Ready()
  {
    camera = GetNode<Camera3D>("Camera3D");
    camera.Position = new Vector3(0, 0, zoomLevel);
  }

  public override void _Input(InputEvent @event)
  {
    if (@event is InputEventMouseButton buttonEvent)
    {
      if (buttonEvent.ButtonIndex == MouseButton.Middle) panning = buttonEvent.Pressed;
      if (buttonEvent.ButtonIndex == MouseButton.Right) orbiting = buttonEvent.Pressed;

      // should we increase or decrease the step depending on the zoom level?
      zoomLevel += (buttonEvent.ButtonIndex == MouseButton.WheelDown) ? zoomStep : (buttonEvent.ButtonIndex == MouseButton.WheelUp) ? -zoomStep : 0f;
      zoomLevel = Mathf.Clamp(zoomLevel, .5f, 50f);
      camera.Position = new Vector3(0, 0, zoomLevel);
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
