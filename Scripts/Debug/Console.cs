using Godot;

public partial class Console : Control
{
  private CanvasLayer _Canvas;
  private PanelContainer _Panel;
  private TextEdit _OutputHistory;
  private LineEdit _UserInput;
  private Button _Submit;

  public override void _Ready()
  {
    _Canvas = GetNode<CanvasLayer>("Canvas");
    _Panel = GetNode<PanelContainer>("Canvas/Panel");
    _OutputHistory = GetNode<TextEdit>("Canvas/Panel/Margin/MainGrid/OutputHistory");
    _UserInput = GetNode<LineEdit>("Canvas/Panel/Margin/MainGrid/InputGrid/UserInput");
    _Submit = GetNode<Button>("Canvas/Panel/Margin/MainGrid/InputGrid/Submit");
  }

  public override void _Input(InputEvent @event)
  {
    if (@event is InputEventKey keyEvent)
    {
      if (keyEvent.Pressed && keyEvent.Keycode == Key.Quoteleft)
      {
        _Canvas.Visible = !_Canvas.Visible;
        if (!_Canvas.Visible) Input.MouseMode = Input.MouseModeEnum.Captured;
        else Input.MouseMode = Input.MouseModeEnum.Visible;
        CallDeferred("FocusUserInput");
      }
    }
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
