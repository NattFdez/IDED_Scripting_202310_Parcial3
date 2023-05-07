using UnityEngine;

public class RefactoredObstacleSpawner : ObstacleSpawnerBase
{
    public static RefactoredObstacleSpawner instance;
    
    [SerializeField]
    private PoolBase obstacleLowPool;

    [SerializeField]
    private PoolBase obstacleMidPool;

    [SerializeField]
    private PoolBase obstacleHardPool;

    protected PoolBase ObjectPool
    {
        get
        {
            int rnd = Random.Range(0, 3);

            switch (rnd)
            {
                case 0:
                    return obstacleLowPool;
                case 1:
                    return obstacleMidPool;
                case 2:
                    return obstacleHardPool;
                default:
                    return obstacleLowPool;
            }
        }
    }
    
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

    protected override void SpawnObject()
    {
        Transform trans = ObjectPool.RetrieveInstance().transform;

        trans.position = new Vector2(Random.Range(MinX, MaxX), YPos);
        trans.rotation = Quaternion.identity;
        trans.gameObject.SetActive(true);
    }
}