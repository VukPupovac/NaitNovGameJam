using UnityEngine;

[CreateAssetMenu(fileName = "PopupDialogue", menuName = "Scriptable Object/Popup Dialogue")]
public class PopupDialogueSO : ScriptableObject
{
    [TextArea]
    public string message;

    [TextArea]
    public string response;
}
