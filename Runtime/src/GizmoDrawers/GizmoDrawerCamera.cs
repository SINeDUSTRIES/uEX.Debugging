using UnityEngine;

namespace SINeDUSTRIES.Unity.GizmoDrawers
{
  /// <summary>
  /// Script for always showing tthe "frustrum" of the attatched camera;
  /// </summary>
  [ExecuteInEditMode]
  public class GizmoDrawerCamera : GizmoDrawer<Camera>
  {
    /// <summary>
    /// Draw frustrum;
    /// Overrides <see cref="GizmoDrawer{TComponent}.onDrawGizmosComponent"/>;
    /// </summary>
    override protected void onDrawGizmosComponent()
    {
      if (_component.orthographic)
      {
        Debug.LogWarning("GizmoDrawerCamera: Orthographic perpective not yet supported");

        // TODO: do this!

        // need to account for rotation
        // best
        //  struct to contain a boxes corners (CornersCuboid)
        //  util method to convert orthographic camera into that box CornersCuboid
        //  UtilGizmo.DrawCornersCuboid(this CornersCuboid)

        // rotate poInt32 around pivot: https://answers.unity.com/questions/532297/rotate-a-vector-around-a-certain-point.html
      }
      else
      {
        // http://answers.unity3d.com/questions/1159913/setting-to-show-camera-cone-always.html

        Matrix4x4 temp = Gizmos.matrix;

        Gizmos.matrix = _component.transform.localToWorldMatrix;
        Gizmos.DrawFrustum(new Vector3(0, 0, _component.nearClipPlane),
          _component.fieldOfView,
          _component.farClipPlane /*+ _component.transform.position.z*/,
          _component.nearClipPlane,
          _component.aspect
        );

        Gizmos.matrix = temp;
      }
    }

    /// <summary>
    /// <see cref="GizmoDrawer{TComponent}.resetColorDefaultGet"/>;
    /// </summary>
    override protected Color resetColorDefaultGet()
    => Color.yellow;
  }
}