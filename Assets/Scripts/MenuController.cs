using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Animator animatorUI;
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowLevelSelect(bool show)
    {
        animatorUI.SetBool("selectLevelShow", show);
    }

    public void ShowOptions(bool show)
    {
        animatorUI.SetBool("optionsShow", show);
    }

    public void ShowAuthor(bool show)
    {
        animatorUI.SetBool("authorShow", show);
    }
    public void SelectLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void Quit()
    {
        Application.Quit();
    }
    
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
