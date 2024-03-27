using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AI
{
    static SiegeDecision siegeDesicion;

    static AssaultDecision assaultDecision;

    static AssaultSystem assaultSystem;

    public static void OttomanDecision()
    {
        if (Random.value > VariableSingleton.GetFloatVariable("outer_wall"))
        {

        }
    }
}
