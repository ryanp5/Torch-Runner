using Godot;
using System;

public class PlayerMovement : KinematicBody2D
{
    //Movement Variables
    public Vector2 velocity;
    Vector2 floor = new Vector2(0, -1);
    [Export]
    public float XmaxVelocity = 500;
    [Export]
    public float gravity = 1500; //1500 *delta
    [Export]
    public float jumpVelocity = 650f; //650 *delta
    [Export]
    public float MinJumpTime = 0.05f;
    [Export]
    public float MaxJumpTime = 500.0f;
    [Export]
    public float accelConstant = 0.98f;
    public float deaccelConstant = 6.0f;

    bool onGround;
    public bool inputEnabled = true;
    public bool finished = false;

    //player collider
    CollisionShape2D playerColl;
    //State altering signals
    [Signal]
    public delegate void playerCollided(KinematicBody2D coll, Vector2 playerPosition);
    [Signal]
    public delegate void playerJump(float yVelocity);
    [Signal]
    public delegate void playerMoving(float xVelocity);
    [Signal]
    public delegate void playerOnGround(KinematicBody2D grounded);
    GameController gm;
    //Camera adjustments
    AnimationPlayer camera;
    //Raycast for ground check
    RayCast2D groundRay;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        gm = (GameController)GetNode("/root/SceneChanger");
        gm.player = this;
        SetPosition(gm.respawnPt);
        camera = GetNode<AnimationPlayer>("Camera2D/AnimationPlayer");
        //connect signal to gameController
        Connect("playerCollided", gm, "OnPlayerDied");
        playerColl = GetNode<CollisionShape2D>("CollisionShape2D");
        groundRay = GetNode<RayCast2D>("GroundRaycast");
        velocity = new Vector2(0, 0);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

        if (groundRay.IsColliding())
        {
            EmitSignal("playerOnGround", false);
        } else
        {
            EmitSignal("playerOnGround", true);

        }
        if (IsOnFloor())
        {
            onGround = true;
        }
        else
        {
            onGround = false;
            velocity.y += gravity * delta;


        }
        if (inputEnabled)
        {
            input(delta);


        }
        else
        {
            velocity.x = 0;

        }


        velocity = MoveAndSlide(velocity, floor, false,2,0.785398f,false);

            checkCollision();
        

    }

    void input(float delta)
    {
        
        if (Input.IsActionPressed("MoveRight"))
        {
            velocity.x = Mathf.Lerp(velocity.x, Mathf.Clamp(velocity.x += 1.0f, 0, XmaxVelocity), accelConstant);
            EmitSignal("playerMoving",velocity.x, true);

        }
        else if (Input.IsActionPressed("MoveLeft"))
        {

            velocity.x = Mathf.Lerp(velocity.x, Mathf.Clamp(velocity.x -= 1.0f, -XmaxVelocity, 0), accelConstant);
            EmitSignal("playerMoving", velocity.x, true);

        }
        else
        {
         
            velocity.x = Mathf.Lerp(velocity.x, 0, delta * deaccelConstant);
            EmitSignal("playerMoving", velocity.x, false);

        }
        if (Input.IsActionPressed("Jump"))
        {
            jump();
        }
    }

    private void jump()
    {
        if (onGround)
        {
            velocity.y -= jumpVelocity;
            EmitSignal("playerJump", velocity.y);

        }
    }
    
    private void checkCollision()
    {

        KinematicCollision2D collision;
        
        for (int i = 0; i < GetSlideCount(); i++)
        { 
            
            collision = GetSlideCollision(i);
          /*  if (collision.Collider is KinematicBody2D)
            {
                KinematicBody2D collider = (KinematicBody2D)collision.Collider;

                //Collision Layer Bit 2 = Enemies layer 3 = Killable tiles
                if (collider.GetCollisionLayerBit(2) || collider.GetCollisionLayerBit(3))
                {
                    EmitSignal("playerCollided", collider, GetPosition());
                    GD.Print("enemyCollided");
                    collider.SetCollisionLayerBit(2, false);
                    
                    inputEnabled = false;
                }
            } */
             if (collision.Collider is TileMap)
            {
                TileMap tile = (TileMap)collision.Collider;
                if (tile.GetCollisionLayerBit(2) || tile.GetCollisionLayerBit(3))
                {
                    EmitSignal("playerCollided", tile, GetPosition());
                    tile.SetCollisionLayerBit(3, false);
                    inputEnabled = false;


                }
            }
        }
    }
    private void onLevelFinish()
    {
        camera.Play("CameraZoom");

        finished = true;
        inputEnabled = false;
    }
}
