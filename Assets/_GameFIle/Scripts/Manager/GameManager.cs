using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public float PencilVoule;
    public float BowlCapacity;
    public float RopeStrength;

    public float money;
    private void Awake()
    {
        Instance = this;
    }
}
