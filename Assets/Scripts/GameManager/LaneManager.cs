using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public List<LaneManager> LaneList;
    public Vector3 LaneStart;
    public Vector3 LaneEnd;

    public float LaneLength;
    public float LaneWidth;

    public Vector2 Dir;
    public int LaneCount;


    void Start()
    {

    }

    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Vector3 horizontalDir = new(Dir.y, 0f, Dir.x);
        Vector3 dir = new(Dir.x, 0, Dir.y);

        for (int i = 0; i < LaneCount; i++)
        {
            var startPoint = LaneStart + i * LaneWidth * horizontalDir;
            Gizmos.DrawLine(startPoint, startPoint + dir * LaneLength);
            Gizmos.DrawCube(startPoint, Vector3.one);
        }
    }
}

public class Lane
{
    Vector3 position;

}
