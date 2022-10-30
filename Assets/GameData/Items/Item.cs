using NaughtyAttributes;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField][ReadOnly] private string _itemId;
    [SerializeField] private string _itemName;
    [SerializeField] private string _itemDescription;

    public string ItemId { get; protected set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }

    public GameObject itemPrefab;
    public Sprite itemIcon;

    protected void Awake()
    {
        _itemId = GetItemId();
    }

    public string GetItemId()
    {
        return System.Guid.NewGuid().ToString();
    }
}
