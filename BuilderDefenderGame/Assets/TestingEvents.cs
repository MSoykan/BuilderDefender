using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestingEvents : MonoBehaviour {
    public event EventHandler<OnSpacePressedEventArgs> OnSpacePressed;

    public class OnSpacePressedEventArgs : EventArgs {
        public int spaceCount;
    }


    public delegate void TestEventDelegate(float f);
    public event TestEventDelegate OnFloatEvent;

    public event Action<bool, int> OnActionEvent;

    public UnityEvent OnUnityEvent;

    private int spaceCount;
    private void Start() {
        OnSpacePressed += Testing_OnspacePressed;
    }

    private void Testing_OnspacePressed(object sender, EventArgs e) {
        Debug.Log("Space!");
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Space pressed
            spaceCount++;
            OnSpacePressed?.Invoke(this, new OnSpacePressedEventArgs { spaceCount = spaceCount });

            OnFloatEvent?.Invoke(5.5f);

            OnActionEvent?.Invoke(true, 56);

            OnUnityEvent?.Invoke();


        }
    }
}
