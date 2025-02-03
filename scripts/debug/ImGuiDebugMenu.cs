using Godot;
using ImGuiNET;


public partial class ImGuiDebugMenu : Node
{
  public override void _Process(double delta)
  {
    ImGui.Begin("ImGui on Godot 4");
    ImGui.Text("hello world");
    ImGui.End();
  }
}
