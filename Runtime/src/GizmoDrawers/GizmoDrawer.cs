using UnityEngine;

namespace SINeDUSTRIES.Unity
{
  /// <summary>
  /// <see cref="Component"/> which draws <see cref="Gizmos"/> for other <see cref="Component"/>;
  /// </summary>
  /// <typeparam name="TComponent">Type of component to draw for;</typeparam>
  [ExecuteInEditMode]
  public abstract class GizmoDrawer<TComponent> : MonoBehaviour
    where TComponent : Component
  {
    // TODO: gizmo drawer for ScrollRect

    #region Protected, Methods

    /// <summary>
    /// <see cref="MonoBehaviour"/> message;
    /// </summary>
    virtual protected void OnDrawGizmos()
    {
      Color old = Gizmos.color; // cache current color

      Gizmos.color = colorMain; // set color

      if // if
      (
        this._component != null && // component is not null
        this.isActiveAndEnabled
      )
      {
        onDrawGizmosComponent(); // draw the gizmo for the component
      }

      Gizmos.color = old; // restore
    }

    /// <summary>
    /// Draw the gizmo for the attatched component;
    /// </summary>
    abstract protected void onDrawGizmosComponent();

    #endregion

    #region Protected, Properties

    /// <summary>
    /// The color of thhis Gizmo;
    /// </summary>
    protected Color colorMain
    => _colorMain;

    #endregion

    #region Private, Fields

    /// <summary>
    /// The component drawing a Gizmo for;
    /// </summary>
    [SerializeField] protected TComponent _component;

    #endregion

    #region Private, Fields

    /// <summary>
    /// Editable Main Color;
    /// </summary>
    [SerializeField] Color _colorMain;

    #endregion

    #region Lifecycle

    /// <summary>
    /// <see cref="MonoBehaviour"/> message;
    /// </summary>
    virtual protected void Reset()
    {
      _component = GetComponent<TComponent>(); // get the component

      _colorMain = resetColorDefaultGet(); // get 
    }

    /// <summary>
    /// Reset the color;
    /// Default color drawn by gizmos of this type;
    /// </summary>
    /// <notes>
    /// Merely a preference by ~me~;
    /// </notes>
    abstract protected Color resetColorDefaultGet();

    #endregion
  }
}