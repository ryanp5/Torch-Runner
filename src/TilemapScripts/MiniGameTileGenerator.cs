using Godot;
using System;

public class MiniGameTileGenerator : TileMap
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    TileSet tileSet;
    [Export]
    public int yRand = 3;
    [Export]
    int startObstacles = 15;
    [Export]
    public int courseLength = 55;
    [Export]
    public float endObstacles = 5;
    [Export]
    public int width = 4;
    int height;
    int MaxXWidth = 10;
    int minXWidth = 5;
    int yWidth = 0;
    KinematicBody2D player;
    public Vector2 playerpos;
    Vector2 tilepos;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        player = GetNode<KinematicBody2D>("/root/MiniGameScene/PlayerMini");
        tileSet = GetTileset();
        
        playerpos = new Vector2(player.GetGlobalPosition());
        tilepos= WorldToMap(playerpos);
        tileGenerator();
        wallGenerator();
        obstacleGenerator();
        
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }
    public float getCourseLength()
    {
        return courseLength;
    }
    private void tileGenerator()
    {
        for (int i =0; i < 10; i++)
        {
            //
            SetCell((int)tilepos.x - width - i, (int)tilepos.y + i, 1);
            SetCell((int)tilepos.x + width + i, (int)tilepos.y + i, 1);

            SetCell((int)tilepos.x - width - i, (int)tilepos.y - i - courseLength, 1);
            SetCell((int)tilepos.x + width + i, (int)tilepos.y - i - courseLength, 1);

        }
      
        
    }
    private void wallGenerator()
    {
        for (int i = 0; i < courseLength; i++)
        {
            SetCell((int)tilepos.x + width, (int)tilepos.y - i, 1, false, true, true);
            SetCell((int)tilepos.x - width, (int)tilepos.y - i, 1, true , true, true);
        }
       
    }
    private void obstacleGenerator()
    {
        for (int i = startObstacles; i < courseLength - endObstacles; i++)
        {

            SetCell((int)tilepos.x + (int)GD.Randi() % width, (int)tilepos.y - i - (int)GD.Randi() % yRand, 2);
        }
    }
}
