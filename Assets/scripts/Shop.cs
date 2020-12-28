using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    bool showShop = false;
    public int shopGold = 500;
    public float multValueShop = 0.4f;
    Inventory inventoryPlayer;
    float currWeight = 0;
    float maxWeight = 500;
    private void Awake()
    {
        inventory.Add(new Item("Miecz", 200, "duzy miecz dla wojownika", 7, StatusItem.toBuy, "Sprite/miecz"));
        inventory.Add(new Item("Miecz", 300, " miecz dla palladyna", 7, StatusItem.toBuy, "Sprite/miecz"));
        inventory.Add(new Item("Miecz", 400, " mały miecz", 7, StatusItem.toBuy, "Sprite/miecz"));
    }
    void BuyItem(Item i)
    {
        if (i.value <= inventoryPlayer.ownedGold & i.weight <= maxWeight - currWeight)
        {
            inventoryPlayer.inventory.Add(i);
            inventoryPlayer.ownedGold -= i.value;
            shopGold += i.value;
            inventory.Remove(i);
        }
    }
    private void ShowShop()
    {
        showShop = !showShop;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            ShowShop();
            other.GetComponent<Inventory>().shop = this;
            inventoryPlayer = other.GetComponent<Inventory>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            ShowShop();
            other.GetComponent<Inventory>().shop = null;
            inventoryPlayer = null;
        }
    }
    private void OnGUI()
    {
        if (showShop)
        {
            GUI.Box(new Rect(510, 55, 400, 500), "Shop");
            currWeight = 0; 
            int lastX = 530;
            int row = 1;
            foreach (Item item in inventory)
            {
                Rect rect = new Rect(lastX, (row * 80) + 50, 80, 80);
                lastX += 80;
                if (GUI.Button(rect, Resources.Load(item.imagePath) as Texture))
                {
                    if(Event.current.button == 0)
                    {
                        BuyItem(item);
                    }
                    
                }
                if (rect.Contains(Event.current.mousePosition))
                {
                    GUI.Label(new Rect(Event.current.mousePosition.x + 20, Event.current.mousePosition.y + 20, 120, 120), "Nazwa: " + item.name + "\nCena: " + item.value + "\nWaga: " + item.weight + "\nOpis: " + item.description);
                }
                currWeight += item.weight;
                if (lastX >= 850)
                {
                    lastX = 530;
                    row++;
                }
            }
            GUI.Label(new Rect(530, 100, 340, 50), "Weight:" + currWeight + "/" + maxWeight + "KG  Gold:" + shopGold);
        }
    }
}
