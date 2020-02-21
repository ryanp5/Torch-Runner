using Godot;
using System;

public class EndLevel : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    AnimatedSprite torchAnim;
    GameController gm;
    [Signal]
    public delegate void levelFinished();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        gm = (GameController)GetNode("/root/SceneChanger");
        Connect("levelFinished", gm, "onLevelFinished");
        
        //send to player 
        Connect("levelFinished", gm.player, "onLevelFinish");
        torchAnim = GetNode<AnimatedSprite>("AnimatedSprite");

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    private void OnAreaEntered(object body)
    {
        torchAnim.Play("lit");
        EmitSignal("levelFinished");
    }

}


