using Unity.Cinemachine;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject DeathPosition;
    public float moveSpeed;
    private bool death = false;
    private Vector3 endPosition;
    private bool didWin=false;
    public GameObject winPosition;

    // Update is called once per frame
    void Update()
    {
        if (death)
        {
            CinemachineFollow cf = this.gameObject.GetComponent<CinemachineFollow>();
            cf.FollowOffset = Vector3.Lerp(cf.FollowOffset, endPosition, moveSpeed * Time.deltaTime);
        }

        if (didWin)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, endPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void activateDeath()
    {
        death = true;
        endPosition=DeathPosition.transform.localPosition;
    }

    public void win()
    {
        didWin = true;
        endPosition=winPosition.transform.position;
        Destroy(GetComponent<CinemachineFollow>());
        Destroy(GetComponent<CinemachineRotationComposer>());
    }
}
