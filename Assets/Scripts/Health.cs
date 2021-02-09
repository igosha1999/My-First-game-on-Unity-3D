using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour

   
{
    #region Singleton 
    public static Health Instance { get; private set; }
    #endregion
    [SerializeField] private int health;//зміна хелс(здоровя персонажів)


    public Action<int, GameObject > OnTakeHit;//сигнатура получаючих методів
    
    
    public int CurrentHealth
    {
        get { return health; }
    }//властивість до зміни хелс

    
    public void TakeHit(int damage, GameObject attacker)//публічний метод для нанесення урону
    {
        health -= damage;//віднімаємо здоровя стільки скільки нанесли урону

        if (OnTakeHit != null)//перевіряємо чи на цей метод хтось підписаний якщо да топопадаємо в тіло
            OnTakeHit(damage, attacker);//визиваємо цю подію
        if (health <= 0)
            Destroy(gameObject);//якщо здоровя менше або рівне нулю то персонаж вмирає
            
        
    }
    public void SetHealth(int bonusHealth)//публічний метод добавляє бонусне здоровя
    {
        health += bonusHealth;//до основного доюавляється бонусне
       
    }



    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.healthContainer.Add(gameObject, this);
    }

   
}