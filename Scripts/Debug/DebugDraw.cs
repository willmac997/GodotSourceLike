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

  public void DrawLine(Vector3 pointA, Vector3 pointB, float thickness = 0, Color color = default)
  {
    // converted from GDScript https://youtu.be/UrENObFwQVo?si=YD2GaTfEcHHP2_5o&t=301

    if (pointA.IsEqualApprox(pointB)) return;
    if (_DrawDebug.Mesh is ImmediateMesh)
    {
      ImmediateMesh mesh = (ImmediateMesh)_DrawDebug.Mesh;
      if (thickness > 0)
      {
        mesh.SurfaceBegin(Mesh.PrimitiveType.TriangleStrip);
        float scaleFactor = 100.0f;
        Vector3 direction = pointA.DirectionTo(pointB);
        float epsilon = 0.00001f;

        // Draw cube line
        Vector3 normal = Mathf.Abs(Mathf.Abs(direction.X) + Mathf.Abs(direction.Y)) > epsilon ?
        new Vector3(-direction.Y, direction.X, 0).Normalized() :
        new Vector3(0, -direction.Z, direction.Y).Normalized();
        normal *= thickness / scaleFactor;

        int[] verticesStripOrder = new int[] { 4, 5, 0, 1, 2, 5, 6, 4, 7, 0, 3, 2, 7, 6 };
        Vector3 localB = pointB - pointA;
        // Calculates line mesh origin
        for (int i = 0; i < verticesStripOrder.Length; i++)
        {
          Vector3 vertex = verticesStripOrder[i] < 4 ? normal : normal / 3.0f + localB;
          Vector3 finalVert = vertex.Rotated(direction, Mathf.Pi * (0.5f * (verticesStripOrder[i] % 4) + 0.25f));
          finalVert += pointA;
          mesh.SurfaceAddVertex(finalVert);
        }
      }
      else
      {
        mesh.SurfaceBegin(Mesh.PrimitiveType.Lines);
        mesh.SurfaceAddVertex(pointA);
        mesh.SurfaceAddVertex(pointB);
      }
      mesh.SurfaceSetColor(color);
      mesh.SurfaceEnd();
    }
  }

  public void DrawLineRelative(Vector3 pointA, Vector3 pointB, float thickness = 0, Color color = default)
  {
    DrawLine(pointA, pointA + pointB, thickness, color);
  }
}
