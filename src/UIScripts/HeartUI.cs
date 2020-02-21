using Godot;
using System;

public class HeartUI : AnimatedSprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    GameController gm;
    PlayerMovement player;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        gm = (GameController)GetNode("/root/SceneChanger");
        gm.player.Connect("playerCollided", this, "playerDied");

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
      public override void _Process(float delta)
      {
          if (gm.playerDiedToEnemy)
        {
            Play("HeartBreak");
        }
    }
    void playerDied(KinematicBody2D collider, Vector2 pos)
    {

        Play("HeartBreak");
    }
}
