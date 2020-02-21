using Godot;
using System;

public class StartGameBtn : Button
{
    GameController gm;
    public override void _Ready()
    {
        gm = (GameController)GetNode("/root/SceneChanger");
        gm.StartButton = this;
        Connect("pressed", gm, "onStartGamePressed");
    }

    public override void _Process(float delta)
    {
        buttonPressed();
    }
    public void buttonPressed()
    {
        if (Pressed)
        {

        }
    }
}
