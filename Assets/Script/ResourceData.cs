using UnityEngine;
using UnityEngine.Tilemaps;

public enum ResourceType
{
    Tile,
    Prefab
}

[CreateAssetMenu(menuName = "Game/Resource Data")]
public class ResourceData : ScriptableObject
{
    public string resourceName;
    public ResourceType resourceType;

    public TileBase tile;         
    public GameObject prefab;      

    public Sprite icon;
    public int maxAmount = 10;
}
