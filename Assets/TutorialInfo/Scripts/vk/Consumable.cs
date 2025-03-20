using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "new Consumable", menuName = "Items/Consumable")]

public class Comsumable : MonoBehaviour
{
    public ConsumableType type;
}

public enum ConsumableType { Medkit, Ammo}