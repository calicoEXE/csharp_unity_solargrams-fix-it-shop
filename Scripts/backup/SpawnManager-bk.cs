/*using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//#if UNITY_EDITOR
//using UnityEditor.SceneManagement;
//#endif

public class SpawnManager : MonoBehaviour
{
    // handling all randomised range =========================

    public int minNumberOfNPCs = 1;
    public int maxNumberOfNPCs = 3;
    public float minCompetency = 0f;
    public float maxCompetency = 1.0f;
    public float minDurability = 0f;
    public float maxDurability = 1.0f;
    private int decimalPlaces = 2;

    // pulling external scripts ==============================

    public UIManager uiManager;
    public DayNightScript dayNightScript;

    // info trackng ==========================================

    public List<GameObject> npcPrefabs = new List<GameObject>();
    public List<GameObject> itemPrefabs = new List<GameObject>();

    public List<GameObject> spawnedNPCs = new List<GameObject>();
    public List<GameObject> spawnedItems = new List<GameObject>();

    public List<EntityData> entityDataList = new List<EntityData>();

    public List<EntityData> currentlySpawnedEntities = new List<EntityData>();
    public List<EntityData> existingSpawnedEntities = new List<EntityData>(); 

    private bool allowSpawning = false;

    public void SpawnNPCs()
    {
        if (!dayStart)
        {
            Debug.LogWarning("It's not dayStart.");
            return;
        }

        allowSpawning = true;

        Debug.Log("SpawnNPCs spawned.");

        if (!Application.isPlaying)
        {
            Debug.LogError("Spawning is only allowed in Play mode.");
            return;
        }

        DestroyAllNPCs();
        DestroyAllItems();

        int numberOfNPCs = Random.Range(minNumberOfNPCs, maxNumberOfNPCs + 1);

        for (int i = 0; i < numberOfNPCs; i++)
        {
            //Random.InitState(System.Environment.TickCount);

            GameObject npcPrefab = GetRandomPrefab(npcPrefabs);
            Vector3 npcSpawnPosition = new Vector3(transform.position.x + Random.insideUnitSphere.x * 5f, 0f, transform.position.z + Random.insideUnitSphere.z * 5f);
            GameObject newNPC = Instantiate(npcPrefab, npcSpawnPosition, Quaternion.identity);
            spawnedNPCs.Add(newNPC);

            float npcCompetency = Random.Range(minCompetency, maxCompetency);
            npcCompetency = Mathf.Round(npcCompetency * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces);

            NPC refToNPC = newNPC.AddComponent<NPC>();
            refToNPC.UpdateCompetency(npcCompetency);




            GameObject itemPrefab = GetRandomPrefab(itemPrefabs);
            GameObject newItem = Instantiate(itemPrefab, newNPC.transform.position + Random.insideUnitSphere, Quaternion.identity);
            newItem.transform.parent = newNPC.transform;
            spawnedItems.Add(newItem);

            Item refToItemScript = newItem.GetComponent<Item>();

            float itemDurability = Random.Range(minDurability, maxDurability);
            itemDurability = Mathf.Round(itemDurability * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces);

            float itemRepairUrgency = 1 - (npcCompetency * itemDurability);

            EntityData entityData = new EntityData(newNPC.name, npcCompetency, itemPrefab.name, itemDurability, itemRepairUrgency);

            Debug.Log("Newly Randomised - NPC Name: " + newNPC.name + ", Item Name: " + newItem.name.ToString() + ", NPC Competency: " + npcCompetency + ", Item Durability: " + itemDurability + ", Repair Urgency: " + itemRepairUrgency);

            entityDataList.Add(entityData);
        }

        LogEntityDataList();
    }



    void DestroyAllNPCs()
    {
        foreach (GameObject npc in spawnedNPCs)
        {
            DestroyImmediate(npc);
        }
        spawnedNPCs.Clear(); // Clear the list after destroying NPCs
    }

    void DestroyAllItems()
    {
        foreach (GameObject item in spawnedItems)
        {
            DestroyImmediate(item);
        }
        spawnedItems.Clear(); // Clear the list after destroying items
    }

    GameObject GetRandomPrefab(List<GameObject> prefabList)
    {
        if (prefabList.Count > 0)
        {
            int randomIndex = Random.Range(0, prefabList.Count);
            return prefabList[randomIndex];
        }
        else
        {
            Debug.LogError("Prefab list is empty!");
            return null;
        }
    }

    public void LogEntityDataList() // storing existing + new spawns
    {
        for (int i = 0; i < entityDataList.Count; i++)
        {
            EntityData data = entityDataList[i];

            if (data.npcName != null && data.selectedItem != null && data.npcCompetency >= 0 && data.itemDurability >= 0 && data.repairUrgency >= 0)
            {
                //string itemName = (i < spawnedItems.Count) ? spawnedItems[i].name : "No item spawned";

                Debug.LogError($"Stored - NPC Name: {data.npcName}, Item Name: {data.selectedItem}, Competency: {data.npcCompetency}, Durability: {data.itemDurability}, Repair Urgency: {data.repairUrgency}");
            }
        }
    }

    public void AddToExistingSpawned(EntityData entityData)
    {
        currentlySpawnedEntities.Remove(entityData);
        existingSpawnedEntities.Add(entityData);
    }


    /*[ContextMenu("Reset Scene")]
    private void ResetScene()
    {
#if UNITY_EDITOR
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().buildIndex);
#endif
    }

    public void ResetAll()
    {
        if (allowSpawning)
        {
            allowSpawning = false;
            ClearAll();
            //EntityStoringSystem.Instance.ClearEntityData();
            Debug.LogError("RESET.");
        }
    }

    void ClearAll()
    {
        entityDataList.Clear();

        DestroyAllNPCs();
        DestroyAllItems();

        uiManager.UpdateCurrentEntityDataUI(); // update UI after clearing all NPC/Items
        uiManager.UpdateExistingEntityDataUI();
    }
}*/
