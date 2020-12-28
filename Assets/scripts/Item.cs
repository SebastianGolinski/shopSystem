using System.Collections;

public class Item
{
    public string name;
    public int value;
    public string description;
    public float weight;
    public StatusItem statusItem;
    public string imagePath;
    public Item(string name, int value, string description, float weight, StatusItem statusItem, string imagePath)
    {
        this.name = name;
        this.value = value;
        this.description = description;
        this.weight = weight;
        this.statusItem = statusItem;
        this.imagePath = imagePath;
    }
}
public enum StatusItem
{
    toBuy,
    ownedByPlayer,
    other
}
