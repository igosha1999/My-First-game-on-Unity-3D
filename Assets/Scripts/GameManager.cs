using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton 
    public static GameManager Instance { get; private set; }
    #endregion

    [SerializeField] private GameObject inventoryPanel; //панель інвентаря

    public GameManager(GameObject inventoryPanel)
    {
        this.inventoryPanel = inventoryPanel;
    }
    
    public Dictionary<GameObject, Health> healthContainer;
    public Dictionary<GameObject, Coin> coinContainer;
    public Dictionary<GameObject, BuffReciever> buffRecieverContainer;
    public Dictionary<GameObject, Menu> menu;
    public Dictionary<GameObject, ItemComponents> itemContainer;
    public ItemBase itemDataBase;
    public PlayerInventory playerInventory;
    //[HideInInspector] public PlayerInventory playerInventory;

    private void Awake()
    {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();//створюємо словарик
        coinContainer = new Dictionary<GameObject, Coin>();
        buffRecieverContainer = new Dictionary<GameObject, BuffReciever>();
        menu = new Dictionary<GameObject, Menu>();
        itemContainer = new Dictionary<GameObject, ItemComponents>();

    }


    //foreach - цикл для обробки всіх елементів які використовуються


    public void OnClickPause()//публічний метод для привязання кнопки інвенторі
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
        //inventoryPanel.gameObject.SetActive(true);//аквтивовуємо панель в кнопці п
    }
    
        





    

        

    





}
