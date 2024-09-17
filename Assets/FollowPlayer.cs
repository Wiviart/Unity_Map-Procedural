using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Player player;

    void Start()
    {
        StartCoroutine(FindPlayer());
    }

    private IEnumerator FindPlayer()
    {
        player = FindObjectOfType<Player>();

        while (player == null)
        {
            player = FindObjectOfType<Player>();
            yield return null;
        }
    }


    void Update()
    {
        if (!player) return;

        Vector3 pos = player.transform.position;
        pos.z = -10;
        transform.position = pos;
    }
}
