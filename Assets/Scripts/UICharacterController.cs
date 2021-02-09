using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterController : MonoBehaviour
{
    public Text scoreText;
    public CollectionCoins playerScore;

    [SerializeField] private PressedButton left; //створюємо реалізовані силки на наші кнопки

    public UICharacterController(PressedButton left, PressedButton right, Button fire)
    {
        this.left = left;
        this.right = right;
        this.fire = fire;
    }

    [SerializeField] private PressedButton right; //створюємо реалізовані силки на наші кнопки

    private void Update()
    {
        scoreText.text = playerScore.coins.ToString();
    }

    public PressedButton Left  //властивості для використання цих змін
    {
        get { return left; }
    }
    public PressedButton Right
    {
        get { return right; }
    }//властивості для використання цих змін

    [SerializeField] private Button fire;//створюємо реалізовані силки на наші кнопки

    public UICharacterController(Button fire, Button jump)
    {
        this.fire = fire;
        this.jump = jump;
    }

    [SerializeField] private Button jump;//створюємо реалізовані силки на наші кнопки

    

    public Button Fire
    {
        get { return fire; }
    }//властивості для використання цих змін
    public Button Jump
    {
        get { return jump; }
    }//властивості для використання цих змін
    void Start()
    {
        Player.Instance.InitUIControler(this);//звертаємося до Player
    }

    
}
