using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    float length, startPosition;
    public GameObject cam;
    public float parralaxEffect;
    
    [SerializeField] float buffer = 10f;
    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
       
    }

    
    void Update() {
        float dist = (cam.transform.position.x * parralaxEffect);
        float temp = (cam.transform.position.x * (1 - parralaxEffect));
        transform.position = new Vector3(startPosition + dist, transform.position.y, transform.position.z);

        if (temp > startPosition + length - buffer)  startPosition += length;
        else if (temp < startPosition - length) startPosition -= length;
    }
}