using Godot;
using System;

public class HeartScript : AnimatedSprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    TileMap course;
    MiniGameTileGenerator tileGen;
    GameController gm;
    float heartPosY;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        gm = (GameController)GetNode("/root/SceneChanger");
        Area2D coll = GetNode<Area2D>("Area2D");
        coll.Connect("body_shape_entered", gm, "OnMiniPlayerCollectHeart");
        SetAnimation("beat");
        course = GetNode<TileMap>("/root/MiniGameScene/TileMover/Walls");
        heartPosY = -(45* 32);
        SetAnimation("idle");
        SetGlobalPosition(new Vector2(GetGlobalPosition().x,heartPosY));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
      
 }
private void PlayerCollectHeart(int body_id, object body, int body_shape, int area_shape)
{

        SetAnimation("HeartCollected");
}
}



