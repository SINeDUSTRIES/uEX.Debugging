using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace SINeDUSTRIES.Unity.Debugging
{
  /// <summary>
  /// A console to display Unity's debug logs in-game.
  /// </summary>
  /// <credit>https://gist.github.com/mminer/975374</credit>
  [SortBefore(typeof(MonoBehaviour))]
  public class ConsoleGUI : MonoBehaviourSingleton<ConsoleGUI>
  {
    #region Public, Methods

    /// <summary>
    /// Invert <see cref="IsVisible"/>;
    /// </summary>
    public void VisibleToggle()
    => IsVisible = !IsVisible;

    #endregion

    #region Public, Properties

    /// <summary>
    /// Showing the GUI?
    /// </summary>
    static public bool IsVisible
    {
      set
      {
        _isVisible = value;
      }

      get
      {
        return _isVisible;
      }
    }

    #endregion

    #region Protected, Methods

    /// <summary>
    /// <see cref="VisibleToggle"/> if <see cref="keyToggle"/>;
    /// </summary>
    protected void Update()
    {
      if (Input.GetKeyDown(this.keyToggle))
      {
        VisibleToggle();
        //_show = !_show;
      }
    }

    #endregion

    #region Private

    private void OnGUI()
    {
      if (IsVisible)
      {
        _rectWindow = GUILayout.Window(123456, _rectWindow, consoleWindow, "Console");
      }
    }

    /// <summary>
    /// A window that displayss the recorded logs.
    /// </summary>
    /// <param name="windowID">Window ID.</param>
    private void consoleWindow(Int32 windowID)
    {
      _scrollPosition = GUILayout.BeginScrollView(_scrollPosition); // scroll position

      // Iterate through the recorded logs.
      for (Int32 i = 0; i < _logs.Count; i++)
      {
        var log = _logs[i];

        // Combine identical messages if collapse option is chosen.
        if (_collapse)
        {
          var messageSameAsPrevious = i > 0 && log._Message == _logs[i - 1]._Message;

          if (messageSameAsPrevious)
          {
            continue;
          }
        }

        GUI.contentColor = _logTypeColors_[log._Type];
        GUILayout.Label(log._Message);

        if (_stackTrace)
        {
          // stack trace
          for (Int32 i_stack = 0; i_stack < log._StackTrace.Length; i_stack++)
          {
            GUILayout.BeginHorizontal();

            GUILayout.Space(TAB);
            GUILayout.Label(log._StackTrace[i_stack]);

            GUILayout.EndHorizontal();
          }
        }

        //GUILayout.Label(log._StackTrace);
      }

      GUILayout.EndScrollView(); // end scroll

      GUI.contentColor = Color.white;

      GUILayout.BeginHorizontal();

      if (GUILayout.Button(_labelClear))
      {
        _logs.Clear();
      }

      _collapse = GUILayout.Toggle(_collapse, _labelCollapse, GUILayout.ExpandWidth(false));

      _stackTrace = GUILayout.Toggle(_stackTrace, _labelStackTrace, GUILayout.ExpandWidth(false));

      GUILayout.EndHorizontal();

      // Allow the window to be dragged by its title bar.
      GUI.DragWindow(_rectTitleBar);
    }

    /// <summary>
    /// Records a log from the log callback.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="stackTrace">Trace of where the message came from.</param>
    /// <param name="type">Type of message (error, exception, warning, assert).</param>
    private void handleLog(string message, string stackTrace, LogType type)
    {
      _logs.Add(new Log()
      {
        _Message = message,
        _StackTrace = stackTrace.ToLinesWhereNotWhitespace().ToArray(),
        _Type = type,
      });
    }

    #endregion

    #region Private, Fields, Unity

    /// <summary>
    /// The hotkey to show and hide the console window.
    /// </summary>
    [SerializeField] KeyCode keyToggle = KeyCode.BackQuote;

    [SerializeField] bool initialState = true;

    #endregion

    #region Private, Fields, Static

    // options 

    static private List<Log> _logs = new List<Log>();
    static private Vector2 _scrollPosition;
    static private bool _isVisible;
    static private bool _collapse;
    static private bool _stackTrace = false;

    // visual elements

    static private Rect _rectWindow = new Rect(MARGIN, MARGIN, Screen.width - (MARGIN * 2), Screen.height - (MARGIN * 2));
    static private Rect _rectTitleBar = new Rect(0, 0, 10000, 20);
    static private GUIContent _labelClear = new GUIContent("Clear", "Clear the contents of the console.");
    static private GUIContent _labelCollapse = new GUIContent("Collapse", "Hide repeated messages.");
    static private GUIContent _labelStackTrace = new GUIContent("Stack trace", "Show stack trace.");

    #endregion

    #region Lifecycle

    /// <summary>
    /// Register event handlers;
    /// </summary>
    override protected void awakeFirst()
    {
      base.awakeFirst();

      Application.logMessageReceived += handleLog;

      _isVisible = this.initialState;
    }

    /// <summary>
    /// Unregister event handlers;
    /// </summary>
    override protected void onDestroySingleton()
    {
      base.onDestroySingleton();

      Application.logMessageReceived -= handleLog;
    }

    #endregion

    #region Static

    static private readonly Dictionary<LogType, Color> _logTypeColors_ = new Dictionary<LogType, Color>()
    {
      { LogType.Assert, Color.white },
      { LogType.Error, Color.red },
      { LogType.Exception, Color.red },
      { LogType.Log, Color.white },
      { LogType.Warning, Color.yellow },
    };

    private const Int32 MARGIN = 20;

    private const Int32 TAB = 20;

    #endregion

    struct Log
    {
      public string _Message;
      public string[] _StackTrace;
      public LogType _Type;
    }
  }
}