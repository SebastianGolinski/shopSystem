using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    public Shop shop;
    bool showInventory = false;
    public int ownedGold = 200;
    float currWeight = 0;
    float maxWeight = 200;
    private void Awake()
    {
        inventory.Add(new Item("Miecz", 100, "duzy miecz dla kogos", 7, StatusItem.ownedByPlayer, "Sprite/miecz"));
        inventory.Add(new Item("Tacza", 70, "tarcza do obrony", 3, StatusItem.ownedByPlayer, "Sprite/tarcza"));
        inventory.Add(new Item("Buty", 50, "Buty do szybkosci", 7, StatusItem.ownedByPlayer, "Sprite/buty"));

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
      

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Shop"))
        {
            ShowInventory();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Shop"))
        {
            ShowInventory();

        }
    }
    
    void SellItem(Item i)
    {
        if (i.value <= shop.shopGold && i.weight <= maxWeight- currWeight)
        {
            inventory.Remove(i);
            shop.shopGold -= i.value;
            ownedGold += i.value;
            i.value = (int)(i.value * shop.multValueShop);
            shop.inventory.Add(i);
        }
        
    }
    private void ShowInventory()
    {
        showInventory = !showInventory;
    }


    private void OnGUI()
    {
        if (showInventory)
        {
            GUI.Box(new Rect(10, 55, 400, 500), "Inventory");
            currWeight = 0;
            int lastX = 30;
            int row = 1;
            foreach(Item item in inventory)
            {
                Rect rect = new Rect(lastX, (row * 80) + 50, 80, 80);
                lastX += 80;
                if (GUI.Button(rect, Resources.Load(item.imagePath) as Texture) && shop!= null)
                {
                    if (Event.current.button == 1)
                    {
                        SellItem(item);
                    }
                   
                }
                if (rect.Contains(Event.current.mousePosition))
                {
                    GUI.Label(new Rect(Event.current.mousePosition.x + 20, Event.current.mousePosition.y + 20, 120, 120), "Nazwa: " + item.name + "\nCena: " + item.value + "\nWaga: " + item.weight + "\nOpis: " + item.description);
                }
                currWeight += item.weight; 
                if (lastX >= 450)
                {
                    lastX = 530;
                    row++;
                }
            }
            GUI.Label(new Rect(30, 100, 340, 50), "Weight:"+ currWeight + "/"+maxWeight+"KG  Gold:"+ownedGold);
        }

    }

}

