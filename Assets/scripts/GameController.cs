using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public List<GameObject> cardLayouts; // ������ ������� ����
    public GameObject cardPrefab; // ������ �����
    public Transform spawnPoint; // ����� ���������
    public int maxRooms = 10; // ������������ ���������� ������
    public float roomSize = 10f; // ������ ������ �������

    private List<GameObject> rooms = new List<GameObject>(); // ������ ���� ��������� ������
    private GameObject previousRoom; // ���������� �������

    void Start()
    {
        SpawnRoom();
    }

    void SpawnRoom()
    {
        int direction = Random.Range(0, 4); // ��������� �����������

        // ���������, ���� �� ������� ���������� �������
        Vector3 offset = Vector3.zero;
        if (previousRoom != null)
        {
            // ���� ��, �� ������� ����� ��������� ������������ ���������� �������
            switch (direction)
            {
                case 0: // �����
                    offset = new Vector3(-roomSize, 0, 0);
                    break;
                case 1: // ������
                    offset = new Vector3(roomSize, 0, 0);
                    break;
                case 2: // �����
                    offset = new Vector3(0, roomSize, 0);
                    break;
                case 3: // ����
                    offset = new Vector3(0, -roomSize, 0);
                    break;
            }
            spawnPoint.position = previousRoom.transform.position + offset;
        }
        
        

        

        // ���� �� �������� ������������� ���������� ������, �� ������������� ���������
        if (rooms.Count >= maxRooms)
        {
            return;
        }

        // ���������� ��������� �������
        Invoke("SpawnRoom", 2);
    }
}