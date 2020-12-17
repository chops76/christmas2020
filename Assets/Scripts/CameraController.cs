using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerControl player;
    private Vector3 lastPlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        lastPlayerPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToMove = player.transform.position.x - lastPlayerPosition.x;
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y,
            transform.position.z);

        lastPlayerPosition = player.transform.position;
    }
}
