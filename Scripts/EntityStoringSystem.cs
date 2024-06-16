using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStoringSystem : MonoBehaviour
{
    public static EntityStoringSystem Instance;

    public List<EntityData> entityDataList = new List<EntityData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddEntityData(EntityData refToData)
    {
        entityDataList.Add(refToData);
    }
    
    public void ClearEntityData()
    {
        entityDataList.Clear();
    }
}
