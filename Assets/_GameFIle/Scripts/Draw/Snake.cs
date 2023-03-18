using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class Snake : MonoBehaviour
{ 
    [SerializeField] private float snakeSpeed;
    [SerializeField] private float snakeDistance;
    [SerializeField] private float radius;
    [SerializeField] private List<Rigidbody2D> collidersnakes=new List<Rigidbody2D>(75);
    public Transform snake;
    public List<Transform> _cylinlist;
    public Collider[] hitColliders;

    private void Start()
    {
        for (int i = 0; i < snake.childCount-1; ++i)
        {
            _cylinlist.Add(snake.GetChild(i));
            collidersnakes.Add(  _cylinlist[i].GetComponent<Rigidbody2D>());
        }
    }

    public void SnakeMove(Transform targetposition)
    {
        Follow(_cylinlist[_cylinlist.Count-1], targetposition, snakeSpeed, snakeDistance);
        for (int i = 0; i < _cylinlist.Count-1; ++i)
        { 
            Follow(_cylinlist[i], _cylinlist[i + 1], snakeSpeed, snakeDistance);
            Follow(_cylinlist[i+1], _cylinlist[i + 2], snakeSpeed, snakeDistance);
            Follow(_cylinlist[i+2], _cylinlist[i + 3], snakeSpeed, snakeDistance);
            i += 2;

        }

        for (int i =  _cylinlist.Count-1; i >0; --i)
        {
            RotationFollow(_cylinlist[i], _cylinlist[i -1]);
        }
    }

    public void SnakeFinish()
    {
        float y = snake.position.y;
        snake.DOMoveY(y+10,10f).SetEase(Ease.Linear);
    }
    
   public void SearchCircle( )
   {   string x = "FishingNet";
       foreach (var VARIABLE in _cylinlist)
       {
           VARIABLE.tag = x;
       }
       /*bool sec=false;
         string x = "FishingNet";
        for (int i = 0; i < _cylinlist.Count; ++i)
        {   
            
            hitColliders = Physics.OverlapSphere(_cylinlist[i].position, radius);
            foreach (var hitCollider in hitColliders)
            {
                if ( hitCollider.CompareTag("Player") && hitCollider.gameObject!=_cylinlist[i].gameObject )
                {sec = true;
                    foreach (var VARIABLE in collidersnakes)
                    {collidersnakes[i].tag = x;
                        print("Calisti");
                        
                    }
                   break;
                }
            }
            /*if(sec)  break;#1#
        }*/
   }
    public void Follow(Transform _Chaser_gameobject, Transform targetTransform, float speed, float targetDistance)
    {
        Vector3 displacementFromTarget = targetTransform.position - _Chaser_gameobject.transform.position;
        float distanceToTarget = displacementFromTarget.magnitude;

        if (distanceToTarget > targetDistance)
        {
            _Chaser_gameobject.transform.position = (Vector3.Lerp(_Chaser_gameobject.transform.position, targetTransform.position, Time.deltaTime * speed));
            /*_Chaser_gameobject.transform.DOMove(_Chaser_gameobject.transform.position,speed);*/
        }
        /*_Chaser_gameobject.transform.rotation = Quaternion.LookRotation(displacementFromTarget);*/
    }
    public void RotationFollow(Transform _Chaser_gameobject, Transform targetTransform )
    {
        Vector3 displacementFromTarget = targetTransform.position - _Chaser_gameobject.transform.position;
        _Chaser_gameobject.transform.rotation = Quaternion.LookRotation(displacementFromTarget);
    }
}
