using Godot;
using System;

public class PlayerMiniMovement : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public Vector2 velocity;
    CollisionShape2D playerMiniColl;
    AnimatedSprite playerMiniSprite;
    bool inputEnabled = false;
    [Export]
    public float xspeed = 200;
    [Signal]
    public delegate void gameOverSignal();
    GameController gm;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerMiniSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        gm = (GameController)GetNode("/root/SceneChanger");
        gm.miniPlayer = this;
        Connect("gameOverSignal", gm, "_on_PlayerMini_gameOverSignal");
        playerMiniColl = GetNode<CollisionShape2D>("CollisionShape2D");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (!inputEnabled)
        {
            if (Input.IsActionPressed("MoveRight"))
            {
                velocity.x = xspeed;
            }
            else if (Input.IsActionPressed("MoveLeft"))
            {
                velocity.x = -xspeed;
            }
            else
            {
                velocity.x = 0;
            }
        }
        else
        {
            velocity.x = 0;
        }

        KinematicCollision2D coll = MoveAndCollide(velocity * delta);
        checkCollision(coll);
    }
    private void checkCollision(KinematicCollision2D coll)
    {
        if (coll != null)
        {
            EmitSignal("gameOverSignal");
            playerMiniSprite.SetAnimation("Explode");
            inputEnabled = true;
            playerMiniColl.SetDisabled(true);
        }
       
    }
    private void OnHeartCollected(int body_id, object body, int body_shape, int area_shape)
    {
        playerMiniSprite.SetVisible(false);
        inputEnabled = true;
    }
}



