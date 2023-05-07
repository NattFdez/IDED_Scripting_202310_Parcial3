using System.Collections;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    [SerializeField] private float duration = 2f;
    public PoolBase pool { get; private set; }
    private Coroutine delayed;

    private void OnEnable()
    {
        if (delayed == null) delayed = StartCoroutine(ReturnToPoolAfterDelay());
        else 
        {
            StopCoroutine(delayed);
            delayed = StartCoroutine(ReturnToPoolAfterDelay());
        }
    }

    public void RecycleObject()
    {
        if(pool != null) OnObjectToRecycle(gameObject);
    }
    
    void OnObjectToRecycle(GameObject instance)
    {
        pool.RecycleInstance(instance);
    }

    public void AssigPool(PoolBase pool)
    {
        this.pool = pool;
    }
    
    private IEnumerator ReturnToPoolAfterDelay()
    {
        yield return new WaitForSeconds(duration);
        RecycleObject();
    }
}
