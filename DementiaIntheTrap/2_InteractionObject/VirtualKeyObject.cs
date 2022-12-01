using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class VirtualKeyObject : BaseInteractionObject //Key Input
{
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private string inputKeyValue = string.Empty;

    public void InsertInputField()
    {
        inputField.text += inputKeyValue;
    }
    public void ClearInputField()
    {
        inputField.Select();
        inputField.text = "";   
    }
    public void BackSpace()
    {
        int stringLength = inputField.text.Length;
        if(stringLength <= 1)
            inputField.text = "";
        else
            inputField.text = inputField.text.Substring(0,inputField.text.Length-1);

    }
}
