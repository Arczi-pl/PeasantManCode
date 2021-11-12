using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class MenuController : MonoBehaviour
{
    public Animator animatorUI;
    public Animator sceneLoader;
    public Database database;
    public AudioClip track1, track2, track3;
    string currentTrackNum;
    float currentVolumeValue;
    AudioSource currentTrack, putCommand;
    Slider currentVolume;
    private void Start() {
        currentTrack = GameObject.Find("/Audio/ThemeSong").GetComponent<AudioSource>();
        currentVolume = GameObject.Find("/UI/Options/Volume/Slider").GetComponent<Slider>();
        putCommand = GameObject.Find("/Audio/putCommand").GetComponent<AudioSource>();

        currentVolumeValue = currentTrack.volume;
        currentVolume.value = currentVolumeValue;
        
        currentTrackNum = currentTrack.clip.name.Substring(5);
        GameObject.Find("/UI/Options/Track/" + currentTrackNum).GetComponent<Image>().color = new Color(0, 0, 0, 0.254902f);
    }
    public void NewGame()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void ShowLevelSelect(bool show)
    {   
        string[] unlockedLevels = database.getUnlockedLevels();
        foreach(string level in unlockedLevels)
        {
            GameObject.Find("/UI/SelectLevel/Levels/" + level).SetActive(true);
        }
        putCommand.Play();
        animatorUI.SetBool("selectLevelShow", show);
    }

    public void ShowOptions()
    {
        putCommand.Play();
        animatorUI.SetBool("optionsShow", !animatorUI.GetBool("optionsShow"));
    }

    public void SelectLevel(int level)
    {
        StartCoroutine(LoadLevel(level));
    }
    public void SelectTrack(int newTrackNum)
    {   
        GameObject.Find("/UI/Options/Track/" + newTrackNum).GetComponent<Image>().color = new Color(0, 0, 0, 0.254902f);
        GameObject.Find("/UI/Options/Track/" + currentTrackNum).GetComponent<Image>().color = new Color(255, 255, 255, 0.254902f);

        currentTrack.clip = (AudioClip)this.GetType().GetField("track" + newTrackNum).GetValue(this);
        currentTrack.Play();
        
        currentTrackNum = newTrackNum.ToString();
    }

    public void ChangeTrackVolume()
    {
        currentTrack.volume = currentVolume.value;
    }
    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadLevel(int levelId)
    {   
        GameObject.Find("/Audio/putCommand").GetComponent<AudioSource>().Play();
        sceneLoader.gameObject.SetActive(true);
        sceneLoader.SetBool("endScene", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelId);
    }
}
