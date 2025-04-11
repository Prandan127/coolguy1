using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item")]
public class ItemSO : ScriptableObject
{
    public string itemID = System.Guid.NewGuid().ToString();
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite icon;

    public bool isGold;
    public int stackSize = 3;

    [Header("Stats")]
    public int currentHealth;
    public int maxHealth;
    public int speed;
    public int damage;

    [Header("For Temporary Items")]
    public float duration;
}
