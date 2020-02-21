using Godot;
using System;

public class SafeRespawns : Node
{
    GameController gm;
    Vector2[] safeSpawns = new Vector2[20];

    public override void _Ready()
    {
        gm = (GameController)GetNode("/root/SceneChanger");
        Node2D curr = null;
        Vector2 spawnpt = new Vector2(0,0);
        for (int i = 0; i < GetChildCount(); i++)
        {
            curr = GetChild<Node2D>(i);
            safeSpawns[i] = curr.GetPosition();
            
        }
      
        gm.respawnPts = safeSpawns;
    
    }
    
}
