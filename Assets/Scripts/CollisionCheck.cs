using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionCheck : MonoBehaviour
{
    public GameObject gameControllerObj;
    private GameController gameController;

    private void Start() {
        gameController = gameControllerObj.GetComponent<GameController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!gameController.isLevelEnd & collision.gameObject.tag == "Wall")
        {
            gameController.ShowFail();
        }

        if (!gameController.isLevelEnd & collision.gameObject.tag == "Win")
        {
            gameController.ShowWin();
        }
    }

}