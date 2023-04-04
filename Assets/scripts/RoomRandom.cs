using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    public GameObject map;
    public GameObject[] rooms;
    public float roomSize;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeRoom();
        }
    }

    private void ChangeRoom()
    {
        List<Transform> exits = GetExits();
        if (exits.Count > 0)
        {
            Transform chosenExit = exits[Random.Range(0, exits.Count)];
            Vector2 newRoomPosition = chosenExit.position / roomSize;
            Instantiate(ChooseRandomRoom(), newRoomPosition * roomSize, Quaternion.identity, map.transform);
        }
    }

    private GameObject ChooseRandomRoom()
    {
        return rooms[Random.Range(0, rooms.Length)];
    }

    private List<Transform> GetExits()
    {
        List<Transform> exits = new List<Transform>();

        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("Exit"))
            {
                exits.Add(child);
            }
        }

        return exits;
    }
}
