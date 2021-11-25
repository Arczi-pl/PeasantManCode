using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Conditions : MonoBehaviour
{
    public Sprite CNN, CNS, CRN, CRS, CBS, CBN;
    // the names of the icons correspond to the names of the executable commands
    public Sprite Go_All, Left_All, Right_All, Kick_All, UseProc_0_All, UseProc_1_All, UseProc_2_All, UseProc_3_All, Teleport_All;
    public Sprite Go_Red, Left_Red, Right_Red, Kick_Red, UseProc_0_Red, UseProc_1_Red, UseProc_2_Red, UseProc_3_Red, Teleport_Red;
    public Sprite Go_Blue, Left_Blue, Right_Blue, Kick_Blue, UseProc_0_Blue, UseProc_1_Blue, UseProc_2_Blue, UseProc_3_Blue, Teleport_Blue;
    private string _currentCondition = "All";

    public void ChangeCondition(string cond)
    {
        string oldCondition = _currentCondition;
        _currentCondition = cond;
        ChangeCondIcon(oldCondition);
        ChangeCommandsIcons();
    }

    private void ChangeCommandIcon(GameObject command)
    {
        // change command name
        string[] nameSplit = command.name.Split('_');
        nameSplit = nameSplit.Take(nameSplit.Length - 1).ToArray();
        string name = "";
        foreach (string namePart in nameSplit)
            name += namePart + "_";
        name += _currentCondition;
        command.name = name;
        // change command icon
        command.GetComponent<Image>().sprite = (Sprite)this.GetType().GetField(name).GetValue(this);
    }

    private void ChangeCommandsIcons()
    {
        GameObject commands = GameObject.Find("/UI/PickPanel/Commands/");
        foreach (Transform command in commands.transform)
            ChangeCommandIcon(command.gameObject);
    }

    private void ChangeCondIcon(string oldCondition)
    {
        // change old condition icon
        Image oldImage = GameObject.Find("/UI/PickPanel/Conditions/" + oldCondition ).GetComponent<Image>();
        switch (oldCondition)
        {
            case "All":
                oldImage.sprite = CNN;
                break;
            case "Red":
                oldImage.sprite = CRN;
                break;
            case "Blue":
                oldImage.sprite = CBN;
                break;
        }

        // change selected condition icon
        Image newImage = GameObject.Find("/UI/PickPanel/Conditions/" + _currentCondition ).GetComponent<Image>();
        switch (_currentCondition)
        {
            case "All":
                newImage.sprite = CNS;
                break;
            case "Red":
                newImage.sprite = CRS;
                break;
            case "Blue":
                newImage.sprite = CBS;
                break;
        }
    }
}
