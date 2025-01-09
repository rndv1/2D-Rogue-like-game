using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour

{
    // Start is called before the first frame update

    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask WhatIsSolid;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, WhatIsSolid);


    }
}
