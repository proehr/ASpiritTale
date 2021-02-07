using UnityEngine;

namespace SymbolDrawing
{
    public class SymbolPoint : MonoBehaviour
    {
        private bool wasHit = false;

        public bool WasHit
        {
            get => wasHit;
            set => wasHit = value;
        }
    }
}