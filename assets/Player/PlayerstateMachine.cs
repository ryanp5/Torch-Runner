using Godot;
using System;

public class PlayerstateMachine : Node
{
    public KinematicBody2D player;
    AnimatedSprite playerSprite;
    AnimationPlayer animationPlayer;
    PlayerMovement playerScript;
    GameController gm;
    SoundEffects sound;
    AnimationPlayer cameraAnim;
    Particles2D dustWalk;
    bool died = false;
    bool walking = false;
    bool inAir;
    bool landed = false;
    bool jump = false;
    bool left;
    bool right;

    public float jumpTime = 0.5f;
    public override void _Ready()
    {
        cameraAnim = GetNode<AnimationPlayer>("/root/scene1/Player/Camera2D/AnimationPlayer");
        sound = GetNode<SoundEffects>("/root/scene1/SoundNode");
        gm = (GameController)GetNode("/root/SceneChanger");
        playerSprite = GetParent().GetNode<AnimatedSprite>("AnimatedSprite");
        player = GetParent<KinematicBody2D>();
        playerScript = GetParent<PlayerMovement>();
        dustWalk = GetParent().GetNode<Particles2D>("WalkParticles");
        
        player.Connect("playerCollided", this, "onDeath");
        player.Connect("playerJump", this, "onJumping");
        player.Connect("playerMoving", this, "onMove");
        player.Connect("playerOnGround", this, "isGrounded");
    }
   

    public override void _Process(float delta)
    {
        if (gm.playerDiedToEnemy)
        {
          
            died = true;
            playerScript.inputEnabled = false;
            
        }
        if (playerScript.velocity.x > 0)
        {
            dustWalk.SetPosition(new Vector2(-14, 27));

        }
        else if (playerScript.velocity.x < 0)
        {
            dustWalk.SetPosition(new Vector2(14, 27));

        }

        stateHandler();
    }
    public void stateHandler()
    {
        if (!playerScript.finished)
        {
            if (!died)
            {
                if (jump)
                {
                    playerSprite.Play("jump");
                    dustWalk.Emitting = false;
                }
                else if (walking && !inAir) //case walking
                {

                    dustWalk.Emitting = true;
                    playerSprite.SetAnimation("walkRight");

                }
                else
                {
                    dustWalk.Emitting = false;

                    playerSprite.SetAnimation("idle");
                }
            }
            else
            {
                dustWalk.Emitting = false;
                playerSprite.Play("Death");
                if (!sound.die.IsPlaying())
                {
                    cameraAnim.Play("CameraDieZoom");
                    sound.music.Stop();
                    sound.die.Play();
                }

            }
        }
        else
        {
           
            playerSprite.SetAnimation("Finish");
            sound.music.Stop();

        }

    }

    //Incoming signal functions
    public void onDeath(KinematicBody coll, Vector2 PlayerPosition)
    {
        //playerSprite.Play("Death");
        cameraAnim.Play("CameraDieZoom");
        died = true;
    }
    public void onJumping(float yVelocity)
    {
        jumpDelay(jumpTime);
    }
    private async void jumpDelay(float jumpDelay)
    {
        jump = true;
        await ToSignal(GetTree().CreateTimer(jumpDelay), "timeout");
        jump = false;
    }
    public void onMove(float xVelocity, bool moving)
    {
        if (xVelocity > 0.1)
        {
            
            playerSprite.SetFlipH(false);
            left = true;
            right = false;
        } else if (xVelocity < -0.1) {
            //flips horizontally when moving
            playerSprite.SetFlipH(true);
            left = false;
            right = true;
        } else
        {
            //resets flip once stopped moving
            if (left)
            {
                playerSprite.SetFlipH(false); 
            } else if (right)
            {
                playerSprite.SetFlipH(true);  
            }
        }
        walking = moving;
        
    }
    public void isGrounded(bool grounded)
    {
        inAir = grounded;
        
    }
}
