using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPC", menuName = "ScriptableObj/ NPC", order = 3)]
public class NPC : ScriptableObject
{
    public Sprite npcImage;
    public string npcName;
    [TextArea]
    public string tooltipInfo;
}
