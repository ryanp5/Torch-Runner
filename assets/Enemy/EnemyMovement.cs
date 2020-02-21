using Godot;
using System;

public class EnemyMovement : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    Vector2 floor = new Vector2(0, -1);
    float enemySpeed = 100;
    float enemyFastSpeed = 200;
    Vector2 enemVelocity;
    [Export]
    public float EnemySightdist = 50f;
    public float gravity = 200f;
    RayCast2D checkfloorR;
    RayCast2D checkfloorL;
    RayCast2D checkForPlayer;
    AnimatedSprite enemySprite;
    bool Direction;
    bool died = false;
    GameController gm;
    [Signal]
    public delegate void playerCollidedEnemy(KinematicBody2D coll, Vector2 playerPosition);
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        gm = (GameController)GetNode("/root/SceneChanger");
        Connect("playerCollidedEnemy", gm, "OnPlayerDied");
        enemySprite = GetNode<AnimatedSprite>("AnimatedSprite");
        checkfloorR = GetNode<RayCast2D>("CollisionShape2D/RayCastDownRight");
        checkfloorL = GetNode<RayCast2D>("CollisionShape2D/RayCastDownLeft");
        checkForPlayer = GetNode<RayCast2D>("CollisionShape2D/RayCastFoward");
        enemVelocity.x = -enemySpeed;
        Direction = true;
        enemySprite.SetAnimation("walk");

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
  {
        if (died)
        {
            enemySprite.SetAnimation("die");
        }
        else
        {
            enemVelocity.y += gravity * delta;

            if (checkForPlayer.GetCollider() != null)
            {
                enemySprite.SetAnimation("chase");
                if (Direction)
                {
                    enemySprite.SetFlipH(true);

                    enemVelocity.x = -enemyFastSpeed;
                }
                else
                {
                    enemySprite.SetFlipH(false);

                    enemVelocity.x = enemyFastSpeed;

                }
                checkfloorL.Enabled = false;
                checkfloorR.Enabled = false;
            }
            else
            {
                enemySprite.SetAnimation("walk");

                checkfloorL.Enabled = true;
                checkfloorR.Enabled = true;
            }

            if (checkfloorL.GetCollider() == null)
            {

                enemVelocity.x = enemySpeed;
                enemySprite.SetFlipH(false);

                checkForPlayer.SetCastTo(new Vector2(EnemySightdist, 0));
                Direction = false;
            }
            else
            {
            }
            if (checkfloorR.GetCollider() == null)
            {
                checkForPlayer.SetCastTo(new Vector2(-EnemySightdist, 0));
                enemySprite.SetFlipH(true);
                enemVelocity.x = -enemySpeed;
                Direction = true;
            }
        }
      //  enemVelocity = enemVelocity * delta;
        MoveAndSlide(enemVelocity);
        checkCollision();
  }
    private void checkCollision()
    {
        KinematicCollision2D collision;
        
        for (int i = 0; i < GetSlideCount(); i++)
        {
            collision = GetSlideCollision(i);
            if (collision.Collider is TileMap)
            {
                TileMap collider = (TileMap)collision.Collider;

                //Collision Layer Bit 2 = Enemies layer 3 = Killable tiles
                if (collider.GetCollisionLayerBit(3))
                {
                    SetCollisionLayerBit(2, false);
                    died = true;
                    checkForPlayer.Enabled = false;
                    enemVelocity = new Vector2(0, 0);
                   

                }
            }
        }
    }
    private void _on_Area2D_body_entered(object body)
    {
        if (body is KinematicBody2D)
        {
           
            KinematicBody2D coll;
            coll = (KinematicBody2D)body;
            if (GetCollisionLayerBit(2))
            {
                SetCollisionLayerBit(2, false);
                EmitSignal("playerCollidedEnemy", coll, coll.GetPosition());
                SetCollisionLayerBit(2, false);
            }
        }
 
    }
}



