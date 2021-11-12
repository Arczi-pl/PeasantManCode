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
        if (gameController.isLevelEnd)
            return;

        switch (collision.gameObject.tag)
        {
            case "Wall":
                gameController.ShowFail();
                break;
            case "Condition":
                gameController.SetCurrnetCondition(collision.gameObject.name);
                gameController.SetInTeleport(null);
                break;
            case "Teleport":
                gameController.SetCurrnetCondition(collision.gameObject.name);
                gameController.SetInTeleport(collision.gameObject);
                break;
            case "Door":
                {
                    if (gameController.isKicking)
                    {
                        Animator doorAnimator = collision.transform.parent.gameObject.GetComponent<Animator>();
                        doorAnimator.SetBool("FallDown", true);
                    }
                    else
                        gameController.ShowFail();

                    break;
                }

            case "Win":
                gameController.ShowWin();
                break;
        }
    }

}