using Godot;
using System;

public class SoundEffects : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    GameController gm;
    public AudioStreamPlayer win;
    public AudioStreamPlayer music;
    public AudioStreamPlayer die;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        gm = (GameController)GetNode("/root/SceneChanger");
        gm.SoundEffects = this;
        win = GetChild<AudioStreamPlayer>(2);
        music = GetChild<AudioStreamPlayer>(0);
        die = GetChild<AudioStreamPlayer>(1);

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
      public override void _Process(float delta)
      {
          
      }
    private void OnPlayerDiedLava(KinematicBody2D coll, Vector2 playerPosition)
    {
       // music.Stop();
       // die.Play();
    }
    private void _on_FinishPt_levelFinished()
    {
        music.Stop();
        win.Play();
    }


}




