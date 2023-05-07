public class RefactoredUIManager : UIManagerBase
{
    public static RefactoredUIManager instance;
    
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

    protected override GameControllerBase GameController => RefactoredGameController.instance;
}