using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{   
    public GameObject CharacterObj, Tutorial;
    public Text[] CommendsFromAllProc;
    public Animator WinPanel, FailPanel;
    public Animator SceneLoader, UIAnimator;
    public Database Database;
    private string _currentCondition;
    private CommandsExecutor _commandsExecutor;
    private Cameras _cameras;
    private bool _isLevelEnd, _isKicking, _isLevelStart;

    // create scripts for cameras and commands execute
    private void Start()
    {
        _cameras = gameObject.AddComponent<Cameras>() as Cameras;

        _commandsExecutor = gameObject.AddComponent<CommandsExecutor>() as CommandsExecutor;
        _commandsExecutor.SetCharacterObj(CharacterObj);
        _commandsExecutor.SetCommendsFromAllProc(CommendsFromAllProc);
        _commandsExecutor.SetGameController(this);

        SetIsLevelStart(false);
        SetCurrnetCondition("All");
    }

    public void SetTutotialVisiable(bool visiable)
    {
        Tutorial.SetActive(visiable);
    }

    public void SetInTeleport(GameObject teleport)
    {
        _commandsExecutor.SetTeleport(teleport);
    }

    public void SetCurrnetCondition(string cond)
    {
        _currentCondition = cond;
        _commandsExecutor.SetCurrentCondition(cond);
    }

    // reload current level
    public void Reload()
    {   
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    // go to menu
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
        GameObject.Find("/Audio/changePage").GetComponent<AudioSource>().Play();
        UIAnimator.SetBool("ShowSecond", !UIAnimator.GetBool("ShowSecond"));
    }

    public void StartCommends()
    {
        if (!_isLevelStart){
            gameObject.GetComponent<CommandsExecutor>().StartCommends();
            _isLevelStart = true;
        }
    }

    public void ShowFail()
    {
        SetIsLevelEnd(true);
        FailPanel.SetBool("isLevelEnd", true);
    }

    public void ShowWin()
    {
        Database.UnlockLevel(SceneManager.GetActiveScene().buildIndex + 1);
        SetIsLevelEnd(true);
        WinPanel.SetBool("isLevelEnd", true);
    }

    public bool GetIsKicking()
    {
        return _isKicking;
    }

    public void SetIsKicking(bool value)
    {
        _isKicking = value;
    }

    public bool GetIsLevelStart()
    {
        return _isLevelStart;
    }

    public void SetIsLevelEnd(bool value)
    {
        _isLevelEnd = value;
    }

    public void SetIsLevelStart(bool value)
    {
        _isLevelStart = value;
    }

    public bool GetIsLevelEnd()
    {
        return _isLevelEnd;
    }
    
    // load level, if id=11, then go to menu
    IEnumerator LoadLevel(int levelId)
    {
        SceneLoader.SetBool("endScene", true);
        yield return new WaitForSeconds(1f);
        if (levelId == 11)
            levelId = 0;
        SceneManager.LoadScene(levelId);
    }

    
}
