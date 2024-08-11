using System;
using Godot;

public partial class RotateCameraTest : Node3D
{
  private Node3D _CameraArmYaw;
  private Node3D _CameraArmPitch;
  // private Camera3D _Camera;

  private const float _RotationSpeed = MathF.PI / 2.0f;
  private const float _MouseSensitivity = 0.35f;
  private const float _MinYawAngle = -1.58f;
  private const float _MaxYawAngle = 1.58f;

  public override void _Ready()
  {
    Input.MouseMode = Input.MouseModeEnum.Captured;
    _CameraArmYaw = GetNode<Node3D>(".");
    _CameraArmPitch = GetNode<Node3D>("CameraArmPitch");
    // _Camera = GetNode<Camera3D>("CameraArmPitch/Camera3D");
  }

  public override void _Input(InputEvent @event)
  {
    if (@event is InputEventMouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured) ProcessMouseInput(@event as InputEventMouseMotion);
  }

  private void ProcessMouseInput(InputEventMouseMotion @event)
  {
    // if (@event.Relative.Y != 0)  _CameraArmPitch.Rotation += new Vector3(-@event.Relative.Y * _RotationSpeed * _MouseSensitivity * (float)GetProcessDeltaTime(), 0, 0);
    if (@event.Relative.X != 0) _CameraArmYaw.Rotation += new Vector3(0, -@event.Relative.X * _RotationSpeed * _MouseSensitivity * (float)GetProcessDeltaTime(), 0);
    if (@event.Relative.Y != 0)
    {
      // Calculate, clamp and apply new rotation
      float newPitchRotation = _CameraArmPitch.Rotation.X - @event.Relative.Y * _RotationSpeed * _MouseSensitivity * (float)GetProcessDeltaTime();
      newPitchRotation = Mathf.Clamp(newPitchRotation, _MinYawAngle, _MaxYawAngle);
      _CameraArmPitch.Rotation = new Vector3(newPitchRotation, _CameraArmPitch.Rotation.Y, _CameraArmPitch.Rotation.Z);
    }
  }

  public override void _PhysicsProcess(double delta)
  {
    ProcessKeyboardInput(delta);
    _CameraArmPitch.Rotation = new Vector3((float)Mathf.Clamp(_CameraArmPitch.Rotation.X, _MinYawAngle, _MaxYawAngle), 0, 0); // Also clamp here
  }

  private void ProcessKeyboardInput(double delta)
  {
    var yRot = -Input.GetAxis("Left", "Right");
    var xRot = Input.GetAxis("Up", "Down");
    _CameraArmYaw.Rotation += new Vector3(0, yRot * _RotationSpeed * (float)delta, 0);
    _CameraArmPitch.Rotation += new Vector3(xRot * _RotationSpeed * (float)delta, 0, 0);
  }
}
