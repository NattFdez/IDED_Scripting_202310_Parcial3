public abstract class RefactoredObstacle : ObstacleBase
{
    protected override GameControllerBase GameController => RefactoredGameController.instance;

    protected override void DestroyObstacle(bool notify = false)
    {
        if (notify)
        {
            GameController?.SendMessage("OnObstacleDestroyed", HP);
        }
        
        gameObject.GetComponent<PoolableObject>().RecycleObject();
    }
}