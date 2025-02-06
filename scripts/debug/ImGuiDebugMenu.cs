using Godot;
using ImGuiNET;


public partial class ImGuiDebugMenu : Node
{
  public override void _Process(double delta)
  {
    ImGui.Begin("ImGui on Godot 4");
    ImGui.Text("hello world");
    ImGui.Text($"FPS: {Engine.GetFramesPerSecond()}");
    if (ImGui.Button("Quit")) GetTree().Quit();
    ImGui.End();
  }
}
