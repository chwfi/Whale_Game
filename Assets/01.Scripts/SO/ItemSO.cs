using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ItemData")]
public class ItemSO : ScriptableObject
{
    public int Id;
    public string ItemName;
    public int Value;
    public Sprite Icon;
}
