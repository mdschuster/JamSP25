using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public GameObject MovePosition;
    public float moveSpeed;
    private Vector3 endPosition;
    private Vector3 startPosition;


    private void Start()
    {
        endPosition = MovePosition.transform.position;
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = this.transform.position;
        position = Vector3.Lerp(startPosition, endPosition, moveSpeed * Time.deltaTime);
        this.transform.position = position;
    }
}
