using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class FishManager : MonoBehaviour
{
     public static FishManager Instance;
     public List<Transform> fishes;
     public List<Rigidbody2D> rb2D_fishes;
     public List<Fish> fish;

     private void Awake()
     {
          Instance = this;
     }

     public void Start()
     {
          FishReList();
          /*FishMoved(5, 50,5,15);*/
          StartCoroutine(Moving());
     }

     IEnumerator Moving()
     {
          FishMoved(-50, 50,-5,15);
          yield return new WaitForSeconds(2.9f);
          StartCoroutine(Moving());
     }

     public void FishReList()
     {    fish.Clear();
          fishes.Clear();
          rb2D_fishes.Clear();
          for (int i = 0; i < transform.childCount; i++)
          {
               fishes.Add(transform.GetChild(i));
          }
          foreach (var t in fishes)
          {
               fish.Add(t.GetComponent<Fish>());
               rb2D_fishes.Add(t.GetComponent<Rigidbody2D>());
          }
     }

     void FishMoved(float minX,float maxX,float minY,float maxY)
     {
          foreach (var VARIABLE in rb2D_fishes)
          {
               float x = UnityEngine.Random.Range( minX,maxX);
               float y = UnityEngine.Random.Range( minY,maxY);
               Vector2 vector2 = new Vector2(x, y);
              
               VARIABLE.transform.DOLookAt(vector2,1f).OnComplete(() =>
               {
                    VARIABLE.AddForce(vector2*2.2f,ForceMode2D.Force);
               }); /* Quaternion.LookRotation(vector2);*/
          }
     }

  public void EscapeFishes()
  {
       Vector3 fishNetCenter = Vector3.zero;

       for (int i = 0; i < rb2D_fishes.Count; i++)
       { 
            fish[i].EscapeAction();
            Vector3 director = fishNetCenter - rb2D_fishes[i].transform.position;
            director = -director.normalized;
            Vector3 targetposition = rb2D_fishes[i].transform.position+director;
          
            /*rb2D_fishes[i].DOMove(targetposition, 1f);*/
            
            rb2D_fishes[i].transform.LookAt((1) *   targetposition);
            rb2D_fishes[i].AddForce(targetposition*100f,ForceMode2D.Force);

       }
  }
}
