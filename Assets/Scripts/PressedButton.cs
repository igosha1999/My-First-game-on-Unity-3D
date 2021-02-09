using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedButton : MonoBehaviour
{
    private bool isPressed; //зміна Трігер перевіряє чи нажата кнопка чи ні
    
    public bool IsPressed //властивості для зміни isPressed
    {
        get { return isPressed; }
    }

    public void OnPointerDown() //метод буде виконуватися коли ми наживаємо на кнопку (1 метод)
    {
        isPressed = true;//
    }
    
    public void OnPointerUp()//метод буде виконуватися коли ми відпустили палець від кнопки (2 метод)
    {
        isPressed = false;
    }
}
