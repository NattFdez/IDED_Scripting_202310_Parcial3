using System.Collections;
using UnityEngine;

public class RefactoredPlayerController : PlayerControllerBase
{
    public static RefactoredPlayerController instance;

    [SerializeField] private PoolBase low, mid, hard;

    private PoolBase selected;
    
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

    protected override bool NoSelectedBullet => selected == null;

    protected override void Shoot()
    {
        Rigidbody rb = selected.RetrieveInstance().GetComponent<Rigidbody>();
        rb.transform.position = spawnPos.position;
        rb.transform.rotation = spawnPos.rotation;
        rb.gameObject.SetActive(true);
        rb.AddForce(transform.forward * shootForce, ForceMode.Force);
    }

    protected override void SelectBullet(int index)
    {
        OnBulletSelected(index);
    }

    public void OnScoreChangedEvent(int scoreAdd)
    {
        base.UpdateScore(scoreAdd);
    }

    void OnBulletSelected(int index)
    {
        RefactoredUIManager.instance.SendMessage("EnableIcon", index);
        
        switch (index)
        {
            case 0 :
                selected = low;
                break;
            case 1 :
                selected = mid;
                break;
            case 2:
                selected = hard;
                break;
            default:
                selected = low;
                break;
        }
    }
}