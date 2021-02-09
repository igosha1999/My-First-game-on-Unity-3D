using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Image icon;
    public Cell(Image icon)
    {
        this.icon = icon;
    }

    public Action OnUpdateCell;//коли проходить обновлення єчєйки визиваютсья методи звязані з інвентарьом в InventoryUIControler

    private  Item item;

    private void Awake()
    {
        //обноляємо нашу іконку
        icon.sprite = null;
    }
    public void Init(Item item)
    {
        //зерігаємо переданий item
        this.item = item;
        if (item == null)
            icon.sprite = null;//якщо іконка не використовується то вона = 0 тобто пуста і
        else
            icon.sprite = item.Icon;//якщо ми щось підібрали і це добавилося в інвентарь то появляється іконка в єчєйці
    }
    public void OnClickCell()
    {
        //кліки по пустим єчєйкам не працють
        if (item == null)
            return;
        GameManager.Instance.playerInventory.Items.Remove(item);//видаляємо Items з gamevanager
        Buff buff = new Buff//створюємо новий баф
        {//це параметри нового бафу
            type = item.Type,
            additiveBonus = item.Value
        };
        GameManager.Instance.playerInventory.buffReciever.AddBuff(buff);//добавляємо новий баф в gamemanager
        if (OnUpdateCell != null)//якщо на дєрєгат підписані якісь методи то ми його визиваємо
            OnUpdateCell();
    }
        
}
