using TMPro;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    [SerializeField] private string code;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        text.text = code;
    }

    public Direction GetDirections()
    {
        Direction direction = new()
        {
            left = int.Parse(code[0].ToString()),
            right = int.Parse(code[1].ToString()),
            up = int.Parse(code[2].ToString()),
            down = int.Parse(code[3].ToString())
        };
        return direction;
    }
}

public struct Direction
{
    public int left, right, up, down;
}
