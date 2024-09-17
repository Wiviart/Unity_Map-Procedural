using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapCollider : MonoBehaviour
{
    MapTile mapObject;
    const string PLAYER_TAG = "Player";
    [SerializeField] private TextMeshProUGUI text;

    public void Init(float size)
    {
        GetComponent<BoxCollider2D>().size = Vector2.one * size;
        text.text = transform.position.x + "," + transform.position.y;
    }

    public void SetMapObject(MapTile mapObject)
    {
        this.mapObject = mapObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!mapObject)
        {
            return;
        }

        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
            mapObject.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
            mapObject.gameObject.SetActive(false);
        }
    }
}
