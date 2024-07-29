using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
[SerializeField] float scrollSpeed = 1f;

void Update()
{
    transform.position += new Vector3(scrollSpeed * Time.deltaTime, 0, 0);
}
}