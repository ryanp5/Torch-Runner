using Godot;
using System;

public class TileMover : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
	[Export]
    public float tileSpeed = 200;
    public Vector2 velocity;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        velocity.y = tileSpeed;
        Translate(velocity * delta);
    }
    private void _on_PlayerMini_gameOverSignal()
    {
        tileSpeed = 0;
    }

    private void onPlayerCollectHeart(int body_id, object body, int body_shape, int area_shape)
    {
        tileSpeed = 0;
    }
}








