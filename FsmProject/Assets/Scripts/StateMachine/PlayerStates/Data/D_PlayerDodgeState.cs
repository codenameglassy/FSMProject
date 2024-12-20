using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDodgeStateData", menuName = "StateData/Player/DodgeStateData")]
public class D_PlayerDodgeState : ScriptableObject
{
    public float dashForce = 10f; // Force for the dash
    public float dashDuration = 0.2f; // How long the dash lasts
   
}
