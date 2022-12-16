using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ShapeData : ScriptableObject
{
    [System.Serializable]
    public class Row
    {
        public bool[] Column;
        private int _size;

        public Row(int size)
        {
            CreateRow(size);
        }

        public void CreateRow(int size)
        {
            _size = size;
            Column = new bool[_size];
            ClearRow();
        }

        public void ClearRow()
        {
            for (int i = 0; i < _size; i++)
            {
                Column[i] = false;
            }
        }
    }

    public int columns = 0;
    public int rows = 0;
    public Row[] board;

    public void Clear()
    {
        for (int i = 0; i < rows; i++)
        {
            board[i].ClearRow();
        } 
    }

    public void CreateNewBoard()
    {
        board = new Row[rows];

        for (int i = 0; i < rows; i++)
        {
            board[i] = new Row(columns);
        }
    }
}
