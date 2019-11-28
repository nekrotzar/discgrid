using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    private DiskController diskController;
    new public string name = "New Item";    // Name of the item
    public Sprite icon = null;              // Item icon
    public bool isDefaultItem = false;      // Is the item default wear?
    public Material itemMaterial;
    public float damageFactor;

    // Called when the item is pressed in the inventory
    public virtual void Use()
    {
        // Use the item
        // Something might happen

        Debug.Log("Using " + name);

        diskController.SetDisk(name, this);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

    public void SetDiskController(DiskController controller)
    {
        diskController = controller;
    }
}