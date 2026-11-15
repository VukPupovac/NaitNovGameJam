using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PopupDialogue", menuName = "Scriptable Object/Popup Dialogue")]
public class PopupDialogueSO : ScriptableObject
{
    public List<PopupDialogue> dialogues = new();
}
