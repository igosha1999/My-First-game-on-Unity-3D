using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventaryUIControler : MonoBehaviour
{
    
    //ініціалізуємо масив наших єчеєк
    [SerializeField] Cell[] cells;
    [SerializeField] private int cellCount;//поле для відображення єчєєк

    public InventaryUIControler(int cellCount)
    {
        this.cellCount = cellCount;
    }

    [SerializeField] private Cell cellPrefab;//зберігаємо префаб єчєєк

    public InventaryUIControler(Cell cellPrefab)
    {
        this.cellPrefab = cellPrefab;
    }

    [SerializeField] private Transform rootParent;//вказуємо родиний обєкт

    public InventaryUIControler(Transform rootParent)
    {
        this.rootParent = rootParent;
    }

    public InventaryUIControler(Cell[] cells)
    {
        this.cells = cells;
    }

    
    private void Init()
    {
        cells = new Cell[cellCount];
        //ініціалізуємо всі єчєйки
        for (int i =0; i < cellCount; i++)
        {
            cells[i] = Instantiate(cellPrefab, rootParent);
            cells[i].OnUpdateCell += UpdateInventory;//на всі дійсні єчєйки оформляється підписка в якому відтворюється обновлення інвентаря
            
        }
        cellPrefab.gameObject.SetActive(false);//виключаємо префаб з єчєйками


    }
    private void OnEnable()
    {
        if (cells == null || cells.Length <= 0)//провірка єчєк на null
            Init();//ініціалізуємо інвентарь
        UpdateInventory();//оновлюємо інвентарь


    }
    public void UpdateInventory()
    {
        //ініціалізуємо єчейки
        var inventory = GameManager.Instance.playerInventory;
        foreach (var cell in cells)
            cell.Init(null);//спочатку обнуляються всі єчєйки
        //проходимо по всім елементами нашого інвентаря і заповнюємо їх
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            //перевіряємо чи ми не перевищили єчєйки чим наший лист
            if (i < cells.Length)
                cells[i].Init(inventory.Items[i]);
        }
    }

}
