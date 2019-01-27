using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Furniture", menuName = "Furniture", order = 1)]
public class Furniture : ScriptableObject
{
    [Tooltip("Store the furniture assets")]
    public GameObject furniture;
}
