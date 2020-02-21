using Godot;
using System;

public class MiniGameSoundEffects : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    AudioStreamPlayer win;
    AudioStreamPlayer music;
    AudioStreamPlayer die;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        win = GetChild<AudioStreamPlayer>(1);
        music = GetChild<AudioStreamPlayer>(0);
        die = GetChild<AudioStreamPlayer>(2);

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    private void _on_PlayerMini_gameOverSignal()
    {
        music.Stop();
        die.Play();
    }
    private void onHeartCollected(int body_id, object body, int body_shape, int area_shape)
    {
        music.Stop();
        win.Play();
    }
}






