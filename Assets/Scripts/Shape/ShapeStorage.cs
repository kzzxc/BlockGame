
using System.Collections.Generic;
using UnityEngine;


namespace Shape
{
    public class ShapeStorage : MonoBehaviour
    {
        [SerializeField] private List<ShapeData> _shapeData;
        [SerializeField] private List<Shape> _shapeList;

        private void Start() => 
            CreateShape();

        private void CreateShape()
        {
            foreach (var shape in _shapeList)
            {
                var shapeIndex = Random.Range(0, _shapeData.Count);
                shape.CreateShape(_shapeData[shapeIndex]);
            }
        }
    }
}
