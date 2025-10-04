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

        private Vector3 originalScale;
        private bool isInitialized;

        private void Awake()
        {
            // Cache the starting scale
            originalScale = transform.localScale;
            isInitialized = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isInitialized) return;
            transform.localScale = originalScale * hoverScale;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isInitialized) return;
            transform.localScale = originalScale;
        }
    }
}
