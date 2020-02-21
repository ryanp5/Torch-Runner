using Godot;
using System;

public class MiniGameCamera : Camera2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    KinematicBody2D player;
    Vector2 playerpos;
    float t;
    Vector2 camPos = new Vector2 (1,1);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetZoom(camPos);
    }
    //Lerp from Zoom of Camera 1 to 0.4
    public override void _Process(float delta)
    {
        t += delta;
        if (t < 1)
        {
            SetZoom(new Vector2(Mathf.Lerp(1.0f, 0.3f, t), Mathf.Lerp(1.0f, 0.3f, t)));
        }
    }
}
