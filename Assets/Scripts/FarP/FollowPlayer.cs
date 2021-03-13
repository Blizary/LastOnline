using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    private float originalY;
    // Start is called before the first frame update
    void Start()
    {
        originalY = transform.position.y;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.y = originalY;
        transform.position = pos;
    }
}
