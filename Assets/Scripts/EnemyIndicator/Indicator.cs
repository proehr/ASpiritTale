/* Source: https://www.youtube.com/watch?v=BC3AKOQUx04 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Register", 0.0f);
    }

    void Register()
    {
        EnemyIndicatorSystem.createIndicator(this.transform);

        //Destroy(this.gameObject, destroyTimer);
    }

}
