using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/CreatureData")]
public class FishSO : ScriptableObject
{
    public int Id;
    public string CreatureName;
    public int Value;
    public Sprite Icon;

    public float FishMoveSpeed;
    public float FishFastMoveSpeed;
}
