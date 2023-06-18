using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SO/Player"))]
public class PlayerSO : ScriptableObject
{
    public float MaxHp;
    public float MaxMana;
    public float MaxHunger;
    public float MaxOxygen;
}
