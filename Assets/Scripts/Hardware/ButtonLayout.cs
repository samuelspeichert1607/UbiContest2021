
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonLayout", menuName = "ControllersInput/ButtonLayout")]
public class ButtonLayout : ScriptableObject
{
    public string actionUp;
    public string actionDown;
    public string actionLeft;
    public string actionRight;
    public string triggerLeft;
    public string triggerRight;
    public string bumperLeft;
    public string bumperRight;
    public string actionStickL;
    public string actionStickR;
    public string start;
    public string select;
}
