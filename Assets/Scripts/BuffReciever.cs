using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffReciever : MonoBehaviour
{
    private List<Buff> buffs;//створюємо список де будемо хранити всі бафи
    public Action OnBuffChanged;//перевіряємо коли сталися зміни з нашими бафами

    public List<Buff> Buffs
    {
        get { return buffs; }
    }



    private void Start()
    {
        buffs = new List<Buff>();//визиваємо список 
        GameManager.Instance.buffRecieverContainer.Add(gameObject, this);

    }
    public void AddBuff(Buff buff)//перевіряємо чи в наший менедр не добавився новйи баф
    {
        if (!buffs.Contains(buff))//перевіряємо чи існує новий баф в листі
            buffs.Add(buff);//якщо не існує то добавляємо його
        if (OnBuffChanged != null)//якщо ми добавляємо баф або видаляємо його то проходить виклик OnBuffChanged();
            OnBuffChanged();//це ділєгат всіх бафів 
    }
    public void RemoveBuff(Buff buff)
    {
        if (buffs.Contains(buff))//якщо баф не використовується то він видаляється
            buffs.Remove(buff);
        if (OnBuffChanged != null)
            OnBuffChanged();
    }    
}
