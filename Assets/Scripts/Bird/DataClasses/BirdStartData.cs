
using UnityEngine;

public struct BirdStartData
{
    public Vector2 Position;
    public Quaternion Rotation;
    public Direction FlyDirection;

    public BirdStartData(Vector2 position, Quaternion rotation, Direction flyDirection)
    {
        Position = position;
        Rotation = rotation;
        FlyDirection = flyDirection;
    }
}
