using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerDamage : MonoBehaviour
{
    [SerializeField] private int damage;//зміна для нанесення урону
    public int Damage //створюємо властивість над роботою зміни демедж
    {
        get { return damage; }
        set { damage = value; }

    }
    [SerializeField] private bool isDestroyinAfterCollision;//зміна для перевірки 

    public TrigerDamage(bool isDestroyinAfterCollision)
    {
        this.isDestroyinAfterCollision = isDestroyinAfterCollision;
    }

    [SerializeField] private IOjestDestroyer destroyer;
    private GameObject parent;//зміна для того щоб стіла не торкалася свого героя

    public GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }
    
    

    public void Init(IOjestDestroyer destroyer)//ініціалізуємо метод 
    {
        this.destroyer = destroyer;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == parent)// зрівнюємо нашого героя з зміною перент щоб вони були взаємозвязані 
            return;
        if (GameManager.Instance.healthContainer.ContainsKey(collision.gameObject))//constainkey=перевіряє чи існує такий ключ
        {
            var health = GameManager.Instance.healthContainer[collision.gameObject];//конструкція вертає скріпт хелс на ігровий обєкт
            if (health != null)//якщо хелс не дорівнює нудю то ми попадаємо в тіло
            {
                health.TakeHit(damage, gameObject);
            }
        }
        if (isDestroyinAfterCollision)//якщо ця зміна торкається до чогось то вона знищується
        {
            if (destroyer == null)
                Destroy(gameObject);
        }
        else destroyer.Destroy(gameObject);
            
    }



    //var = анонімний тип даних який приймає в себе люий тип якщо приймає один тип він ним стає назавжди
}
