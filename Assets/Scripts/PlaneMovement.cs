using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if (player.getWin()) return;
        this.transform.position = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
    }
}
