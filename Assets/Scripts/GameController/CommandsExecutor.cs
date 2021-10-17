using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;
public class CommandsExecutor : MonoBehaviour
{   
    public GameObject charakterAnimator;
    public GameObject EndStagePanel;
    public GameController gameController;

    Animator animatorPossition, animatorMove;
    Vector3 startPosition;

    bool isGoingForward;
    int goForwardCount;

    bool isTurningLeft;
    bool isTurningRight;
    int turnCount;

    public Text commendList;
    int currentCommend;
    string[] commends;
    Transform charakter;

    // Start is called before the first frame update
    void Start()
    {
        animatorPossition = charakterAnimator.GetComponent<Animator>();
        charakter = charakterAnimator.transform.GetChild(0);
        animatorMove = charakter.GetComponent<Animator>();
        currentCommend = -1;
        startPosition = charakterAnimator.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    async void GoForward()
    {
        animatorPossition.SetBool("isGoingForward", true);
        animatorMove.SetBool("isGoingForward", true);
        await Task.Delay(TimeSpan.FromSeconds(1f));
        charakterAnimator.transform.Translate(0f, 0f, 2.0f);
        animatorPossition.SetBool("isGoingForward", false);
        animatorMove.SetBool("isGoingForward", false);
        await Task.Delay(TimeSpan.FromSeconds(0.3f));
        NextCommend();
    }

    async void TurnLeft()
    {
        animatorPossition.SetBool("isTurningLeft", true);
        animatorMove.SetBool("isTurningLeft", true);
        await Task.Delay(TimeSpan.FromSeconds(1f));
        charakterAnimator.transform.Rotate(0f, -90f, 0f);
        animatorPossition.SetBool("isTurningLeft", false);
        animatorMove.SetBool("isTurningLeft", false);
        await Task.Delay(TimeSpan.FromSeconds(0.3f));
        NextCommend();

    }

    async void TurnRight()
    {   
        animatorPossition.SetBool("isTurningRight", true);
        animatorMove.SetBool("isTurningRight", true);
        await Task.Delay(TimeSpan.FromSeconds(1f));
        charakterAnimator.transform.Rotate(0f, 90f, 0f);
        animatorPossition.SetBool("isTurningRight", false);
        animatorMove.SetBool("isTurningRight", false);
        await Task.Delay(TimeSpan.FromSeconds(0.3f));
        NextCommend();
    }
    async void Kick()
    {
        gameController.isKicking = true;
        animatorPossition.SetBool("isKicking", true);
        animatorMove.SetBool("isKicking", true);
        await Task.Delay(TimeSpan.FromSeconds(1f));
        charakterAnimator.transform.Translate(0f, 0f, 0.25f);
        animatorPossition.SetBool("isKicking", false);
        animatorMove.SetBool("isKicking", false);
        await Task.Delay(TimeSpan.FromSeconds(0.3f));
        gameController.isKicking = false;
        NextCommend();
    }

    public void StartCommends()
    {   
        commends = commendList.text.Remove(commendList.text.Length - 1, 1).Split(';');
        NextCommend();
    }
    

    void NextCommend()
    {   
        if (gameController.isLevelEnd)
        {
            return;
        }
        
        currentCommend += 1;
        if (currentCommend == commends.Length)
        {   
            gameController.ShowFail();
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
        else if (commend == "kick")
        {
            Kick();
        }
    }
}
