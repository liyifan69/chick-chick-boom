using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.enableEmission = true;
        ps.Simulate(1);
        ps.Play();
    }
    void Update()
    {

        
    }
}
