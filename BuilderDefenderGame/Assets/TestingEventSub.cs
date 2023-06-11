using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static TestingEvents;

public class TestingEventSub : MonoBehaviour {

    public void Start() {
        TestingEvents testingEvents = GetComponent<TestingEvents>();
        testingEvents.OnSpacePressed += TestingEvents_OnSpacePressed;
        testingEvents.OnFloatEvent += TestingEvents_OnFloatEvent;
        testingEvents.OnActionEvent += TestingEvents_OnActionEvent;
    }

    private void TestingEvents_OnActionEvent(bool arg1, int arg2) {
        Debug.Log("OnActionEvent");
    }

    private void TestingEvents_OnFloatEvent(float f) {
        Debug.Log("FLoat: "+ f);
    }

    private void TestingEvents_OnSpacePressed(object sender, OnSpacePressedEventArgs e) {
        Debug.Log("Space2! Count:"+ e.spaceCount.ToString()+ " count");
        //TestingEvents testingEvents = GetComponent<TestingEvents>();
        //testingEvents.OnSpacePressed -= TestingEvents_OnSpacePressed;
    }

    public void TestingUnityEvent() {
        Debug.Log("TestingUnityEvent");
    }
}
