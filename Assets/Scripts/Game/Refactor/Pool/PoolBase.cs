using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PoolBase : MonoBehaviour, IPool
{
    [SerializeField]
    private int count;

    [SerializeField]
    private GameObject basePrefab;

    private List<GameObject> instances = new List<GameObject>();

    public void RecycleInstance(GameObject instance)
    {
        if (instance != null)
        {
            instance.transform.position = transform.position;
            instance.transform.rotation = Quaternion.identity;
            instance.SetActive(false);
            instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            instances.Add(instance);
        }
    }

    public GameObject RetrieveInstance()
    {
        if (instances.Count > 0)
        {
            GameObject instance = instances.ElementAt(0);

            instances.Remove(instance);

            return instance;
        }
        else
        {
            PopulatePool();
            return RetrieveInstance();
        }
    }

    private void PopulatePool()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject GO = Instantiate(basePrefab, transform.position, Quaternion.identity, transform);
            GO.SetActive(false);
            GO.GetComponent<PoolableObject>().AssigPool(this);
            instances.Add(GO);
        }
    }
}