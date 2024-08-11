using Godot;

public partial class DebugDraw : Node
{
  // Autoloads in C# global variables so we have to use a singleton as below
  public static DebugDraw _Instance { get; private set; }
  private MeshInstance3D _DrawDebug;

  public override void _Ready()
  {
    _Instance = this;
    _DrawDebug = GetNode<MeshInstance3D>("MeshInstance3D");
  }

  public override void _PhysicsProcess(double delta)
  {
    if (_DrawDebug.Mesh is ImmediateMesh)
    {
      ImmediateMesh mesh = (ImmediateMesh)_DrawDebug.Mesh;
      mesh.ClearSurfaces();
    }
  }

  public void EnableXRayMode()
  {
    _DrawDebug.MaterialOverride.Set("no_depth_test", true);
  }

  public void DrawLine(Vector3 pointA, Vector3 pointB, Color color = default)
  {
    // TODO:
    // Continue from https://youtu.be/UrENObFwQVo?si=YD2GaTfEcHHP2_5o&t=301

    if (pointA.IsEqualApprox(pointB)) return;
    if (_DrawDebug.Mesh is ImmediateMesh)
    {
      ImmediateMesh mesh = (ImmediateMesh)_DrawDebug.Mesh;
      mesh.SurfaceBegin(Mesh.PrimitiveType.Lines);
      mesh.SurfaceSetColor(color);
      mesh.SurfaceAddVertex(pointA);
      mesh.SurfaceAddVertex(pointB);
      mesh.SurfaceEnd();
    }
  }

  public void DrawLineRelative(Vector3 pointA, Vector3 pointB, Color color = default)
  {
    DrawLine(pointA, pointA + pointB, color);
  }
}
