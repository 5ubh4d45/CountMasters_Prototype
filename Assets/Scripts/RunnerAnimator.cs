using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RunnerAnimator : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private String runCycleOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat(runCycleOffset, Random.Range(0.0f, 1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
