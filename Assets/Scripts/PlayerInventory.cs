using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int coinsCount;//зміна для збільшення монети на одну
    [SerializeField] private Text coinText;//присваюємо стеарелізоване поле

    public BuffReciever buffReciever;
    public PlayerInventory(BuffReciever buffReciever)
    {
        this.buffReciever = buffReciever;
    }
    public List<Item> Items //получаємо доступ до листа з еементами
    {
        get { return items; }
    }
    public PlayerInventory(Text coinText)
    {
        this.coinText = coinText;
    }


    private List<Item> items;
    private void Start()
    {
        //coinsCount = 0;
        //coinText.text = coinsCount.ToString();//нашу зміну пишемо текст який буде відображатися на екрані
        //ToString = int переводить в string
        items = new List<Item>();
        GameManager.Instance.playerInventory = this;//
       // PlayerPrefs.HasKey("Coin");
        //coinsCount = PlayerPrefs.GetInt("Coin", 0);
    }
    private void Update()
    {
        
        //PlayerPrefs.SetInt("Coin", coinsCount);
    }

    private void OnTriggerEnter2D(Collider2D col)//добавляємо трігер на обєкт
    {
        /*if (GameManager.Instance.coinContainer.ContainsKey(col.gameObject))//добавляємо gamemanager(монетки) і якщо він використовується
        {
            
            
            coinsCount++;//наша монета збільшується в кількості
            coinText.text = coinsCount.ToString(); //виводимо на екран якщо збираємо монету змінюється число
            
            var coin = GameManager.Instance.coinContainer[col.gameObject];//присваюєму зміну монета від gamemanager 
            coin.StartDestroy();//і визиваємо її в юніті де будемо використовувати
        }*/
        
        if (GameManager.Instance.itemContainer.ContainsKey(col.gameObject))
        {
            var itemComponents = GameManager.Instance.itemContainer[col.gameObject];
            items.Add(itemComponents.Item);
            itemComponents.Destroy(col.gameObject);

        }
    }
       
        
        
    
    








}
