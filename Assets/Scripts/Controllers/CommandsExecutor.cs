using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;
using System.Linq;

public class CommandsExecutor : MonoBehaviour
{   
    private GameObject _characterObj, _teleport;
    private GameController _gameController;
    private Text[] _commendsFromAllProc;
    private string _currentCondition;
    private Animator _animatorPossition, _animatorMove;
    private Transform _charakterTransform;
    private int[] _beforeProcCommend, _beforeProcProc;
    private string[][] _allCommends;
    private int[][] _mapSlotsToCommands;
    private Color _activeIconColor, _normalIconColor;
    private Image _previousIconImage;

    private void Start()
    {
        _animatorPossition = _characterObj.GetComponent<Animator>();
        _charakterTransform = _characterObj.transform.GetChild(0);
        _animatorMove = _charakterTransform.GetComponent<Animator>();
        _allCommends = new string[4][];
        _mapSlotsToCommands = new int[4][];
        _activeIconColor = new Color(100, 255, 0, 1);
        _normalIconColor = new Color(255, 255, 255, 1);
        _previousIconImage = null;
        _beforeProcCommend = new int[4] {0, 0, 0, 0};
        _beforeProcProc = new int[4] {0, 0, 0, 0};
    }

    private async void GoForward(int procID, int commendID)
    {
        // run animation to change character position
        _animatorPossition.SetBool("isGoingForward", true);
        // run animation to start character movement
        _animatorMove.SetBool("isGoingForward", true);
        // wait for animation
        await Task.Delay(TimeSpan.FromSeconds(1f));
        // change character position
        _characterObj.transform.Translate(0f, 0f, 2.0f);
        // stop animation to change character position
        _animatorPossition.SetBool("isGoingForward", false);
        // stop animation to start character movement
        _animatorMove.SetBool("isGoingForward", false);
        // wait for the animation to stop
        await Task.Delay(TimeSpan.FromSeconds(0.3f));
        // execute next command
        ExecuteCommend(procID, commendID + 1);
    }

    private async void TurnLeft(int procID, int commendID)
    {
        _animatorPossition.SetBool("isTurningLeft", true);
        _animatorMove.SetBool("isTurningLeft", true);
        await Task.Delay(TimeSpan.FromSeconds(1f));
        _characterObj.transform.Rotate(0f, -90f, 0f);
        _animatorPossition.SetBool("isTurningLeft", false);
        _animatorMove.SetBool("isTurningLeft", false);
        await Task.Delay(TimeSpan.FromSeconds(0.3f));
        ExecuteCommend(procID, commendID + 1);

    }

    private async void TurnRight(int procID, int commendID)
    {   
        _animatorPossition.SetBool("isTurningRight", true);
        _animatorMove.SetBool("isTurningRight", true);
        await Task.Delay(TimeSpan.FromSeconds(1f));
        _characterObj.transform.Rotate(0f, 90f, 0f);
        _animatorPossition.SetBool("isTurningRight", false);
        _animatorMove.SetBool("isTurningRight", false);
        await Task.Delay(TimeSpan.FromSeconds(0.3f));
        ExecuteCommend(procID, commendID + 1);
    }

    private async void Kick(int procID, int commendID)
    {
        _gameController.SetIsKicking(true);
        _animatorMove.SetBool("isKicking", true);
        _animatorPossition.SetBool("isKicking", false);
        await Task.Delay(TimeSpan.FromSeconds(1.5f));
        _animatorPossition.SetBool("isKicking", false);
        _animatorMove.SetBool("isKicking", false);
        await Task.Delay(TimeSpan.FromSeconds(0.3f));
        _gameController.SetIsKicking(false);
        ExecuteCommend(procID, commendID + 1);
    }

    private async void UseTeleport(int procID, int commendID)
    {   
        // crouch down
        _animatorMove.SetBool("isTeleportDown", true);
        await Task.Delay(TimeSpan.FromSeconds(1f));
        _animatorMove.SetBool("isTeleportDown", false);
        // if currently on teleport then teleport and play sound 
        if(_teleport)
        {
            _characterObj.transform.position = _teleport.GetComponent<Teleport>().SecondTeleport.transform.position;
            GameObject.Find("/Audio/teleport").GetComponent<AudioSource>().Play();
        }
        // stand up
        _animatorMove.SetBool("isTeleportUp", true);
        await Task.Delay(TimeSpan.FromSeconds(1f));
        _animatorMove.SetBool("isTeleportUp", false);
        await Task.Delay(TimeSpan.FromSeconds(0.3f));
        ExecuteCommend(procID, commendID + 1);
    }

    private void GetCommandsFromLists()
    {
        for(int i = 0 ; i < _commendsFromAllProc.Length ; i++)
        {
            string[] procCommends = new string[]{};
            if (!(String.IsNullOrEmpty(_commendsFromAllProc[i].text)))
            {
                procCommends = _commendsFromAllProc[i].text.Remove(_commendsFromAllProc[i].text.Length - 1, 1).Split(';');
            }
            _allCommends[i] = procCommends;
        }
    }

    private void MapCommandsToSlots()
    {
        int currentCommand = 0;
        for(int procNum = 0; procNum < 4; procNum++) {
            _mapSlotsToCommands[procNum] = new int[9];
            currentCommand = 0;
            for(int slotID = 1; slotID <= 9; slotID++)
            {   
                GameObject slot = GetSlot(procNum, slotID);
                if(slot)
                {
                    if(slot.transform.childCount != 0)
                    {
                        _mapSlotsToCommands[procNum][currentCommand] = slotID;
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
        // highlights the command currently being executed
        GameObject slot = GetSlot(procID, _mapSlotsToCommands[procID][commendID]);
        GameObject icon = slot.transform.GetChild(0).gameObject;
        Image iconImage = icon.GetComponent<Image>();
        iconImage.color = _activeIconColor;
        if (_previousIconImage)
            _previousIconImage.color = _normalIconColor;
        _previousIconImage = iconImage;
    }

    private void ExecuteCommend(int procID, int commendID)
    {   
        if (_gameController.GetIsLevelEnd())
            return;
        
        // if the end of commands in a process
        if (commendID >= _allCommends[procID].Length)
        {   
            if (procID == 0)
                // if it's main then show fail
                _gameController.ShowFail();
            else
                // else execute the next command from the previous process
                ExecuteCommend(_beforeProcProc[procID], _beforeProcCommend[procID] + 1);
            return;
        }

        // get command and condition
        string command = _allCommends[procID][commendID];
        string[] commandSplit = command.Split('_');
        string commandCondition = commandSplit[commandSplit.Length - 1];
        commandSplit = commandSplit.Take(commandSplit.Length - 1).ToArray();
        command = String.Join("_", commandSplit);

        // if the condition does not match then execute the next command
        if(commandCondition != "All" && commandCondition != _currentCondition)
        {
            ExecuteCommend(procID, commendID + 1);
            return;
        }

        SetActiveIcon(procID, commendID);

        // execute the command
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
                UseTeleport(procID, commendID);
                break;
            default:
                // or switch process
                if (command.StartsWith("UseProc"))
                {
                    var newProcID = int.Parse(command.Split('_')[1]);
                    _beforeProcCommend[newProcID] = commendID;
                    _beforeProcProc[newProcID] = procID;
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

    public void SetCurrentCondition(string value)
    {
        _currentCondition = value;
    }
    
    public void SetTeleport(GameObject value)
    {
        _teleport = value;
    }

    public void SetGameController(GameController value)
    {
        _gameController = value;
    }

    public void SetCommendsFromAllProc(Text[] value)
    {
        _commendsFromAllProc = value;
    }

    public void SetCharacterObj(GameObject value)
    {
        _characterObj = value;
    }
}
