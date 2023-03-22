using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;


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
    public RectTransform finishWinImage;
    public TMP_Text finishText;
    [FormerlySerializedAs("win")] public bool lose;

    private void Awake()
    {
        Instance = this;
    }

   public void Finish()
    {   if(!lose)
        {
            finishWinImage.gameObject.SetActive(true);
            finishText.gameObject.SetActive(true);
            finishWinImage.DORotate(new Vector3(0, 0, -36000), 500);
            Color color = new Color(0.9016687f, 1, 0.1556604f, 1);
            finishText.DOColor(color, 1);
        }
        else
        {
            
            finishText.gameObject.SetActive(true);
            finishWinImage.DORotate(new Vector3(0, 0, -36000), 500);
            Color color = new Color(0.9016687f, 1, 0.1556604f, 1);
            finishText.SetText("FAIL!!!");
            finishText.DOColor(Color.red, 1).SetDelay(1);
        }
    }

   public void SceneRestart()
   {
       SceneManager.LoadScene(0);
   }

}
