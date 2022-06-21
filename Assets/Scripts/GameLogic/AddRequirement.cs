using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRequirement : MonoBehaviour
{
    public void AddThisRequirement(string requirement) {
        GameState.fulfilledRequirement(requirement);
    }
}
