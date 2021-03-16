using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tab 
{
    public string tabName;
    public List<ChatText> tabChat;
    public int currentText;
    public List<string> displayedText;
    public ChatType type;
    public int tabNum;
    public ChatText lastMessage;
}
