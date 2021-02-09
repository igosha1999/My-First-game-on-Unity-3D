using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponents : MonoBehaviour , IOjestDestroyer
{
    [SerializeField] private ItemType type;

    public ItemComponents(ItemType type)
    {
        this.type = type;
    }

    [SerializeField] private SpriteRenderer spriteRenderer;

    public ItemComponents(SpriteRenderer spriteRenderer)
    {
        this.spriteRenderer = spriteRenderer;
    }
    [SerializeField] private Animator animator;

    public ItemComponents(Animator animator)
    {
        this.animator = animator;
    }

    private Item item;
    public Item Item 
    {
        get { return item; }
    }

    public void Destroy(GameObject gameObject)
    {
        animator.SetTrigger("StartDestroy");
    }

    void Start()
    {
        item = GameManager.Instance.itemDataBase.GetItemOfID((int)type);
        spriteRenderer.sprite = item.Icon;
        GameManager.Instance.itemContainer.Add(gameObject, this);
    }

    // Update is called once per frame
    public void EndDestroy()
    {
        MonoBehaviour.Destroy(gameObject);
    }
}

public enum ItemType
{
    ArmorPotion = 1,
    ForcePotion = 2,
    DamagePotion = 3
}