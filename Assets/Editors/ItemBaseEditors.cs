using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;

//привязуємо скріпт ItemBase
[CustomEditor(typeof(ItemBase))]

public class ItemBaseEditors : Editor
{
    //зміна для викоритсання цього скрипта
    private ItemBase itemBase ;


    private void Awake()
    {
        //визиваємо цей скрипт
        itemBase = (ItemBase)target;
    }



    public override void OnInspectorGUI()
    {
        //добавляємо кнопку
        if (GUILayout.Button("New Item"))
            itemBase.CreateItem();
        if (GUILayout.Button("Remove"))
            itemBase.RemoveItem();
        if (GUILayout.Button("=>"))
            itemBase.NextItem();
        if (GUILayout.Button("<="))
            itemBase.PrevItem();



        base.OnInspectorGUI();
    }
}
