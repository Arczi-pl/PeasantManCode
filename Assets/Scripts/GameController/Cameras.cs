using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cameras : MonoBehaviour
{   
    List<GameObject> cameras = new List<GameObject>();
    int currentCamera = 0;
    
    private void Start()
    {
        foreach (Transform tran in GameObject.Find("Cameras").transform)
        {
            cameras.Add(tran.gameObject);
        }
    }

    public void NextCamera()
    {
        cameras[currentCamera].SetActive(false);
        int nextCamera = 0;
        if (currentCamera + 1 < cameras.Count)
        {
            nextCamera = currentCamera + 1;
        }
        cameras[nextCamera].SetActive(true);
        currentCamera = nextCamera;
    }
}