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
        Wall
    }

    [System.Serializable]
    public struct rowData
    {
        public State[] row;
    }

    public rowData[] rows = new rowData[7];
}

