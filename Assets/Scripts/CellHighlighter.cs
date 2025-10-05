using UnityEngine;
using UnityEngine.EventSystems;

namespace TicTacToe
{
    /// <summary>
    /// Simple hover effect for a button representing a tic‑tac‑toe cell.
    /// When the pointer enters the button, it scales up slightly to provide
    /// tactile feedback. The scale resets when the pointer exits.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class CellHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Tooltip("How much to scale the cell when hovered (e.g. 1.1 = 10% larger).")]
        public float hoverScale = 1.1f;

        private Vector3 _originalScale;
        private bool _isInitialized;

        private void Awake()
        {
            // Cache the starting scale
            _originalScale = transform.localScale;
            _isInitialized = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isInitialized) return;
            transform.localScale = _originalScale * hoverScale;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isInitialized) return;
            transform.localScale = _originalScale;
        }
    }
}
