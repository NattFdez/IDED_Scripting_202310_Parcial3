public sealed class RefactoredGameController : GameControllerBase
{
    public static RefactoredGameController instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    protected override PlayerControllerBase PlayerController => RefactoredPlayerController.instance;

    protected override UIManagerBase UiManager => RefactoredUIManager.instance;

    protected override ObstacleSpawnerBase Spawner => RefactoredObstacleSpawner.instance;

    protected override void OnScoreChanged(int hp)
    {
        PlayerController?.SendMessage("OnScoreChangedEvent", hp);
        UiManager?.SendMessage("UpdateScoreLabel");
    }

    protected override void SetGameOver()
    {
        OnGameOver();
        base.SetGameOver();
    }

    public void OnGameOver()
    {
        UiManager?.SendMessage("OnGameOver");
        PlayerController?.SendMessage("OnGameOver");
        Spawner?.SendMessage("OnGameOver");
    }
    
    public void OnObstacleDestroyed(int hp)
    {
        base.OnObstacleDestroyed(hp);
    }
}