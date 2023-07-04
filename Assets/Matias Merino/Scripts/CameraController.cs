using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.position;
        transform.position = new Vector3(0, player.position.y+10, -1);
    }
}
