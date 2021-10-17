using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
public class MenuController : MonoBehaviour
{
    public Animator animatorUI;
    public Animator sceneLoader;
    public async void NewGame()
    {
        sceneLoader.SetBool("endScene", true);
        await Task.Delay(TimeSpan.FromSeconds(0.51f));
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
    public async void SelectLevel(int level)
    {
        sceneLoader.SetBool("endScene", true);
        await Task.Delay(TimeSpan.FromSeconds(0.51f));
        SceneManager.LoadScene(level);
    }
    public void Quit()
    {
        Application.Quit();
    }
    
    public async void Menu()
    {   
        sceneLoader.SetBool("endScene", true);
        await Task.Delay(TimeSpan.FromSeconds(0.51f));
        SceneManager.LoadScene(0);
    }
}
