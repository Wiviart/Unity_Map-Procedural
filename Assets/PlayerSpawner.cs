using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Vector3 pos = playerSpawnPoint.position;
        var player = Instantiate(playerPrefab, pos, Quaternion.identity);
    }
}