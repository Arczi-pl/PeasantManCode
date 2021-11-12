using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cameras : MonoBehaviour
{   
    private List<GameObject> cameras = new List<GameObject>();
    private int currentCamera = 0;
    
    private void Start()
    {
        cameras.AddRange(from Transform tran in GameObject.Find("Cameras").transform
                        select tran.gameObject);
    }

    public void NextCamera()
    {
        cameras[currentCamera].SetActive(false);
        int nextCamera = 0;
        if (currentCamera + 1 < cameras.Count)
            nextCamera = currentCamera + 1;
        cameras[nextCamera].SetActive(true);
        currentCamera = nextCamera;
    }
}