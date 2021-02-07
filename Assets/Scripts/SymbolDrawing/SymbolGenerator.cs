using System.Collections.Generic;
using UnityEngine;

namespace SymbolDrawing
{
    public class SymbolGenerator : MonoBehaviour
    {
        [SerializeField] private List<Symbol> easySymbolList = null;
        [SerializeField] private List<Symbol> mediumSymbolList = null;
        [SerializeField] private List<Symbol> hardSymbolList = null;
        [SerializeField] private bool godMode = false;

        public void GenerateSymbol(Transform enemyTransform)
        {
            Symbol symbolPrefab;
            int random = Random.Range(0, easySymbolList.Count + mediumSymbolList.Count + hardSymbolList.Count);
            if (random < easySymbolList.Count)
            {
                symbolPrefab = easySymbolList[random];
            }
            else if (random - easySymbolList.Count < mediumSymbolList.Count)
            {
                symbolPrefab = mediumSymbolList[random - easySymbolList.Count];
            }
            else
            {
                symbolPrefab = hardSymbolList[random - mediumSymbolList.Count - easySymbolList.Count];
            }

            Symbol symbol = Instantiate(symbolPrefab, enemyTransform, false);
            float zCoord = enemyTransform.GetChild(0).localPosition.z;
            symbol.transform.localPosition = new Vector3(0, 0.25f, zCoord - 0.23f);
            if (godMode)
            {
                symbol.allSymbolPointsHit = true;
            }
        }
    }
}