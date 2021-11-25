using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cameras : MonoBehaviour
{   
    private List<GameObject> _cameras = new List<GameObject>();
    private int _currentCamera = 0;

    private void Start()
    {
        // adds all camera from board to list
        _cameras.AddRange(from Transform tran in GameObject.Find("Cameras").transform
                          select tran.gameObject);
    }

    // change to next camera
    // if it last in list then go to begin
    public void NextCamera()
    {
        _cameras[_currentCamera].SetActive(false);
        int nextCamera = 0;
        if (_currentCamera + 1 < _cameras.Count)
            nextCamera = _currentCamera + 1;
        _cameras[nextCamera].SetActive(true);
        _currentCamera = nextCamera;
    }
}