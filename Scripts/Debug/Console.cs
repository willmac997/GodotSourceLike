using Godot;

public partial class Console : Control
{
  private Window _Window;
  private PanelContainer _Panel;
  private TextEdit _OutputHistory;
  private LineEdit _UserInput;
  private Button _Submit;

  public override void _Ready()
  {
    _Window = GetNode<Window>("Canvas/Window");
    _Panel = GetNode<PanelContainer>("Canvas/Window/Panel");
    _OutputHistory = GetNode<TextEdit>("Canvas/Window/Panel/MainGrid/OutputHistory");
    _UserInput = GetNode<LineEdit>("Canvas/Window/Panel/MainGrid/InputGrid/UserInput");
    _Submit = GetNode<Button>("Canvas/Window/Panel/MainGrid/InputGrid/Submit");
  }

  public override void _Input(InputEvent @event)
  {
    // Strange input doesnt register when focused on the window
    if (@event is InputEventKey keyEvent)
    {
      if (keyEvent.Pressed && keyEvent.Keycode == Key.Quoteleft)
      {
        ToggleConsole();
      }
    }
  }

  private void ToggleConsole()
  {
    _Window.Visible = !_Window.Visible;
    if (!_Window.Visible) Input.MouseMode = Input.MouseModeEnum.Captured;
    else Input.MouseMode = Input.MouseModeEnum.Visible;
    CallDeferred("FocusUserInput");
  }

  private void FocusUserInput()
  {
    _UserInput.GrabFocus();
  }

  private void _OnSubmitEnter(string newText)
  {
    ExecuteCommand(newText);
  }

  private void _OnSubmitPressed()
  {
    ExecuteCommand(_UserInput.Text);
  }

  private void ExecuteCommand(string command = "")
  {
    _OutputHistory.Text += $"> {command}\n";
    _UserInput.Text = "";
  }
}
