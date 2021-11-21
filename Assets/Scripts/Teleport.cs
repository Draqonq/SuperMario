using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 teleportPosition;
    public bool isKey = false;

    public Vector3 GetTeleportPosition()
    {
        return teleportPosition;
    }

    public bool GetIsKey()
    {
        return isKey;
    }

}
