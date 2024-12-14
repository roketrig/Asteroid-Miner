using ArcadeBridge.ArcadeIdleEngine.Helpers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Popup_UI : MonoBehaviour
{
    [SerializeField] GameObject game;
    [SerializeField] Canvas canvas;
    [SerializeField] LookAtConstraint LookAtConstraint;

    void Awake()
    {
        ConstraintSource source = new ConstraintSource();
        source.sourceTransform = Camera.main.transform;
        source.weight = 1f;
        LookAtConstraint.AddSource(source);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LookAtConstraint.enabled = true;
            TweenHelper.ShowSlowly(canvas.transform, Vector3.one, 0.5f, null);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LookAtConstraint.enabled = false;
            TweenHelper.DisappearSlowlyAndDeactivate(canvas.transform);
        }
    }
}
