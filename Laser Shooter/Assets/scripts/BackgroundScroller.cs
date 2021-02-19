using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float Backgroundscroller = 1.0f;
    Material MyMaterial;
    Vector2 Offset;


    // Start is called before the first frame update
    void Start()
    {
        MyMaterial = GetComponent<Renderer>().material;
        Offset = new Vector2(0, Backgroundscroller);
    }

    // Update is called once per frame
    void Update()
    {
        MyMaterial.mainTextureOffset += Offset * Time.deltaTime;
    }
}
