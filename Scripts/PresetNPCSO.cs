using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "ScriptableObjects/PresetNPC", order = 1)]
public class PresetNPCSO : ScriptableObject
{
    public GameObject NPCPrefab;
    public float competency;
    public float itemDurability;
    public GameObject itemPrefab;
    public TextAsset dialogue;
}