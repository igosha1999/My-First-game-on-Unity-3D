using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Item DataBase", menuName ="DataBase/Items")]

public class ItemBase : ScriptableObject//створює файли (контейнер для даних)
{
    //ItemBase - скрипт який хранить базу даних
    [SerializeField, HideInInspector ] private List<Item> items; //створюємо лист даних
    [SerializeField] private Item currentItem;


    private int currentIndex;//зміна для перегляду елементів в базі даних

    //метод який створює елементи
    public void CreateItem()
    {
        //якщо items != 0 то переходимо в тіло якщо = 0 то проаналізовуємо ще раз наший лист
        if (items == null)
            items = new List<Item>();
        //створюємо новйи Item і переносимо його в лист
        Item item = new Item();
        items.Add(item);
        currentItem = item;
        //currentIndex = кількості елементів в листі - 1
        currentIndex = items.Count - 1;
    }
    //метод який видаляє елемент з баз даних
    public void RemoveItem()
    {
        //якщо items = 0 то ми питаємося видалити елемент
        if (items == null)
            return;//виходимо з цього методу
        //перевіряємо чи currentItem != 0
        if (currentItem == null)
            return;
        //якщо вони не !=0 то можемо елемент видаляти
        items.Remove(currentItem);
        //провіряємо чи осталися в базі даних елементи
        if (items.Count > 0)
            currentItem = items[0];//якщо предмет остався то він буде 0
        else CreateItem();//і свторюємо новий предмет
        currentIndex = 0;//або остається цей елемент або переключаємся на елемент 0
    }

    public void NextItem()
    {//пересовуємося по базі даних на одиницю вперед(показує наступний предмет
        if(currentIndex + 1 < items.Count)
        {
            currentIndex++;
            currentItem = items[currentIndex];
        }
    }
    public void PrevItem()
    {//пересуваємося по базі даних на одиницю назад(показує попередній предет)
        if(currentIndex > 0)
        {
            currentIndex--;
            currentItem = items[currentIndex];
        }
    }
    //лямда вираз для перебирання нашого списку в баз даних
    public Item GetItemOfID(int id)
    {
        return items.Find(t => t.ID == id);

    }



}
[System.Serializable]
//клас для відображення елементів в Item
public class Item  //ScriptableObject//створює файли (контейнер для даних)
{
    //SerializeField - запамятовує зміні які ми вказуємо
    [SerializeField] private int id;

    public Item(int id, string description)
    {
        this.id = id;
        this.description = description;
    }
    [SerializeField] private Sprite icon;

    public Item(Sprite icon)
    {
        this.icon = icon;
    }

    public Sprite Icon
    {
        get { return icon; }
    }
    public int ID
    {
        get { return id; }
    }
    [SerializeField] private string itemName;

    public Item(string itemName)
    {
        this.itemName = itemName;
    }

    public string ItemName
    {
        get { return itemName; }
    }
    [SerializeField] private string description;
    public string Description
    {
        get { return description; }
    }
    [SerializeField] private BuffType type;

    public Item(BuffType type)
    {
        this.type = type;
    }

    public BuffType Type
    {
        get { return type; }
    }
    [SerializeField] private float value;

    public Item(float value)
    {
        this.value = value;
    }

    public Item()
    {
    }

    public float Value
    {
        get { return value; }
    }


}
