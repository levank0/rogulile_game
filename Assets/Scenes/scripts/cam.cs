using UnityEngine;

public class cam : MonoBehaviour
{
    private GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y, -10f), Time.deltaTime * 2f);
    }
}
