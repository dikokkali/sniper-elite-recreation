using NaughtyAttributes;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [ReadOnly]
    public string itemId;
    public string itemName;
    public string itemDescription;

    public GameObject itemPrefab;
    public Sprite itemIcon;

    protected void Awake()
    {
        itemId = GetItemId();
    }

    public string GetItemId()
    {
        return System.Guid.NewGuid().ToString();
    }
}
