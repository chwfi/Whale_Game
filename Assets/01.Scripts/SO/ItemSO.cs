using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ItemData")]
public class ItemSO : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite icon;
}
