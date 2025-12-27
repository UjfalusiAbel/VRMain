using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VRMain.Assets.Code.UI.Utility
{
    public class ScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private float _scale;

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale *= _scale;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale /= _scale;
        }
    }
}