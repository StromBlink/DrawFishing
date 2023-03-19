using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class Snake : MonoBehaviour
{
    public static Snake Instance;
    
    [SerializeField] private float snakeSpeed;
    [SerializeField] private float snakeDistance;
    [SerializeField] private float radius;
    
    public Transform snake;
    public List<Transform> _cylinlist;
    public List<ParticleSystem> _particules;
   
    private PolygonCollider2D _polygonCollider;

    private void Awake()
    {
        Instance = this;
        _polygonCollider =GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        for (int i = 0; i < snake.childCount-1; ++i)
        {
            snake.GetChild(i).GetComponent<FishingCollider>().ID = i;
            _cylinlist.Add(snake.GetChild(i));
             _particules.Add(snake.GetChild(i).GetChild(0).GetChild(0).GetComponent<ParticleSystem>());
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
        snake.DOMoveY(y+10,10f).SetEase(Ease.Linear).SetDelay(3);
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
    
    
    public void CalculationPolygon(Vector2 intersectionpoint,int start, int end)
    {
        _polygonCollider.pathCount = 0;
        List<Vector2> vector2s = new List<Vector2>(_cylinlist.Count+2);
        vector2s.Add(intersectionpoint);
        
        for (int i = start; i < end; i++)
        {
            vector2s.Add(_cylinlist[i].position);
            _particules[i].Play();
        } 
        vector2s.Add(intersectionpoint);
        
        _polygonCollider.points = vector2s.ToArray();
        print("Calisti:KesisimPolygonHesaplamasi");
    }
}
