using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;
using System.Linq;

public class CommandsExecutor : MonoBehaviour
{   
    public GameObject charakterAnimator, teleport;
    public GameController gameController;
    public Text[] commendsFromAllProc;
    public string currentCondition;
    private Animator animatorPossition, animatorMove;
    private Transform charakter;
    private int[] beforeProcCommend, beforeProcProc;
    private string[][] allCommends;
    private int[][] mapSlotsToCommands;
    private Color activeIconColor, normalIconColor;
    private Image previousIconImage;

    private void Start()
    {
        animatorPossition = charakterAnimator.GetComponent<Animator>();
        charakter = charakterAnimator.transform.GetChild(0);
        animatorMove = charakter.GetComponent<Animator>();
        allCommends = new string[4][];
        mapSlotsToCommands = new int[4][];
        activeIconColor = new Color(100, 255, 0, 1);
        normalIconColor = new Color(255, 255, 255, 1);
        previousIconImage = null;
        beforeProcCommend = new int[4] {0, 0, 0, 0};
        beforeProcProc = new int[4] {0, 0, 0, 0};
    }

    private async void GoForward(int procID, int commendID)
    {
        animatorPossition.SetBool("isGoingForward", true);
        animatorMove.SetBool("isGoingForward", true);
        await Task.Delay(TimeSpan.FromSeconds(1f));
        charakterAnimator.transform.Translate(0f, 0f, 2.0f);
        animatorPossition.SetBool("isGoingForward", false);
        animatorMove.SetBool("isGoingForward", false);
        await Task.Delay(TimeSpan.FromSeconds(0.3f));
        ExecuteCommend(procID, commendID + 1);
    }

    private async void TurnLeft(int procID, int commendID)
    {
        animatorPossition.SetBool("isTurningLeft", true);
        animatorMove.SetBool("isTurningLeft", true);
        await Task.Delay(TimeSpan.FromSeconds(1f));
        charakterAnimator.transform.Rotate(0f, -90f, 0f);
        animatorPossition.SetBool("isTurningLeft", false);
        animatorMove.SetBool("isTurningLeft", false);
        await Task.Delay(TimeSpan.FromSeconds(0.3f));
        ExecuteCommend(procID, commendID + 1);

    }

    private async void TurnRight(int procID, int commendID)
    {   
        animatorPossition.SetBool("isTurningRight", true);
        animatorMove.SetBool("isTurningRight", true);
        await Task.Delay(TimeSpan.FromSeconds(1f));
        charakterAnimator.transform.Rotate(0f, 90f, 0f);
        animatorPossition.SetBool("isTurningRight", false);
        animatorMove.SetBool("isTurningRight", false);
        await Task.Delay(TimeSpan.FromSeconds(0.3f));
        ExecuteCommend(procID, commendID + 1);
    }
    private async void Kick(int procID, int commendID)
    {
        gameController.isKicking = true;
        animatorMove.SetBool("isKicking", true);
        animatorPossition.SetBool("isKicking", false);
        await Task.Delay(TimeSpan.FromSeconds(1.5f));
        animatorPossition.SetBool("isKicking", false);
        animatorMove.SetBool("isKicking", false);
        await Task.Delay(TimeSpan.FromSeconds(0.3f));
        gameController.isKicking = false;
        ExecuteCommend(procID, commendID + 1);
    }

    private async void Teleport(int procID, int commendID)
    {
        animatorMove.SetBool("isTeleportDown", true);
        await Task.Delay(TimeSpan.FromSeconds(1f));
        animatorMove.SetBool("isTeleportDown", false);
        if(teleport)
        {
            charakterAnimator.transform.position = teleport.GetComponent<Teleport>().secondTeleport.transform.position;
            GameObject.Find("/Audio/teleport").GetComponent<AudioSource>().Play();
        }
        animatorMove.SetBool("isTeleportUp", true);
        await Task.Delay(TimeSpan.FromSeconds(1f));
        animatorMove.SetBool("isTeleportUp", false);
        await Task.Delay(TimeSpan.FromSeconds(0.3f));
        ExecuteCommend(procID, commendID + 1);
    }
    private void GetCommandsFromLists()
    {
        for(int i = 0 ; i < commendsFromAllProc.Length ; i++)
        {
            string[] procCommends = new string[]{};
            if (!(String.IsNullOrEmpty(commendsFromAllProc[i].text)))
            {
                procCommends = commendsFromAllProc[i].text.Remove(commendsFromAllProc[i].text.Length - 1, 1).Split(';');
            }
            allCommends[i] = procCommends;
        }
    }
    private void MapCommandsToSlots()
    {
        int currentCommand = 0;
        for(int procNum = 0; procNum < 4; procNum++) {
            mapSlotsToCommands[procNum] = new int[9];
            currentCommand = 0;
            for(int slotID = 1; slotID <= 9; slotID++)
            {   
                GameObject slot = GetSlot(procNum, slotID);
                if(slot)
                {
                    if(slot.transform.childCount != 0)
                    {
                        mapSlotsToCommands[procNum][currentCommand] = slotID;
                        currentCommand++;
                    }
                }
                
            }
        }
    }
    
    private GameObject GetSlot(int procID, int slotID)
    {   
        string slotPath = "/UI";
        switch (procID)
        {
            case 0:
                slotPath += "/CommandsPanel/MainSlot";
                break;
            case 1:
                slotPath += "/CommandsPanel/Proc1Slot";
                break;
            case 2:
                slotPath += "/CommandsPanel2/Proc2Slot";
                break;
            case 3:
                slotPath += "/CommandsPanel2/Proc3Slot";
                break;
        }
        switch (slotID)
        {
            case int n when n >=7:
                slotPath += "/7-9/";
                break;
            case int n when n >= 4:
                slotPath += "/4-6/";
                break;
            case int n when n <= 3:
                slotPath += "/1-3/";
                break;
        }
        slotPath += (slotID);
        GameObject slot = GameObject.Find(slotPath);
        return slot;
    }

    private void SetActiveIcon(int procID, int commendID)
    {
        GameObject slot = GetSlot(procID, mapSlotsToCommands[procID][commendID]);
        GameObject icon = slot.transform.GetChild(0).gameObject;
        Image iconImage = icon.GetComponent<Image>();
        iconImage.color = activeIconColor;
        if (previousIconImage)
        {
            previousIconImage.color = normalIconColor;
        }
        previousIconImage = iconImage;
    }

    private void ExecuteCommend(int procID, int commendID)
    {   
        if (gameController.isLevelEnd)
            return;
        
        if (commendID >= allCommends[procID].Length)
        {   
            if (procID == 0)
                gameController.ShowFail();
            else
                ExecuteCommend(beforeProcProc[procID], beforeProcCommend[procID] + 1);
            return;
        }

        string command = allCommends[procID][commendID];
        string[] commandSplit = command.Split('_');
        string commandCondition = commandSplit[commandSplit.Length - 1];
        commandSplit = commandSplit.Take(commandSplit.Length - 1).ToArray();
        command = String.Join("_", commandSplit);

        if(commandCondition != "All" && commandCondition != currentCondition)
        {
            ExecuteCommend(procID, commendID + 1);
            return;
        }

        SetActiveIcon(procID, commendID);

        switch (command)
        {
            case "Go":
                GoForward(procID, commendID);
                break;
            case "Left":
                TurnLeft(procID, commendID);
                break;
            case "Right":
                TurnRight(procID, commendID);
                break;
            case "Kick":
                Kick(procID, commendID);
                break;
            case "Teleport":
                Teleport(procID, commendID);
                break;
            default:
                if (command.StartsWith("UseProc"))
                {
                    var newProcID = int.Parse(command.Split('_')[1]);
                    beforeProcCommend[newProcID] = commendID;
                    beforeProcProc[newProcID] = procID;
                    ExecuteCommend(newProcID, 0);
                }

                break;
        }
    }
    public void StartCommends()
    {   
        GetCommandsFromLists();
        MapCommandsToSlots();
        ExecuteCommend(0, 0);
    }
}
