using UnityEngine;

namespace SINeDUSTRIES.Unity
{
  /// <summary>
  /// GizmoDrawer for something with <see cref="RectCorners"/>;
  /// </summary>
  public abstract class GizmoDrawerRectCorners<TComponent> : GizmoDrawer<TComponent>
    where TComponent : Component
  {
    #region Protected, Method

    /// <summary>
    /// <see cref="GizmoDrawer{TComponent}.onDrawGizmosComponent"/>;
    /// </summary>
    override protected void onDrawGizmosComponent()
    {
      _corners.GizmoDraw(); // draw the gizmo for the corners
    }

    /// <summary>
    /// Get current <see cref="RectCorners"/>;
    /// </summary>
    abstract protected RectCorners cornersGet();

    /// <summary>
    /// Cache <see cref="_corners"/>;
    /// </summary>
    /// <remarks>
    /// should be invoked by the derived type whenever the corners changed;
    /// </remarks>
    protected void cornersCache()
    {
      _corners = cornersGet();
    }

    #endregion

    #region Protected, Fields

    /// <summary>
    /// Cache of the corners to draw;
    /// </summary>
    [SerializeField]
    [HideInInspector]
    protected RectCorners _corners;

    #endregion
  }
}