using Godot;
using System;

public class EndGameBtn : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    GameController gm;
    [Signal]
    public delegate void goToMainMenu();
    public override void _Ready()
    {
        gm = (GameController)GetNode("/root/SceneChanger");
        Connect("goToMainMenu", gm, "onGameFinish");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    private void _on_Button_pressed()
{
        GetTree().Quit();
}

private void _on_mainMenu_pressed()
{
        EmitSignal("goToMainMenu");
}
}




