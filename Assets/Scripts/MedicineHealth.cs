using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineHealth : MonoBehaviour
{
    public int bonusHealth;


    private void OnTriggerEnter2D(Collider2D col)//при попадані в бонусздоровя буде переданий колайдер 
    {
        if (col.gameObject.CompareTag("Player")|| col.gameObject.CompareTag("Enemy"))//якщо це обєк ігрок чи ворог то тоді...
        {
            
            Health health = col.gameObject.GetComponent<Health>();//з допомоогою геткомпонент ви забираємо хелс і добавляємо герою
            health.SetHealth(bonusHealth);//тепер добавляємо бонусне здоровя
            Destroy(gameObject);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
