using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("Fishing")]
    public Image pencilFill;
    [Header("FishCollectPanel")]
    public Image bowlFull;
    public TMP_Text goldFish;
    public TMP_Text crapFish;
    public TMP_Text phirinaFish;
    public TMP_Text coralFish;
    [Header("Money")] 
    public TMP_Text money;
    public Transform moneyICon;
    public ParticleSystem  moneyParticule;
    [Header("Finish")] 
    public GameObject finishPanel;
    public RectTransform finishImage;
    public TMP_Text finishText;
    

    private void Awake()
    {
        Instance = this;
    }

   public void Finish()
    {
        finishPanel.SetActive(true);
        finishImage.DORotate(new Vector3(0, 0, -36000), 500);
        Color color = new Color(0.9016687f, 1, 0.1556604f, 1);
        finishText.DOColor(color, 1);
    }

}
