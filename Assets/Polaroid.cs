using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Polaroid : MonoBehaviour
{
    public Image extraBorder;
    private Animator animator;
    public string defaultState = "GameScene";

    public bool scaled = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger(defaultState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        scaled = !scaled;
        animator.SetBool("Scaled",scaled);
    }

    public void OnHighlight()
    {
        extraBorder.gameObject.SetActive(true);
    }

    public void OnUnHighlight()
    {
        extraBorder.gameObject.SetActive(false);
    }
    
}
