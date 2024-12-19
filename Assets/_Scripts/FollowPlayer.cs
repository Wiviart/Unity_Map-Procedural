using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        StartCoroutine(FindPlayer());
    }

    private IEnumerator FindPlayer()
    {
        while (!player)
        {
            player = FindObjectOfType<Player>();
            yield return null;
        }
    }


    private void Update()
    {
        if (!player) return;

        var pos = player.transform.position;
        pos.z = -10;
        transform.position = pos;
    }
}
