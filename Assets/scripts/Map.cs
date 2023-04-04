using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<GameObject> roomPrefabs; //������ ������� ������
    public int numberOfRooms; //���������� ������ �� �����
    public float roomWidth = 50; //������ ������ �������
    public float roomHeight = 50; //������ ������ �������
    public bool showGizmos; //����������� �����


    private List<GameObject> spawnedRooms = new List<GameObject>(); // ������ ��������� ������
    private void Start()
    {
        GenerMap();
    }

    private void GenerMap()
    {
        for (int i = 0; i < numberOfRooms; i++)
        {
            
            //�������� ��������� ����� �������
            int randomIndex = Random.Range(0, roomPrefabs.Count);
            GameObject newRoom = Instantiate(roomPrefabs[randomIndex]);
            float roomPositionX =i * roomWidth;
            float roomPositionY = 0f /*i* roomHeight*/;
            newRoom.transform.position = new Vector2(roomPositionX, roomPositionY); //��������� ������� �� �����
            spawnedRooms.Add(newRoom); //��������� ��������� ������� � ������
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;

        for (int i = 0; i < spawnedRooms.Count - 1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(spawnedRooms[i].transform.position, spawnedRooms[i + 1].transform.position);
        }
    }
}
/*for (int i = 1; i < spawnedRooms.Count; i++)
{
    //�������� ��������� ����� �� ���������� ������� � �������
    Vector2 prevRoomEnd = spawnedRooms[i - 1].GetComponentInChildren<Room>().GetRandomEndPoint();
    Vector2 currentRoomStart = spawnedRooms[i].GetComponentInChildren<Room>().GetRandomStartPoint();

    //������� �������
    GameObject tunnel = new GameObject("Tunnel");
    LineRenderer lineRenderer = tunnel.AddComponent<LineRenderer>();
    lineRenderer.startWidth = 0.1f;
    lineRenderer.endWidth = 0.1f;
    lineRenderer.material.color = Color.gray;
    lineRenderer.SetPositions(new Vector3[] { prevRoomEnd, currentRoomStart });
}*/
