using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartLVL : MonoBehaviour
{
   public void GotoLVL()
    {
        SceneManager.LoadScene(1);
    }
}
