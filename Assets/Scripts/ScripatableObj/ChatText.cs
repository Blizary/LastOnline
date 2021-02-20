using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewText", menuName = "ScriptableObj/ Text", order = 2)]
public class ChatText : ScriptableObject
{
    public string characterName;//character that is saying this text
    public List<string>chatText;// text
    public float timer;//time between this text and the previous one
}
