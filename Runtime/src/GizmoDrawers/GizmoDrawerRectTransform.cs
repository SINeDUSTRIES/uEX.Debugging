using UnityEngine;

namespace SINeDUSTRIES.Unity
{
  /// <summary>
  /// GizmoDrawer for a <see cref="RectTransform"/>;
  /// </summary>
  public class GizmoDrawerRectTransform : GizmoDrawerRectCorners<RectTransform>
  {
    #region Protected, Methods

    /// <summary>
    /// <see cref="MonoBehaviour"/> message;
    /// </summary>
    virtual protected void Update()
    {
      if (this._component.transform.hasChanged) // if the RectTransform has changed
      {
        cornersCache(); // corners on the RecTransform
      }
    }

    /// <summary>
    /// Get <see cref="RectCorners"/> of this <see cref="RectTransform"/>;
    /// </summary>
    override protected RectCorners cornersGet()
    => _component.GetWorldCornersStruct();

    #endregion

    #region Lifecycle

    /// <summary>
    /// <see cref="GizmoDrawer{TComponent}.resetColorDefaultGet"/>;
    /// </summary>
    override protected Color resetColorDefaultGet()
    => Color.white;

    #endregion
  }
}