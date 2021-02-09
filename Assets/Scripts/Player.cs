using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{



    [SerializeField] private float speed = 2;//швидкість гравця
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public static Player Instance { get; internal set; }
    [SerializeField] private float force;//сила прижка
    [SerializeField] private Rigidbody2D rigidboby;//підкючення ріджет баді 
    public Player(Rigidbody2D rigidboby, float minimalHeight, float force, float damageForce)
    {
        this.rigidboby = rigidboby;
        this.minimalHeight = minimalHeight;
        this.force = force;
        this.damageForce = damageForce;
    }
    [SerializeField] private float minimalHeight;//мінімальна висота
    [SerializeField] private bool isCheatMode;//включає чи вмирає персонаж
    public Player(bool isCheatMode)
    {
        this.isCheatMode = isCheatMode;
    }
    [SerializeField] private GroundDetection groundDetection;
    public Player(GroundDetection groundDetection)
    {
        this.groundDetection = groundDetection;
    }
    [SerializeField] private Vector3 direction;//зміна яка зберігає напрям героя
    [SerializeField] private Animator animator;// зміна яка дозволяє ходити в ліво і в право
    public Player(Animator animator)
    {
        this.animator = animator;
    }
    [SerializeField] private SpriteRenderer spriteRenderer;//зміна яка відповідає за поворот персонажа (куди йде туди і дивиться)
    public Player(SpriteRenderer spriteRenderer)
    {
        this.spriteRenderer = spriteRenderer;
    }
    [SerializeField] private Arrow arrow;
    public Player(Arrow arrow)
    {
        this.arrow = arrow;
    }
    [SerializeField] private Transform arrowSpawnPoint;//силка для спавну стріли на герої
    public Player(Transform arrowSpawnPoint)
    {
        this.arrowSpawnPoint = arrowSpawnPoint;
    }
    [SerializeField] private float cooldown;
    public Player(float cooldown)
    {
        this.cooldown = cooldown;
    }
    [SerializeField] private int arrowsCout = 3;//зміна для визначення скільки стріл буде мати наший гравець
    private Arrow currentArrow;
    private List<Arrow> arrowPool;//створюємо клас еровпул
    private bool isJumping;//зміна для перевірки чи ми в повітрі чи ні
    // Fixed метод який незалежно як юніті малює
    private bool isCooldown;
    [SerializeField] private Health health;
    [SerializeField] private BuffReciever buffReciever;
    [SerializeField] private float damageForce; //зміна для збереження сили віштовхування гравця при полученя урону
    [SerializeField] private Camera playerCamera; //зміна силки на камеру

    public Player(Camera playerCamera)
    {
        this.playerCamera = playerCamera;
    }

    public Player(BuffReciever buffReciever)
    {
        this.buffReciever = buffReciever;
    }

    public Player(Health health)
    {
        this.health = health;
    }
    public Health Health { get { return health; } }//властивість роботи з цим компоннтом
    [SerializeField] private Item item;//підключаєм скріпт Item
    public Player(Item item)
    {
        this.item = item;
    }

    private float bonusForce;
    private float bonusDamage;
    private float bonusHealth;
    private bool isBlockMovement;//зміна для блокування переміщення
    private UICharacterController characterController; //поле для зберігання контролєра





    private void Awake()
    {
        Instance = this;
    }//


    private void Start()
    {
        arrowPool = new List<Arrow>();//створили наший лист (клас)
        for (int i = 0; i < arrowsCout; i++)//створюємо цикл для стріли яка буде появлятися 
        {
            var arrowTemp = Instantiate(arrow, arrowSpawnPoint);
            arrowPool.Add(arrowTemp);//добавили стрілу в ліст arrowPool
            arrowTemp.gameObject.SetActive(false);//щоб стріли не літали куди попаде а дотримувалися того циклу фор який вище
            //SetActive = виключає ігровий обєкт в ігрі

        }

        buffReciever.OnBuffChanged += ApllyBuffs;//тут проходить зміна бафу

        health.OnTakeHit += TakeHit;//підписуємося зhealth скріпта на TakeHit


    }
    public void InitUIControler(UICharacterController controller)
    {
        characterController = controller;//запамятовуємо контролер
        characterController.Jump.onClick.AddListener(Jump);//метод визиває стрибок нажавши на кнопку 
        characterController.Fire.onClick.AddListener(CheckShoot);//метод визиває CheckShoot нажавши на кнопку
    }


    private void ApllyBuffs()
    {
        var forceBuff = buffReciever.Buffs.Find(t => t.type == BuffType.Force);//створюємо зміну forceBuff куди вкладуємо результат пошуку бафа з елементом лямба виразу
        var damageBuff = buffReciever.Buffs.Find(t => t.type == BuffType.Damage);//створюємо зміну damageBuff куди вкладуємо результат пошуку бафа з елементом лямба виразу
        var armorBuff = buffReciever.Buffs.Find(t => t.type == BuffType.Armor);//створюємо зміну armorBuff куди вкладуємо результат пошуку бафа з елементом лямба виразу
        bonusForce = forceBuff == null ? 0 : forceBuff.additiveBonus;//до нашої зміни присваюємо нову зміну якщо баф не викростовується то ми кладемо 0 в інших випадках використовуємо цей баф
        bonusHealth = armorBuff == null ? 0 : armorBuff.additiveBonus;
        health.SetHealth((int)bonusHealth);//дане значення бонусного здоровя зразу записуємо в скріпт Health
        bonusDamage = damageBuff == null ? 0 : damageBuff.additiveBonus;
    }
    //метод для получення урону
    private void TakeHit(int damage, GameObject attacker)
    {
        animator.SetBool("GetDamage", true); //в аніматор передаємо трігер
        animator.SetTrigger("TakeHit");
        isBlockMovement = true;//блокуємо переміщення
        rigidboby.AddForce(transform.position.x < attacker.transform.position.x ? 
            new Vector2(-damageForce, 0) : new Vector2(damageForce, 0), ForceMode2D.Impulse); //застосовуємо силу получення урону
    }
    //метод для розблокування переміщення
    public void UnBlockMovement()
    {
        animator.SetBool("GetDamage", false); //в аніматор передаємо трігер
        isBlockMovement = false;
    }



    void FixedUpdate()
    {
        Move();
        animator.SetFloat("Speed", Mathf.Abs(direction.x));//звертаємося до аніматора і кажемо що він може рухатися Math.Abs-вертає модуль від числа (тоюто наш гравець може рухатися в ліво
        CheckFall();
        


    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space))
            Jump();
#endif

    }

    private void Move()
    {
        animator.SetBool("isGrounded", groundDetection.isGrounded);//передаємо значення аніматору що ми торкаємося землі
        //if (animator != null)//не будемо звертатися до аніматора якщо не використовуєтся його силка
        if (!isJumping && !groundDetection.isGrounded)// якщо ми не на землі і не пригаємо (тобто ми в повітрі)
            animator.SetTrigger("StartFall");//запускамо кадер падіння
        isJumping = isJumping && !groundDetection.isGrounded;//якщо ми знаходимося в прижку і не торкаємося землі то зберігаємо зміну ісджампінг
        direction = Vector3.zero;//метод спочатку приймає нульове значення тобто гравець не рухається

        if (characterController.Left.IsPressed)
            direction = Vector3.left; //(-1;0)
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A))
            direction = Vector3.left;
        if (Input.GetKey(KeyCode.D))
            direction = Vector3.right;
#endif
        if (characterController.Right.IsPressed)
            direction = Vector3.right;//(1;0)
        direction *= speed;//направлення множимо на швидкість яку задаємо
        direction.y = rigidboby.velocity.y;//щоб ось у не доровнювала 0 тобто гравець має можливість пригати(не правильно буде працювати гравітація)
        if (!isBlockMovement) //перевіряємо чи не заблоковане переміщення
        rigidboby.velocity = direction;

        if (direction.x > 0)//якщо напрям гравця по х > 0 то він дивиться в право
            spriteRenderer.flipX = false;//наше зображення стає не правдиве
        if (direction.x < 0)// якщо напрям гравця по х < 0 то він дивиться в ліво
            spriteRenderer.flipX = true;//наше зображення стає правдиве

    }

    private void Jump()//блок кода для прижка
    {
        if (groundDetection.isGrounded)//якщо ми нажимаємо на спейс і наш обєкт пригає і якщо groundDetection означає що потом торкається землі то ми попадаємо в тіло 
        {
            rigidboby.AddForce(Vector2.up * (force + bonusForce), ForceMode2D.Impulse);
            animator.SetTrigger("StartJump");//аніматор приймає значення прижка
            isJumping = true;//наша зміна правдива якщо ми пригнули
        }
    }

    private void CheckShoot()
    {
        if (!isCooldown)
        {
            animator.SetTrigger("StartShoot");
        }
    }
    public void InitArrow()
    {
        currentArrow = GetArrowFromPool();
        currentArrow.SetImpulse(Vector2.right, 0, 0, this);
    }
    public void Shoot()
    {
        currentArrow.SetImpulse(Vector2.right, spriteRenderer.flipX ?
            -force * 5 : force * 5, (int)bonusDamage, this);

        StartCoroutine(Cooldown());
    }
    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isCooldown = false;
    }
    private Arrow GetArrowFromPool()//задіюємо метод пул (достаємо з цього метода стрілу)
    {
        if (arrowPool.Count > 0)//якщо кількість стріл більше нуля
        {
            var arrowTemp = arrowPool[0];//
            arrowPool.Remove(arrowTemp);//
            arrowTemp.gameObject.SetActive(true);//коли нажимаємо на стрілу вона ця зміна активуція і дозволиться стріляти
            arrowTemp.transform.parent = null;
            arrowTemp.transform.position = arrowSpawnPoint.transform.position;
            return arrowTemp;//
        }
        return Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity);//якщо стріли закінчилися то обєкт просто обновляється і повертає всі стріли назад
    }
    public void  ReturnArrowToPool(Arrow arrowTemp)
    {
        if (!arrowPool.Contains(arrowTemp))//перевіряємо чи немає двох однакових силок
            arrowPool.Add(arrowTemp);//якщо немає силки то вона добавляється


        arrowTemp.transform.parent = arrowSpawnPoint;
        arrowTemp.transform.position = arrowSpawnPoint.transform.position;
        arrowTemp.gameObject.SetActive(false);//виключаємо силку
    }
    private void CheckFall()//метод провіряє чи впав гравець чи ні
    {
        if (transform.position.y < minimalHeight && isCheatMode)
        {
            rigidboby.velocity = new Vector2(0, 0);//векторна швидкість
            transform.position = new Vector2(0, 0);//падіння героя
        }
        else if (transform.position.y < minimalHeight && !isCheatMode)
            Destroy(gameObject);//видаля гравця із сцени
    }
    private void OnDestroy()
    {
        playerCamera.transform.parent = null;//камера не привязана ні до чого
        playerCamera.enabled = true;
    }

    





}
