using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject leftBorder;//зміна для границі ходьби в ліво
    public GameObject rightBorder;//зміна для границі ходьди в право
    public new Rigidbody2D rigidbody;//зміна для 
    public GroundDetection groundDetection;//зміна чи торкаєтьс ворог якогос ьобєкта чи ні
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CollisionDamage collisionDamage;

    public EnemyPatrol(CollisionDamage collisionDamage)
    {
        this.collisionDamage = collisionDamage;
    }

    public EnemyPatrol(SpriteRenderer spriteRenderer)
    {
        this.spriteRenderer = spriteRenderer;
    }

    public bool isRightDirection;//коликравець доходить до границі міняє напрямок руху

    public float speed;//зміна для швидкості

    private void Update()
    {
        if (groundDetection.isGrounded)
        {
            if (transform.position.x > rightBorder.transform.position.x || collisionDamage.Direction < 0)//на якій грані ми знаходимося
                //або противник знаходиться зліва
                isRightDirection = false;
            else if (transform.position.x < leftBorder.transform.position.x || collisionDamage.Direction > 0)//також на якій грані ми знаходимося 
        //або якщо противник зправа (тобто не важливо де знаходиться противник герой буде наносити урон передом)
                isRightDirection = true;
            rigidbody.velocity = isRightDirection ? Vector2.right : Vector2.left;//якщо вираз = правда то використовуємо зразу за ? якщо = не правда то зразу за :
            rigidbody.velocity *= speed;
            
        }




        
        if (rigidbody.velocity.x > 0)//якщо напрям гравця по х > 0 то він дивиться в право
            spriteRenderer.flipX = true;//наше зображення стає не правдиве
        if (rigidbody.velocity.x < 0)// якщо напрям гравця по х < 0 то він дивиться в ліво
            spriteRenderer.flipX = false;//наше зображення стає правдиве


    }
}
