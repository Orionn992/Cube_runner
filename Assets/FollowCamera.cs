using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;


    void Update()
    {
        transform.position = player.position + offset;
    }
}
