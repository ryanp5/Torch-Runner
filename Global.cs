using Godot;
using System;

public class Global : Node
{
    public Node CurrentScene { get; set; }
    KinematicBody2D player;
    KinematicBody2D miniPlayer;
    Button startGameBtn;
    bool secondChanceMode = false;
    float Diedelay = 0.5f;
    float miniDieDelay = 1.0f;
    public Vector2 lastPlayerPos;
    PlayerMovement pm;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Viewport root = GetTree().GetRoot();
        CurrentScene = root.GetChild(root.GetChildCount() - 1);
     
    }
       
}
