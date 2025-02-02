using Godot;
using System;
using ImGuiGodot;
using ImGuiNET;


public partial class MyNode : Node
{
	public override void _Process(double delta)
	{
		ImGui.Begin("ImGui on Godot 4");
		ImGui.Text("hello world");
		ImGui.End();
	}
}
