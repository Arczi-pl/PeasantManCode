using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public GameObject GameControllerObj;
    private GameController _gameController;
    
    private void Start() {
        _gameController = GameControllerObj.GetComponent<GameController>();
    }

    void OnCollisionEnter(Collision collision)
    {   
        if (_gameController.GetIsLevelEnd())
            return;

        switch (collision.gameObject.tag)
        {
            case "Wall":
                _gameController.ShowFail();
                break;
            case "Condition":
                _gameController.SetCurrnetCondition(collision.gameObject.name);
                _gameController.SetInTeleport(null);
                break;
            case "Teleport":
                _gameController.SetCurrnetCondition(collision.gameObject.name);
                _gameController.SetInTeleport(collision.gameObject);
                break;
            case "Door":
                {
                    if (_gameController.GetIsKicking())
                    {
                        Animator doorAnimator = collision.transform.parent.gameObject.GetComponent<Animator>();
                        GameObject.Find("/Audio/doorCrash").GetComponent<AudioSource>().Play();
                        doorAnimator.SetBool("FallDown", true);
                    }
                    else
                        _gameController.ShowFail();

                    break;
                }

            case "Win":
                _gameController.ShowWin();
                break;
        }
    }

}