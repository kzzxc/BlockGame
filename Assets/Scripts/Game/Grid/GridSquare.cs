using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Grid
{
    public class GridSquare : MonoBehaviour
    {
        [SerializeField] Image _normalImage;
        [SerializeField] private List<Sprite> _normalImages;
        

        public void SetImage(bool setFirstImage)
        {
            _normalImage.sprite = setFirstImage ? _normalImages[1] : _normalImages[0];
        }
    }
}