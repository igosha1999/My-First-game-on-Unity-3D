using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionCoins : MonoBehaviour
{
    

    public int coins = 0;

    private void OnTriggerEnter2D(Collider2D col)//добавляємо трігер на обєкт
    {
        if (GameManager.Instance.coinContainer.ContainsKey(col.gameObject))//добавляємо gamemanager(монетки) і якщо він використовується
        {


            coins++;//наша монета збільшується в кількості
            //text.text = coins.ToString(); //виводимо на екран якщо збираємо монету змінюється число

            var coin = GameManager.Instance.coinContainer[col.gameObject];//присваюєму зміну монета від gamemanager 
            coin.StartDestroy();//і визиваємо її в юніті де будемо використовувати
        }

    }
    private void Start()
    {
        coins = PlayerPrefs.GetInt("Player Score");
    }
    private void Update()
    {
        PlayerPrefs.SetInt("Player Score", coins);
        
    }
}
