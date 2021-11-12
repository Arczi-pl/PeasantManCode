using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{   
    public GameObject charakterAnimator, tutorial;
    public Text[] commendsFromAllProc;
    public Animator WinPanel, FailPanel;
    public bool isLevelEnd, isKicking;
    public Animator sceneLoader, UIAnimator;
    public Database database;
    public string currentCondition;
    CommandsExecutor mc;
    Cameras cameras;
    private void Start()
    {
        cameras = gameObject.AddComponent<Cameras>() as Cameras;

        mc = gameObject.AddComponent<CommandsExecutor>() as CommandsExecutor;
        mc.charakterAnimator = charakterAnimator;
        mc.commendsFromAllProc = commendsFromAllProc;
        mc.gameController = this;
        mc.currentCondition = currentCondition;
        mc.teleport = null;

    }
    public void SetTutotialVisiable(bool visiable)
    {
        tutorial.SetActive(visiable);
    }
    public void SetInTeleport(GameObject teleport)
    {
        mc.teleport = teleport;
    }
    public void SetCurrnetCondition(string cond)
    {
        currentCondition = cond;
        mc.currentCondition = cond;
    }
    public  void Reload()
    {   
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void Menu()
    {   
        StartCoroutine(LoadLevel(0));
    }

    public void NextCamera()
    {
        gameObject.GetComponent<Cameras>().NextCamera();
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void ChangePage()
    {
        UIAnimator.SetBool("ShowSecond", !UIAnimator.GetBool("ShowSecond"));
    }
    public void StartCommends()
    {
        gameObject.GetComponent<CommandsExecutor>().StartCommends();
    }

    public void ShowFail()
    {
        isLevelEnd = true;
        FailPanel.SetBool("isLevelEnd", true);
    }

    public void ShowWin()
    {
        database.UnlockLevel(SceneManager.GetActiveScene().buildIndex + 1);
        isLevelEnd = true;
        WinPanel.SetBool("isLevelEnd", true);
    }

    IEnumerator LoadLevel(int levelId)
    {
        sceneLoader.SetBool("endScene", true);
        yield return new WaitForSeconds(1f);
        if (levelId == 11)
            levelId = 0;
        SceneManager.LoadScene(levelId);
    }

    
}
