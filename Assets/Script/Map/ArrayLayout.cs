using UnityEngine;
using System.Collections;

[System.Serializable]
public class ArrayLayout
{
    public enum State
    {
        EmptyFloor,
        Rock,
        Spawner,
        ObjectSpawner,
        WallRight,
        WallLeft,
        WallDown,
        WallTop,
        WallLeftDown,
        WallRightDown
    }

    [System.Serializable]
    public struct rowData
    {
        public State[] row;
    }

    public rowData[] rows = new rowData[10];
}