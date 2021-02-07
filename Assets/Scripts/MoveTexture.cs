using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * https://docs.unity3d.com/ScriptReference/Material.SetTextureOffset.html
 */
public class MoveTexture : MonoBehaviour
{

    public float scrollSpeed = 0.1f;
    private Renderer _renderer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveThisTexture = Time.time * scrollSpeed;
        _renderer.material.SetTextureOffset("_MainTex", new Vector2(moveThisTexture, moveThisTexture));
    }
}
