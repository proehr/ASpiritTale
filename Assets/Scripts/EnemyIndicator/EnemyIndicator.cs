/* Source: https://www.youtube.com/watch?v=BC3AKOQUx04 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    private const float maxTimer = 1.0f;
    private float timer = maxTimer;

    public Transform enemy { get; protected set; } = null;
    private Transform player = null;

    private IEnumerator countdownIE = null;
    private Action unregister = null;

    private Quaternion rotationEnemy = Quaternion.identity;
    private Vector3 positionEnemy = Vector3.zero;

    public void Register(Transform enemy, Transform player, Action unregister)
    {
        this.enemy = enemy;
        this.player = player;
        this.unregister = unregister;

        StartCoroutine(RotateToEnemy());
        StartTimer();
    }

    private void StartTimer()
    {
        if (countdownIE != null)
        {
            StopCoroutine(countdownIE);
        }
        countdownIE = Countdown();
        StartCoroutine(countdownIE);
    }

    public void RestartTimer()
    {
        timer = maxTimer;
        StartTimer();
    }

    private IEnumerator Countdown()
    {
        while(GetComponent<CanvasGroup>().alpha < 1.0f)
        {
            GetComponent<CanvasGroup>().alpha += 4 * Time.deltaTime;
            yield return null;
        }
        while(timer > 0)
        {
            timer--;
            yield return new WaitForSeconds(1);
        }

        while (GetComponent<CanvasGroup>().alpha > 0.0f)
        {
            GetComponent<CanvasGroup>().alpha -= 2 * Time.deltaTime;
            yield return null;
        }
        unregister();
        Destroy(gameObject);

    }

    IEnumerator RotateToEnemy()
    {
        while (enabled)
        {
            if (enemy)
            {
                positionEnemy = enemy.position;
                rotationEnemy = enemy.rotation;
            }

            Vector3 direction = player.position - positionEnemy;

            rotationEnemy = Quaternion.LookRotation(direction);
            rotationEnemy.z = -rotationEnemy.y;
            rotationEnemy.x = 0;
            rotationEnemy.y = 0;

            Vector3 northDirection = new Vector3(0, 0, player.eulerAngles.y);
            GetComponent<RectTransform>().localRotation = rotationEnemy * Quaternion.Euler(northDirection);

            yield return null;

        }
    }
}
