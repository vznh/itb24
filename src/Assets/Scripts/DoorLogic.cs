// DoorLogic.cs
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked = true;
    public string unlockKeyId;

    public void Unlock()
    {
        isLocked = false;
        Debug.Log("Door unlocked!");
    }
}
