using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public Animator AnimatorUI, SceneLoader;
    public Database Database;
    public AudioClip Track1, Track2, Track3;
    private string _currentTrackNum;
    private float _currentVolumeValue;
    private AudioSource _currentTrack, _putCommand;
    private Slider _currentVolume;

    private void Start() {
        _currentTrack = GameObject.Find("/Audio/ThemeSong").GetComponent<AudioSource>();
        _currentVolume = GameObject.Find("/UI/Options/Volume/Slider").GetComponent<Slider>();
        _putCommand = GameObject.Find("/Audio/putCommand").GetComponent<AudioSource>();

        _currentVolumeValue = _currentTrack.volume;
        _currentVolume.value = _currentVolumeValue;
        
        _currentTrackNum = _currentTrack.clip.name.Substring(5);
        GameObject.Find("/UI/Options/Track/" + _currentTrackNum).GetComponent<Image>().color = new Color(0, 0, 0, 0.254902f);
    }

    public void NewGame()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void ShowLevelSelect(bool show)
    {   
        string[] unlockedLevels = Database.GetUnlockedLevels();
        foreach(string level in unlockedLevels)
        {
            GameObject.Find("/UI/SelectLevel/Levels/" + level).SetActive(true);
        }
        _putCommand.Play();
        AnimatorUI.SetBool("selectLevelShow", show);
    }

    public void ShowOptions()
    {
        _putCommand.Play();
        AnimatorUI.SetBool("optionsShow", !AnimatorUI.GetBool("optionsShow"));
    }

    public void ShowAbout()
    {
        _putCommand.Play();
        AnimatorUI.SetBool("aboutShow", !AnimatorUI.GetBool("aboutShow"));
    }

    public void SelectLevel(int level)
    {
        StartCoroutine(LoadLevel(level));
    }

    public void SelectTrack(int newTrackNum)
    {   
        GameObject.Find("/UI/Options/Track/" + newTrackNum).GetComponent<Image>().color = new Color(0, 0, 0, 0.254902f);
        GameObject.Find("/UI/Options/Track/" + _currentTrackNum).GetComponent<Image>().color = new Color(255, 255, 255, 0.254902f);

        // get value of Track + <ID> variable
        _currentTrack.clip = (AudioClip)this.GetType().GetField("Track" + newTrackNum).GetValue(this);
        _currentTrack.Play();
        
        _currentTrackNum = newTrackNum.ToString();
    }

    public void ChangeTrackVolume()
    {   
        AudioSource[] allSounds = GameObject.Find("/Audio").GetComponentsInChildren<AudioSource>();
        foreach(AudioSource sound in allSounds)
            sound.volume = _currentVolume.value;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ResetProgress()
    {
        _putCommand.Play();
        Database.ResetProgress();
        // hide levels in select level panel
        for (int i = 2; i<= 10; i++)
            GameObject.Find("/UI/SelectLevel/Levels/" + i).SetActive(false);
    }

    IEnumerator LoadLevel(int levelId)
    {   
        GameObject.Find("/Audio/putCommand").GetComponent<AudioSource>().Play();
        SceneLoader.gameObject.SetActive(true);
        SceneLoader.SetBool("endScene", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelId);
    }
}
