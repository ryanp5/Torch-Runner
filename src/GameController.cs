using Godot;
using System;

public class GameController : CanvasLayer
{
    bool inSecondChanceMode = false;
    float Diedelay = 2.0f;
    float miniGameOverDelay = 1;
    float onWinDelay = 3.5f;
    float sceneDelay = 0.5f;
    float miniWinDelay = 2f;
    public bool secondLife = true;
    public bool playerDiedToEnemy;
    public Node SoundEffects;
    Node CurrentScene;
    public PlayerMiniMovement miniPlayer;
    public PlayerMovement player;
    public Vector2[] respawnPts = new Vector2[20];
    public Vector2 respawnPt;
    //stores position player died
    Vector2 startPt = new Vector2(0, 0);
    public Vector2 lastPlayerPos;
    public HeartScript miniHeart;
    public StartGameBtn StartButton;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
    public void GoToScene(string path, float delay)
    {
        SceneDelay(path, delay);

    }
    public void DefferredGoToScence(string path, float delay)
    {
        CurrentScene = GetTree().GetCurrentScene();
        CurrentScene.Free();
        var nextScene = (PackedScene)GD.Load(path);
        GetTree().ChangeScene(path);
    }


    private async void SceneDelay(string path, float delay)
    {
        await ToSignal(GetTree().CreateTimer(delay), "timeout");
        
        
        CallDeferred(nameof(DefferredGoToScence), path, delay);

    }

    private void OnPlayerDied(KinematicBody2D coll, Vector2 playerPos)
    {
        lastPlayerPos = playerPos;
        closestSpawnpt(lastPlayerPos);
        playerDiedToEnemy = true;
        GoToScene("res://scenes/MiniGameScene.tscn", Diedelay);
    }

    private void _on_PlayerMini_gameOverSignal()
    {
        playerDiedToEnemy = false;
        GoToScene("res://scenes/TitleScreen.tscn", miniGameOverDelay);
        respawnPt = startPt;
    }

    private void onStartGamePressed()
    {
        playerDiedToEnemy = false;
        GoToScene("res://scenes/Scene1.tscn", sceneDelay);
    }
    private void OnMiniPlayerCollectHeart(int body_id, object body, int body_shape, int area_shape)
    {
        playerDiedToEnemy = false;
        GoToScene("res://scenes/Scene1.tscn", miniWinDelay);
    }
    private void closestSpawnpt(Vector2 lastPlayerps)
    {
        float currDistance = Mathf.Inf;
        for (int i = 0; i < respawnPts.Length; i++)
        {
            if (respawnPts[i].DistanceTo(lastPlayerps) < currDistance && lastPlayerPos.x > respawnPts[i].x)
            {
                respawnPt = respawnPts[i];
                currDistance = respawnPts[i].DistanceTo(lastPlayerPos);
            }
        }
    }
 private void onLevelFinished()
    {
        GoToScene("res://scenes/FinishScene.tscn",onWinDelay);
    }   
private void onGameFinish()
    {
        GoToScene("res://scenes/TitleScreen.tscn", 0.0f);
    }
}














