using UnityEngine;
using UnityEngine.UI;

namespace Shape
{
    public class ShapeSquare : MonoBehaviour
    {
        public Image occupiedImage;

        private void Start() => 
            occupiedImage.gameObject.SetActive(false);
    }
}
