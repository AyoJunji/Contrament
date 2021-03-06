using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_Controller : MonoBehaviour
{
    public float length, startpos;
    [SerializeField] GameObject cam;
    public float paralaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = (cam.transform.position.x * paralaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
} 