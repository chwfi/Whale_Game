using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/CreatureData/Giant")]
public class GiantFishSO : ScriptableObject
{
    public int Id;
    public string CreatureName;
    public int Value;
    public Sprite Icon;

    public float GiantNormalMoveSpeed;
    public float GiantFastMoveSpeed;
    public float Damage;
}
