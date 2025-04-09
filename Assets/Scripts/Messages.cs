using System;
using UnityEngine;

public class Messages : MonoBehaviour
{
    public int messageID;

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance().showTutorialMessage(messageID);
    }
}
