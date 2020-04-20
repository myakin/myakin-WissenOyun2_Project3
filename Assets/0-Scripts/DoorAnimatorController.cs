﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimatorController : MonoBehaviour {
    public Animator animator;

    public void OpenDoor() {
        animator.SetTrigger("Open");
    }
    public void CloseDoor() {
        animator.SetTrigger("Close");
    }

    
}
