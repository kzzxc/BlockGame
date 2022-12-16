using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shape
{ 
    public class Shape : MonoBehaviour
    {
        [SerializeField] private GameObject squareShapeImage;

        [HideInInspector] public ShapeData CurrentShapeData;

        private List<GameObject> _currentShape = new();

        private void Start()
        {
            
        }

        private void RequestNewShape(ShapeData shapeData) => 
            CreateShape(shapeData);

        public void CreateShape(ShapeData shapeData)
        {
            CurrentShapeData = shapeData;
            var totalSquareNumber = GetNumberOfSquares(shapeData);

            while (_currentShape.Count <= totalSquareNumber)
            {
                _currentShape.Add(Instantiate(squareShapeImage, transform));                
            }

            foreach (var square in _currentShape)
            {
                square.gameObject.transform.position = Vector3.zero;
                square.gameObject.SetActive(false);
            }

            var squareRect = squareShapeImage.GetComponent<RectTransform>();
            var rect = squareRect.rect;
            var localScale = squareRect.localScale;
            
            var moveDistance = new Vector2(rect.width * localScale.x, rect.height * localScale.y);

            var currentIndexInList = 0;

            for (int row = 0; row < shapeData.rows; row++)
            {
                for (var column = 0; column < shapeData.columns; column++)
                {
                    if (shapeData.board[row].Column[column])
                    {
                        _currentShape[currentIndexInList].SetActive(true);
                        _currentShape[currentIndexInList].GetComponent<RectTransform>().localPosition =
                            new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance),
                                GetYPositionForShapeSquare(shapeData, row, moveDistance));
                        
                        currentIndexInList++;
                    }
                }
            }
        }

        private float GetXPositionForShapeSquare(ShapeData shapeData, int column, Vector2 moveDistance)
        {
            var shiftOnX = 0f;
            
            if (shapeData.columns > 1)
            {
                float startXPos;
                if (shapeData.columns % 2 != 0)
                    startXPos = (shapeData.columns / 2) * moveDistance.x * -1;
                else
                    startXPos = ((shapeData.columns / 2) - 1) * moveDistance.x * -1 - moveDistance.x / 2;
                shiftOnX = startXPos + column * moveDistance.x;
                return shiftOnX;
            }

            return shiftOnX;
        }

        private float GetYPositionForShapeSquare(ShapeData shapeData, int row, Vector2 moveDistance)
        {
            var shiftOnY = 0f;
            
            if (shapeData.rows > 1)
            {
                float startYPos;
                if (shapeData.rows % 2 != 0)
                    startYPos = (shapeData.rows / 2) * moveDistance.y;
                else
                    startYPos = ((shapeData.rows / 2) - 1) * moveDistance.y + moveDistance.y / 2;
                shiftOnY = startYPos - row * moveDistance.y;
            }
            return shiftOnY;
        }

        private int GetNumberOfSquares(ShapeData shapeData) => 
            shapeData.board.SelectMany(rowData => rowData.Column).Count(active => active);
    }
}
