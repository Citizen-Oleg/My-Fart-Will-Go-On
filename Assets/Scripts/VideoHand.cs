using System;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class VideoHand : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _one;
        [SerializeField]
        private RectTransform _two;

        private bool _isPressQ;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _one.gameObject.SetActive(true);
                    _isPressQ = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _one.gameObject.SetActive(false);
                _isPressQ = false;
            }

            if (_isPressQ)
            {
                _one.position = Input.mousePosition;
                _two.position = Input.mousePosition;

                if (Input.GetMouseButtonDown(0))
                {
                    _one.gameObject.SetActive(false);
                    _two.gameObject.SetActive(true);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    _one.gameObject.SetActive(true);
                    _two.gameObject.SetActive(false);
                }
            }
        }
    }
}