using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float backGroundScrollSpeed = 0.5f;
    Material mymaterial;
    Vector2 Offset;
    void Start()
    {
        mymaterial = GetComponent<Renderer>().material;
        Offset = new Vector2(0f,backGroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        mymaterial.mainTextureOffset += Offset * Time.deltaTime;
    }
}
