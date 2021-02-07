/* Source: https://www.youtube.com/watch?v=BC3AKOQUx04 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicatorSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyIndicator indicator = null;
    [SerializeField] private RectTransform holder = null;
    [SerializeField] private Transform player = null; // used the CenterEyeAnchor

    private Dictionary<Transform, EnemyIndicator> indicators = new Dictionary<Transform, EnemyIndicator>();

    #region Delegates
    public static Action<Transform> createIndicator = delegate { };
    #endregion

    private void OnEnable()
    {
        createIndicator += Create;
    }

    private void OnDisable()
    {
        createIndicator -= Create;
    } 

    void Create(Transform enemy)
    {
        if (indicators.ContainsKey(enemy))
        {
            indicators[enemy].RestartTimer();
            return;
        }

        EnemyIndicator newIndicator = Instantiate(indicator, holder, false);
        newIndicator.Register(enemy, player, new Action(() => { indicators.Remove(enemy); }));

        indicators.Add(enemy, newIndicator);
    }
}