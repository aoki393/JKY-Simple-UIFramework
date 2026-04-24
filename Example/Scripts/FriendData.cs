using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FriendData", menuName = "Simple UI/Example/FriendData")]
public class FriendData : ScriptableObject
{
    public FriendDataEntry[] friends;
}
[Serializable]
public class FriendDataEntry
{
    public Sprite headImage;
    public string FriendName;
}