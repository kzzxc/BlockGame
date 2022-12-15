using System.Collections.Generic;
using UnityEngine;

namespace Game.Grid
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private int _columns = 0;
        [SerializeField] private int _rows = 0;
    
        [SerializeField] private float _squaresGap  = 0.1f;
        [SerializeField] private float _squaresScale  = 0.5f;
        [SerializeField] private float _everySquareOffset;

        [SerializeField] private GameObject _gridSquare;
        [SerializeField] private Vector2 _startPosition;

        private Vector2 _offset = new Vector2(0f, 0);
        private List<GameObject> _gridSquares = new List<GameObject>();

        private void Start()
        {
            CreateGrid();
        }

        private void CreateGrid()
        {
            SpawnGridSquares();
            SetGridSquaresPositions();
        }

        private void SpawnGridSquares()
        {
            int squareIndex = 0;
            
            for(var row = 0; row < _rows; ++row)
            {
                for (var column = 0; column < _columns; ++column)
                {
                    _gridSquares.Add(Instantiate(_gridSquare) as GameObject);
                    _gridSquares[^1].transform.SetParent(this.transform);
                    _gridSquares[^1].transform.localScale = new Vector3(_squaresScale, _squaresScale, _squaresScale);
                    _gridSquares[^1].GetComponent<GridSquare>().SetImage(squareIndex % 2 == 0);
                    squareIndex++;
                }
            }
        }

        private void SetGridSquaresPositions()
        {
            int columnNumber = 0;
            int rowNumber = 0;
            Vector2 squareGapNumber = new Vector2(0f, 0f);
            bool rowMoved = false;

            var square_rect = _gridSquares[0].GetComponent<RectTransform>();

            var localScale = square_rect.transform.localScale;
            var rect = square_rect.rect;
            
            _offset.x = rect.width * localScale.x + _everySquareOffset;
            _offset.y = rect.height * localScale.y + _everySquareOffset;

            foreach (GameObject square in _gridSquares)
            {
                if (columnNumber + 1 > _columns)
                {
                    squareGapNumber.x = 0;
                    columnNumber = 0;
                    rowNumber++;
                    rowMoved = false;
                }

                var posOffsetX = _offset.x * columnNumber + (squareGapNumber.x + _squaresGap);
                var posOffestY = _offset.y * rowNumber + (squareGapNumber.y * _squaresGap);

                if (columnNumber > 0 && columnNumber % 3 == 0)
                {
                    squareGapNumber.x++;
                    posOffsetX += _squaresGap;
                }

                if (rowNumber > 0 && rowNumber % 3 == 0 && rowMoved == false)
                {
                    rowMoved = true;
                    squareGapNumber.y++;
                    posOffestY += _squaresGap;
                }

                square.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(_startPosition.x + posOffsetX, _startPosition.y - posOffestY);
                
                square.GetComponent<RectTransform>().localPosition = new Vector3(_startPosition.x + posOffsetX,
                    _startPosition.y - posOffestY, 0f);

                columnNumber++;
            }
        }
    }
}
