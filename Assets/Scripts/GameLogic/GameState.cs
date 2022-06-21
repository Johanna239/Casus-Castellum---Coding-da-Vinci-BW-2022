using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState 
{
    private static bool inMuseum;
    private static List<string> fulfilledRequirements = new List<string>(); 

    public static bool InMuseum { get => inMuseum; set => inMuseum = value; }
    public static List<string> FulfilledRequirements { get => fulfilledRequirements; set => fulfilledRequirements = value; }

    public static void fulfilledRequirement(string requirement) {
        fulfilledRequirements.Add(requirement);
    }
}
