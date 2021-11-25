using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        // allow only one audio object to exist
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Audio");
        if(objs.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
}
