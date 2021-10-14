using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandsExecutor : MonoBehaviour
{   
    public GameObject charakter;
    public GameObject EndStagePanel;
    public GameController gameController;

    Animator animator;
    Vector3 startPosition;

    bool isGoingForward;
    int goForwardCount;

    bool isTurningLeft;
    bool isTurningRight;
    int turnCount;

    public Text commendList;
    int currentCommend;
    string[] commends;

    // Start is called before the first frame update
    void Start()
    {
        animator = charakter.GetComponent<Animator>();
        isGoingForward = false;
        goForwardCount = 200;

        isTurningLeft = false;
        isTurningRight = false;
        turnCount = 90;

        currentCommend = -1;

        startPosition = charakter.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        GoForwardLogic();
        TurnLeftLogic();
        TurnRightLogic();
    }


    void GoForward()
    {
        animator.SetBool("isSneakWalk", true);
        isGoingForward = true;
    }


    void GoForwardLogic()
    {
        if (isGoingForward)
        {
            charakter.transform.Translate(0f, 0f, 0.01f);
            goForwardCount -= 1;
            if (goForwardCount == 0)
            {   
                isGoingForward = false;
                animator.SetBool("isSneakWalk", false);
                goForwardCount = 200;
                Vector3 tmp = charakter.transform.position;
                tmp.x = Mathf.Round(tmp.x);
                tmp.y = Mathf.Round(tmp.y);
                tmp.z = Mathf.Round(tmp.z);
                charakter.transform.position = tmp;
                NextCommend();
            }
        }
    }


    void TurnLeft()
    {
        animator.SetBool("isTurningLeft", true);
        isTurningLeft = true;
    }


    void TurnLeftLogic()
    {
        if (isTurningLeft)
        {
            charakter.transform.Rotate(0f, -1f, 0f);
            turnCount -= 1;
            if (turnCount == 0)
            {   
                isTurningLeft = false;
                animator.SetBool("isTurningLeft", false);
                turnCount = 90;
                NextCommend();
            }
        }
    }


    void TurnRight()
    {
        animator.SetBool("isTurningRight", true);
        isTurningRight = true;
    }


    void TurnRightLogic()
    {
        if (isTurningRight)
        {
            charakter.transform.Rotate(0f, 1f, 0f);
            turnCount -= 1;
            if (turnCount == 0)
            {   
                isTurningRight = false;
                animator.SetBool("isTurningRight", false);
                turnCount = 90;
                NextCommend();
            }
        }
    }

    public void StartCommends()
    {   
        commends = commendList.text.Remove(commendList.text.Length - 1, 1).Split(';');
        NextCommend();
    }
    

    void NextCommend()
    {
        currentCommend += 1;
        if (currentCommend == commends.Length)
        {   
            if (!gameController.isLevelEnd)
            {
                gameController.ShowFail();
            }
            return;
        }

        string commend = commends[currentCommend];
        if (commend == "go") {
            GoForward();
        }
        else if (commend == "left")
        {
            TurnLeft();
        }
        else if (commend == "right")
        {
            TurnRight();
        }
    }
}
