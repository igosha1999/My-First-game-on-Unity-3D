using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public Coin(Animator animator)
    {
        this.animator = animator;
    }

    public void Start()
    {
        GameManager.Instance.coinContainer.Add(gameObject, this);
    }



    public void StartDestroy()
    {
        animator.SetTrigger("StartDestroy");
    }
    public void EndDestroy()
    {
        Destroy(gameObject);
    }
   


}
