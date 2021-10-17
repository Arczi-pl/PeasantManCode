using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;

public class GameController : MonoBehaviour
{   
    public GameObject charakterAnimator;
    public GameObject EndStagePanel;
    public Text commendList;
    public GameObject WinPanel;
    public GameObject FailPanel;
    public bool isLevelEnd, isKicking;

    public GameObject controllPanel;
    public GameObject showControll;
    public Animator sceneLoader;

    private void Start()
    {
        Cameras cameras = gameObject.AddComponent<Cameras>() as Cameras;

        CommandsExecutor mc = gameObject.AddComponent<CommandsExecutor>() as CommandsExecutor;
        mc.charakterAnimator = charakterAnimator;
        mc.EndStagePanel = EndStagePanel;
        mc.commendList = commendList;
        mc.gameController = this;
    }

    public async void Reload()
    {   
        sceneLoader.SetBool("endScene", true);
        await Task.Delay(TimeSpan.FromSeconds(0.51f));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public async void Menu()
    {   
        sceneLoader.SetBool("endScene", true);
        await Task.Delay(TimeSpan.FromSeconds(0.51f));
        SceneManager.LoadScene(0);
    }

    public void NextCamera()
    {
        gameObject.GetComponent<Cameras>().NextCamera();
    }

    public async void NextLevel()
    {
        sceneLoader.SetBool("endScene", true);
        await Task.Delay(TimeSpan.FromSeconds(0.51f));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartCommends()
    {
        gameObject.GetComponent<CommandsExecutor>().StartCommends();
    }

    public void ShowFail()
    {
        isLevelEnd = true;
        GameObject.Find("UI/ControlPanel").SetActive(false);
        GameObject.Find("UI/CommandsPanel").SetActive(false);
        FailPanel.SetActive(true);
    }

    public void ShowWin()
    {
        isLevelEnd = true;
        GameObject.Find("UI/ControlPanel").SetActive(false);
        GameObject.Find("UI/CommandsPanel").SetActive(false);
        WinPanel.SetActive(true);
    }

    public void ShowAndHideControllPanel()
    {
        Animator controllPanelAnimator = controllPanel.GetComponent<Animator>();
        Animator showControllAnimator = showControll.GetComponent<Animator>();
        bool isShowing = controllPanelAnimator.GetBool("Show");
        controllPanelAnimator.SetBool("Show", !isShowing);
        showControllAnimator.SetBool("Show", !isShowing);
    }
}
