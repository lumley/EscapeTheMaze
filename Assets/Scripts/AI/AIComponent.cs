﻿using UnityEngine;
using System.Collections;
using System;

namespace AI
{
    public class AIComponent : MonoBehaviour
    {
        public WanderingBehavior wanderingBehavior;
        public AlertBehavior alertBehavior;

        private Behavior currentBehavior;
        private State currentState = State.WANDERING;

        // Use this for initialization
        void Start()
        {
            currentBehavior = wanderingBehavior;
        }

        // Update is called once per frame
        void Update()
        {
            currentBehavior.Update();
        }
    }

    /// <summary>
    /// State in which the AI might be at the moment. Depending on the state, the corresponding controller will be called.
    /// Other possible states might be "FEAR", "ENRAGED", "BLIND"
    /// </summary>
    public enum State
    {
        WANDERING, ALERT
    }

    /// <summary>
    /// Behavior of the AI depending on its state. Why is this not an interface? Because Unity cannot expose interfaces to the editor. 
    /// </summary>
    public abstract class Behavior : MonoBehaviour
    {
        public abstract void Update();
    }

    public abstract class WanderingBehavior : Behavior
    {

    }
    
    public abstract class AlertBehavior : Behavior
    {

    }
}
