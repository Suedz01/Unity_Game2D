using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using Random = System.Random;

public class NpCpaths : MonoBehaviour
{
    public List<Transform> paths = new List<Transform>();
    private Vector2 _dirNpc, _rotNpc;

    private float _speed;
    private float _walkSpeed = 1.75f;
    public int index = 0;
    
    public void Update()
    {
        _dirNpc = paths[index].position - transform.position;

        _rotNpc = _dirNpc.x < 0 ? new Vector2(0, 180) : new Vector2(0, 0);
        transform.eulerAngles = _rotNpc;
        
        if (_dirNpc.magnitude <= 0.2f)
        {
            index = UnityEngine.Random.Range(0,paths.Count);
        }

        _speed = DialogueControl.Instance.IsShowing ? 0f : _walkSpeed;
    }
    
    public void FixedUpdate()
    {
        transform.position = (Vector2)transform.position + Time.deltaTime * _speed * _dirNpc.normalized;
    }
}