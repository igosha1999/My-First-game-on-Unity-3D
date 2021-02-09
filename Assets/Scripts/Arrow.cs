using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IOjestDestroyer
{



    [SerializeField] private SpriteRenderer spriteRenderer;//
    [SerializeField] private new Rigidbody2D rigidbody;//визиваємо ріджет баді для сили тяжіння

    public Arrow(Rigidbody2D rigidbody)
    {
        this.rigidbody = rigidbody;
    }

    [SerializeField] private float force;//визиваємо зміну форс для задання швидкості
    [SerializeField] private float lifeTime;// визиваємо міну лайфтайм для перебування вистреленої стріли деякий час

    public Arrow(float lifeTime)
    {
        this.lifeTime = lifeTime;
    }

    [SerializeField] private TrigerDamage trigerDamage;//визиваємо зміну 

    public Arrow(TrigerDamage trigerDamage)
    {
        this.trigerDamage = trigerDamage;
    }

    private Player player;//для получення силки гравця
    public float Force
    {
        get { return force; }
        set { force = value; }

    }

    public void Destroy(GameObject gameObject)
    {
        player.ReturnArrowToPool(this);
    }

    public void SetImpulse(Vector2 direction, float force, int bonusDamage, Player player)//  метод який задає координату перебуванєє стріли тобто звідки вона буде вилітати
    {
        this.player = player;
        trigerDamage.Init(this);
        trigerDamage.Parent = player.gameObject;//це потрібно для того щоб стріла не торкалася героя який нею стріляє
        trigerDamage.Damage += bonusDamage;
        rigidbody.AddForce(direction * force, ForceMode2D.Impulse);//задаємо силу і швидкість нашої стріли
        if (force < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);//якщо гравець дивиться в ліво тобто це < 0  то і стріла розвертається на 180 і вилітає зліва
        StartCoroutine(Startlife());//запускаємо метод стартлайф

    }


    private IEnumerator Startlife()//метод для створення часу перебування
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        yield break;
    }
}
//Quatertion = обєкт який працює з поворотом
//Eular = структора яка вказує як повернути обєкт
public interface IOjestDestroyer
{
    void Destroy(GameObject gameObject);//створюємо інтерфейс щоб видалити стілу 
}