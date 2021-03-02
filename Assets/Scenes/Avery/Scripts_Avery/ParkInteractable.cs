﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class ParkInteractable : MonoBehaviour
{
    private PlayerControls _controls;
    private Collider _collider;
    private Animator _anim;
    PuzzleUI puzzleUI;

    public GameObject behaviorToBeTriggered;
    public AudioSource audioSource;

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _anim = GameObject.FindWithTag("buttonprompt").GetComponent<Animator>();
        puzzleUI = GameObject.FindGameObjectWithTag("puzzleui").GetComponent<PuzzleUI>();
        puzzleUI.enabled = false; 
    }

    private void OnTriggerEnter(Collider _collider)
    {
        _anim.SetBool("showButton", true);
    }

    private void OnTriggerStay(Collider _collider)
    {
        if (_controls.Standard.Interact.ReadValue<float>() > .5f)
        {
            //Debug.Log("Player has interacted with " + this.name);
            puzzleUI.enabled = true;
            if (audioSource != null)
            {
                audioSource.Play();
                Debug.Log("Sound plays");
            }
            else
            {
                Debug.Log("Please insert audio source");
            }
            //do something e.g
            //behaviorToBeTriggered.GetComponent<SomeScript>.SomeFunction();
            Debug.Log(puzzleUI.counter);
            
            _anim.SetBool("showButton", false);
            puzzleUI.ShowUI();
        }
        
    }

    private void OnTriggerExit(Collider _collider)
    {
        if (_anim.GetBool("showButton"))
        {
            _anim.SetBool("showButton", false);
            audioSource.Stop();
            puzzleUI.HideUI();
        }
    }
}
