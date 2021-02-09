using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    // масив зберішання панелів(вибір уровня, магазин)
    public GameObject[] gamePanels;

    //метод який відображає панель
    public void ShowChangePanel(int index)
    {
        gamePanels[index].SetActive(true);
    }
    //метод який ховає панель
    public void HideChangePanel(int index)
    {
        gamePanels[index].SetActive(false);
    }

    

    


    [SerializeField] private InputField nameField;

    public Menu(InputField nameField)
    {
        this.nameField = nameField;
    }

    private void Start()
    {
        //PlayerPrefs.HasKey("Player_Name");//перевіряємо чи наший ключ існує якщо існує то переходимо нище
        //nameField.text = PlayerPrefs.GetString("Player_Name");//коли ведемо наше імя воно збережеться
        
    }
    /*public void OnEndEditName()
    {
        PlayerPrefs.SetString("Player_Name", nameField.text);//строчка кода відповідаєє за імя яке ми вказуємо в меню і зберігає його

    }*/
    public void OnClickPlay()
    {
            SceneManager.LoadScene(1);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
