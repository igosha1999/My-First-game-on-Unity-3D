using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int damage = 10;//нанесення урону
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    public CollisionDamage(Animator animator)
    {
        this.animator = animator;
    }

    private Health health;
    private float direction;//ця зміна буде вказувати напрям до героя по осі х
    public float Direction
    {
        get { return direction; }
    }

    private void OnCollisionStay2D(Collision2D col)//приватний метод коли обєкти торкаються один одного (Collision2D сімейний метод зміної який відповідає торканю до обєктів,col його зміна )
    {
        health = col.gameObject.GetComponent<Health>();//звязуємо скріпт Health з обєктом
        if (health != null)//звертаємося до обєкта до якого торкнулися (тобто ми в обєкта получаємо компонент хелс)
        //якщо ми стулкнулися з обєктом без зміни хелс то ми получаємо нуль тобто урон не наноситься
        {
            direction = (col.transform.position - transform.position).x;//якщо дірекшен плюсове то противник знаходиться  справа якщо відємне то зліва
            animator.SetFloat("Direction", Mathf.Abs(direction)); //перехід в анімацію урона
        }



    }
    //Mathf.Abs = модуль направлення
    // Trigger - це подія яка передається в аніматор і він може цю подію може реагувати або не реагувати
    // Start is called before the first frame update
    
    public void SetDamage()//цей метод буде працювати тільки якщо його визвали в аніматорі
    {
        if (health != null)
            health.TakeHit(damage, gameObject);
        health = null;
        direction = 0;
        animator.SetFloat("Direction", 0f);
    }
}
