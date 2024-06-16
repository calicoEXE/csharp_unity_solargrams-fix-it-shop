using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;
using Unity.VisualScripting;

//#if UNITY_EDITOR
//using UnityEditor.SceneManagement;
//#endif

public class SpawnManager : MonoBehaviour
{

    public Transform refToSpawnPoint;
    public Transform refToItemSpawnPoint;


    /// This script handles all spawning for NPCs, NPC's competency,
    /// Item, Item durability and calculating the repairUrgency.

    public int minNumberOfNPCs = 1;
    public int maxNumberOfNPCs = 3;
    public float minCompetency = 0f;
    public float maxCompetency = 1.0f;
    public float minDurability = 0f;
    public float maxDurability = 1.0f;
    private int decimalPlaces = 2;

    int uniqueID = 1;

    // pulling external scripts ==============================

    public UIManager uiManager;
    public DayNightScript dayNightScript;
    public QueueSystem queueSystem;
    public EntityData entityData;
    public Item itemScript;
    public NarrativeManagerScript narrativeManager;

    // info trackng ==========================================

    public List<GameObject> npcPrefabs = new List<GameObject>();
    public List<GameObject> itemPrefabs = new List<GameObject>();

    public List<GameObject> spawnedNPCs = new List<GameObject>();
    public List<GameObject> spawnedItems = new List<GameObject>();
    public Dictionary<int, GameObject> spawnedNPCsDictionary = new Dictionary<int, GameObject>();

    public List<EntityData> entityDataList = new List<EntityData>();
    public List<PresetNPCSO> presetNPCSOs = new List<PresetNPCSO>();

    public List<EntityData> currentlySpawnedEntities = new List<EntityData>();
    public List<EntityData> existingSpawnedEntities = new List<EntityData>();

    // boolean check ========================================

    public bool allowSpawning = false;

    // ====================================================================================



    private void Awake()
    {
        if (Application.isPlaying)
        {
            if (dayNightScript.dayCycle == DayNightScript.states.dayStart)
            {
                if (dayNightScript.timeOfDay >= 9f && dayNightScript.timeOfDay < 19f)
                {
                    //Debug.Log("DayStart detected.");
                    allowSpawning = true;
                    SpawnNPCs();
                    //Debug.Log("SpawnNPCs spawned.");
                }
                else
                {
                    //Debug.LogWarning("Not in the specified time range.");
                }

                //allowSpawning = true;

                if (!allowSpawning)
                {
                    return;
                }

                if (!Application.isPlaying)
                {
                    //Debug.LogError("Spawning is only allowed in Play mode.");
                    return;
                }
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnNPC(presetNPCSOs[0]);
        }
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    SpawnNPCFromEntityData(entityDataList[0]);
        //}
    }
    public void SpawnNPCs()
    {

        DestroyAllNPCs();
        DestroyAllItems();

        int numberOfNPCs = Random.Range(minNumberOfNPCs, maxNumberOfNPCs + 1);

        foreach (EntityData entityData in entityDataList)
        {
            entityData.wasSelected = false;
        }

        for (int i = 0; i < numberOfNPCs; i++)
        {
            //Random.InitState(System.Environment.TickCount);
            ///NPC instantiating is done here
            SpawnRandomNPC();
        }

        LogEntityDataList();

        QueueSystem refToQueueSystem = GetComponent<QueueSystem>();

        if (refToQueueSystem != null)
        {
            refToQueueSystem.UpdateRepairQueue();

        }
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
               //Debug.LogError("Prefab list is empty!");
                return null;
            }
        }

        void LogEntityDataList()
        {
            for (int i = 0; i < entityDataList.Count; i++)
            {
                EntityData data = entityDataList[i];

                if (data.npcName != null && data.selectedItem != null && data.npcCompetency >= 0 && data.itemDurability >= 0 && data.repairUrgency >= 0)
                {
                    //string itemName = (i < spawnedItems.Count) ? spawnedItems[i].name : "No item spawned";

                    //Debug.LogError($"Stored - NPC Name: {data.npcName}, Item Name: {data.selectedItem}, Competency: {data.npcCompetency}, Durability: {data.itemDurability}, Repair Urgency: {data.repairUrgency}");
                }
            }
        }

        void SpawnRandomNPC() /// handles randomised (queue) NPC
        {
            GameObject npcPrefab = GetRandomPrefab(npcPrefabs);
            Vector3 npcSpawnPosition = new Vector3(transform.position.x + Random.insideUnitSphere.x * 5f, 0f, transform.position.z + Random.insideUnitSphere.z * 5f);
            //GameObject newNPC = Instantiate(npcPrefab, npcSpawnPosition, Quaternion.identity);
            Vector3 spawnPoint = refToSpawnPoint.position;
            GameObject newNPC = Instantiate(npcPrefab, spawnPoint, Quaternion.identity);
            newNPC.name = newNPC.name.TrimEnd("(Clone)");
            spawnedNPCs.Add(newNPC);
            ///////change
            newNPC.SetActive(false);


            float npcCompetency = Random.Range(minCompetency, maxCompetency);
            npcCompetency = Mathf.Round(npcCompetency * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces);

            NPC refToNPC = newNPC.AddComponent<NPC>();
            refToNPC.UpdateCompetency(npcCompetency);


            ///ITEM instantiating is done here

            GameObject itemPrefab = GetRandomPrefab(itemPrefabs);
            //GameObject newItem = Instantiate(itemPrefab, newNPC.transform.position + Random.insideUnitSphere, Quaternion.identity);
            //GameObject newItem = Instantiate(itemPrefab, newNPC.transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
            GameObject newItem = Instantiate(itemPrefab, refToItemSpawnPoint.position, Quaternion.identity);
            newItem.name = newItem.name.TrimEnd("(Clone)");
            //newItem.transform.parent = newNPC.transform;
            spawnedItems.Add(newItem);
            //////change
            Item refToItemScript = newItem.GetComponent<Item>();

            newItem.SetActive(false);

            float itemDurability = Random.Range(minDurability, maxDurability);
            itemDurability = Mathf.Round(itemDurability * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces);

            float itemRepairUrgency = 1 - (npcCompetency * itemDurability);
            //will always add one to give a unique ID for NPC and Item
            uniqueID += 1;
            EntityData entityData = new EntityData(newNPC.name, npcCompetency, itemPrefab.name, itemDurability, itemRepairUrgency, uniqueID);

            spawnedNPCsDictionary.Add(uniqueID, newNPC);

            //Debug.Log("Newly Randomised - NPC Name: " + newNPC.name + ", Item Name: " + newItem.name.ToString() + ", NPC Competency: " + npcCompetency + ", Item Durability: " + itemDurability + ", Repair Urgency: " + itemRepairUrgency);

            entityDataList.Add(entityData);
        }

        void SpawnNPC(PresetNPCSO presetNPCSO) /// handles preset (core) NPC
        {
            Vector3 npcSpawnPosition = new Vector3(transform.position.x + Random.insideUnitSphere.x * 5f, 0f, transform.position.z + Random.insideUnitSphere.z * 5f);
            //GameObject newNPC = Instantiate(npcPrefab, npcSpawnPosition, Quaternion.identity);
            Vector3 spawnPoint = refToSpawnPoint.position;
            GameObject newNPC = Instantiate(presetNPCSO.NPCPrefab, spawnPoint, Quaternion.identity);
            newNPC.name = newNPC.name.TrimEnd("(Clone)");   
            narrativeManager.coreNPCs.Add(newNPC); // change it to NarrativeManagerScript's list
 
            newNPC.SetActive(false);

            NPC refToNPC = newNPC.AddComponent<NPC>();
            refToNPC.UpdateCompetency(presetNPCSO.competency);


            ///ITEM instantiating is done here
            GameObject newItem = Instantiate(presetNPCSO.itemPrefab, refToItemSpawnPoint.position, Quaternion.identity);
            newItem.name = newItem.name.TrimEnd("(Clone)");
            narrativeManager.coreItems.Add(newItem); // change it to NarrativeManagerScript's list

            Item refToItemScript = newItem.GetComponent<Item>();

            newItem.SetActive(false);

            float itemRepairUrgency = 1 - (presetNPCSO.competency * presetNPCSO.itemDurability);
        
            //will always add one to give a unique ID for NPC and Item
            EntityData entityData = new EntityData(newNPC.name, presetNPCSO.competency, presetNPCSO.itemPrefab.name, presetNPCSO.itemDurability, itemRepairUrgency, uniqueID);

            spawnedNPCsDictionary.Add(uniqueID + 1 , newNPC);

            WorkshopManager workshopManager = FindObjectOfType<WorkshopManager>();
            refToNPC.SetAssociatedItem(newItem);
            workshopManager.SetWorkshopItem(newItem);

            entityDataList.Add(entityData);
        }

    void SpawnNPCFromEntityData(EntityData entityData) /// handles pre-spawned NPCs
    {
        Vector3 spawnPoint = refToSpawnPoint.position;

        GameObject npcPrefab = null;
        foreach (var npc in npcPrefabs)
        {
            if(npc.name == entityData.npcName)
            {
                npcPrefab = npc;
            }
        }
        GameObject newNPC = Instantiate(npcPrefab, spawnPoint, Quaternion.identity);
        newNPC.name = newNPC.name.TrimEnd("(Clone)");
        spawnedNPCs.Add(newNPC);
        newNPC.SetActive(false);


        NPC refToNPC = newNPC.AddComponent<NPC>();
        refToNPC.UpdateCompetency(entityData.npcCompetency);


        GameObject itemPrefab = null;
        foreach (var item in itemPrefabs)
        {
            if (item.name == entityData.selectedItem)
            {
                itemPrefab = item;
            }
        }
        ///ITEM instantiating is done here
        GameObject newItem = Instantiate(itemPrefab, refToItemSpawnPoint.position, Quaternion.identity);
        newItem.name = newItem.name.TrimEnd("(Clone)");
        spawnedItems.Add(newItem);

        spawnedNPCsDictionary.Add(entityData.uniqueID, newNPC);
    }

    /*public void AddToExistingSpawned(EntityData entityData)
    {
        currentlySpawnedEntities.Remove(entityData);
        existingSpawnedEntities.Add(entityData);
    }*/

    /*[ContextMenu("Reset Scene")]
    private void ResetScene()
    {
#if UNITY_EDITOR
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().buildIndex);
#endif
    }*/

    /*public void ResetAll()
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
    }*/


}