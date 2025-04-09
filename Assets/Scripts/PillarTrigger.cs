using System;
using UnityEngine;

public class PillarTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        this.GetComponentInParent<FallPillar>().Fall();
    }
}
