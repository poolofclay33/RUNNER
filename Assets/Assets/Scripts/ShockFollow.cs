using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockFollow : MonoBehaviour {

    public Transform track;
    private Transform cachedTransform;
    private Vector3 cachedPosition;

    void Start()
    {
        cachedTransform = GetComponent<Transform>();
        if (track)
        {
            cachedPosition = track.position;
        }
    }

    void Update()
    {
        if (track && cachedPosition != track.position)
        {
            cachedPosition = track.position;
            transform.position = cachedPosition;
        }
    }
}
