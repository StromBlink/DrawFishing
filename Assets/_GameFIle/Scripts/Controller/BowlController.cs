using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BowlController : MonoBehaviour
{
     public static BowlController Instance;
     
     [SerializeField] private Transform bowlLid;
     [SerializeField] private Transform bowlInside;
 
     private float goldFish_2, crapFish_2, phirinaFIsh_2, coralFish_2,money;
     public Vector3 Moneyscale;
     private bool _isUsedBowl;
     private void Awake()
     {
          Instance = this;
     }

     private void Start()
     {
          Moneyscale  = UIManager.Instance.moneyICon.localScale;
     }

     public void SpawnFish(Transform fish)
     {
         UIManager.Instance.bowlFull.fillAmount += 0.1f;
          transform.DORotate(new Vector3(300.000122f,89.9996567f,0.000297112274f), 1f).OnComplete(() =>
          {
               transform.DORotate(new Vector3(-2.41483121e-06f,89.9998322f,3.62225842e-06f), 0.5f);
          });
          Vector3 scale = fish.transform.lossyScale;
          fish.DOScale(scale*0.18f, 1f);
          fish.DOMove(bowlLid.position, 1.5f).OnComplete(() =>
          {
               fish.DOMove(transform.position, 0.5f).OnComplete((() => { BowlInsadeFishes(fish,5);}));
               RandomText();
          });
          fish.GetComponent<Rigidbody2D>().simulated = false;
          fish.GetComponent<Rigidbody2D>().drag = 10;
          fish.GetComponent<Rigidbody2D>().angularDrag = 10;
          fish.SetParent(bowlInside);
          /*Destroy(fish.GetComponent<Fish>());*/
          FishManager.Instance.FishReList();
          
     }

      public  void EnumerateFish()
     {
          if (UIManager.Instance.pencilFill.fillAmount>0.1f || _isUsedBowl) return;
          
          StartCoroutine(EnumerateFishStart());
     }
    
     IEnumerator  EnumerateFishStart()
     {
          DOTween.KillAll();
          yield return new WaitForSeconds(1f);
          Vector3 target=   Camera.main.transform.position + new Vector3(0,-1,+3);
          
          transform.DOMove(target, 1f);
          yield return new WaitForSeconds(1f);
          for (int i = 0; i < bowlInside.childCount; i++)
          {
               Transform child = bowlInside.GetChild(i);
               yield return new WaitForSeconds(0.1f);
               child.GetComponent<Fish>().EarnMoney();
          }
          _isUsedBowl = true;
          UIManager.Instance.Finish();
     }

     void BowlInsadeFishes(Transform fish,float duration)
     {
          float x = Random.Range(0,6);
          switch (x)
          {
               case 1: fish.DOMove(new Vector3(3.13199997f, 4.51300001f, 0.351999998f), duration).OnComplete((() =>
               {
                    fish.DOMove(new Vector3(3.58500004f, 3.84200001f, 0.351999998f), duration);  
               })); break;
               case 2: fish.DOMove(new Vector3(3.58500004f,3.88200001f,0.351999998f),duration).OnComplete((() =>
               {
                    fish.DOMove(new Vector3(3.13199997f, 3.84200001f, 0.351999998f), duration);  
               })); break;
               case 3: fish.DOMove(new Vector3(3.13199997f,3.87200001f,0.351999998f), duration).OnComplete((() =>
               {
                    fish.DOMove(new Vector3(3.13199997f, 4.33799982f, 0.351999998f), duration);  
               })); break;
               case 4: fish.DOMove(new Vector3(3.13199997f,4.33799982f,0.351999998f), duration).OnComplete((() =>
               {
                    fish.DOMove(new Vector3(3.58500004f, 3.84200001f, 0.351999998f), duration);
                    
               })); break;
               case 5: fish.DOMove(new Vector3(3.58500004f,3.86200001f,0.351999998f), duration).OnComplete((() =>
               {
                    fish.DOMove(new Vector3(3.13199997f, 4.51300001f, 0.351999998f), duration);
                    
               })); break;
               case 6: fish.DOMove(new Vector3(3.13199997f, 4.51300001f, 0.351999998f), duration).OnComplete((() =>
               {
                    fish.DOMove(new Vector3(3.58500004f, 3.85200001f, 0.351999998f), duration);
                    
               })); break;
               
          }
          

     }

     void RandomText()
     {
          int x = Random.Range(0, 4);
          switch (x)
          {
               case  1 :   UIManager.Instance.goldFish.text = goldFish_2.ToString();
                    goldFish_2++;
                    break;
               case 2 :  UIManager.Instance.crapFish.text = crapFish_2.ToString();
                    crapFish_2++;
                    break;
               case  3 :   UIManager.Instance.phirinaFish.text = phirinaFIsh_2.ToString();
                    phirinaFIsh_2++;
                    break;
               case 4 :  UIManager.Instance.coralFish.text = coralFish_2.ToString();
                    coralFish_2++;
                    break;
               
               
          }

     }
}
