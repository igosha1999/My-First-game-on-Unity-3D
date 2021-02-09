using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;//зміна для роботи із зображеннями
    public HealthBar(Image health)
    {
        this.health = health;
    }//конструктор для неї

    [SerializeField] private float delta;//зміна для плавного здвигу шкали здоровя

    public HealthBar(float delta)
    {
        this.delta = delta;
    }
    private float currentHealth;//зміна яка буде означати що якщо нам наносять урон то він буде зменшуватися
    private float healthValue;//зміна для відображення текущего здоровя
    private Player player;//силка із скріпта player
    void Start()
    {
        player = Player.Instance;//визиваємо скріпт плейер
        healthValue = player.Health.CurrentHealth / 100.0f;//ініціалізуємо healthValue 100.0f - повна шкала здоровя
    }


    
    void Update()
    {//ці значення потрібні для плавного знаття здоровя у нашої шкали
        currentHealth = player.Health.CurrentHealth / 100.0f;//зміна яка приймає значення нашого здоровя 
        if (currentHealth > healthValue)//якщо наше здоровя = 100 а зміна healthValue 10(будь яке число) то значення збільшеться на delta
            healthValue += delta;
        if (currentHealth < healthValue)//якщо наше здоровя = 100 а зміна healthValue 10(будь яке число) то значення зменшиться на delta
            healthValue -= delta;
        if (currentHealth < delta)//провірка якщо здоровя = 100 то більше чим 100 воно не може бути
            healthValue = currentHealth;
        health.fillAmount = healthValue;
    }
}
