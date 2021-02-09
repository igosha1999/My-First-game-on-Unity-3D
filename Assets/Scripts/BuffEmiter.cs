using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEmiter : MonoBehaviour
{
    [SerializeField] private Buff buff;

    public BuffEmiter(Buff buff)
    {
        this.buff = buff;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.buffRecieverContainer.ContainsKey(collision.gameObject))
        {
            var reciever = GameManager.Instance.buffRecieverContainer[collision.gameObject];
            reciever.AddBuff(buff);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.Instance.buffRecieverContainer.ContainsKey(collision.gameObject))
        {
            var reciever = GameManager.Instance.buffRecieverContainer[collision.gameObject];
            reciever.RemoveBuff(buff);
        }
    }




}
[System.Serializable]//атрибут для реалізування класів
public class Buff//створюємо клас бафу
    {
    public BuffType type;
    public float additiveBonus;//
    public float multipleBones;//


}
public enum BuffType : byte//енам добавляє стани які ми будемо використовувати
{//enum- оболочка типа даних 
    Damage, Force, Armor
}

