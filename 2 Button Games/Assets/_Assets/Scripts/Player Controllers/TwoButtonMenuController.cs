using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoButtonMenuController : BaseMenuController
{
    // TODO Move to two button menu controller
    [Header("_______________", order = 0)]
    [Header("Two Button Menu Controller", order = 1)]
    [Header("--------------------", order = 2)]
    public float autoAdvanceTimer = 5f;
    public float inputExtension = 1f;

    [SerializeField, ReadOnly] private float elapsedAdvanceTime = 0;
    [SerializeField, ReadOnly] private bool takeInfluence = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
