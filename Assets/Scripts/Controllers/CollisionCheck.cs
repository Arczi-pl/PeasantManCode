using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public GameObject GameControllerObj;
    private GameController _gameController;
    
    private void Start() {
        _gameController = GameControllerObj.GetComponent<GameController>();
    }

    // checks what the character collided with
    void OnCollisionEnter(Collision collision)
    {   
        // if level end then return
        if (_gameController.GetIsLevelEnd())
            return;

        switch (collision.gameObject.tag)
        {
            case "Wall":
                // if wall then show fail
                _gameController.ShowFail();
                break;
            case "Condition":
                // if condition field then change cond
                _gameController.SetCurrnetCondition(collision.gameObject.name);
                _gameController.SetInTeleport(null);
                break;
            case "Teleport":
                // if teleport field then change teleport
                _gameController.SetCurrnetCondition(collision.gameObject.name);
                _gameController.SetInTeleport(collision.gameObject);
                break;
            case "Door":
                {   
                    // if door then check if character is kicking now
                    if (_gameController.GetIsKicking())
                    {
                        // if yes then break down the door
                        Animator doorAnimator = collision.transform.parent.gameObject.GetComponent<Animator>();
                        GameObject.Find("/Audio/doorCrash").GetComponent<AudioSource>().Play();
                        doorAnimator.SetBool("FallDown", true);
                    }
                    else
                        // if no then show fail
                        _gameController.ShowFail();

                    break;
                }

            case "Win":
                // if win field then show win
                _gameController.ShowWin();
                break;
        }
    }

}