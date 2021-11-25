using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cameras : MonoBehaviour
{   
    private List<GameObject> _cameras = new List<GameObject>();
    private int _currentCamera = 0;

    private void Start()
    {
        _cameras.AddRange(from Transform tran in GameObject.Find("Cameras").transform
                          select tran.gameObject);
    }

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