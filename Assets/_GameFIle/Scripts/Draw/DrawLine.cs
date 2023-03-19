using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class DrawLine : MonoBehaviour
{
    public static DrawLine Instance;
 
    [SerializeField] Camera mainCamera;
    
    [Header("Line")]
    [SerializeField] List<Vector2> listVector2;
    [SerializeField] LineRenderer scribbleLineRenderer;
    [SerializeField] LineRenderer Deneme;
    [SerializeField] GameObject snakeHeadpostion;
    [SerializeField] float speed;
    [SerializeField] private bool Fishnet;
   
    
    Vector3 _currentMousePos;
    Vector3 _lastMousePos;
    Snake _snake;

    private void Awake()
    {
        Instance = this;
        _snake = GetComponent<Snake>();
    }

    private void Update()
    {
        DrawLineFNC();
    }

    private void FixedUpdate()
    {
     
        _snake.SnakeMove(snakeHeadpostion.transform);
    }

    public void DrawLineFNC()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
        if (Input.GetMouseButton(0))
        { 
            AddPoint();
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            
            StartCoroutine(Path());
        }
    }

    public void AddPoint()
    {   if( UIManager.Instance.pencilFill.fillAmount<0.01)return;
        _currentMousePos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane + 1f));

        float x = _currentMousePos.x;
            x =Mathf.Clamp(x,-0.1029043f,0.6231667f);
            float y = _currentMousePos.y; 
            y=Mathf.Clamp(y,1.443286f,1.772016f);
            float z = _currentMousePos.z; 
            z=Mathf.Clamp(z,-8.038218f,-7.954221f);
            _currentMousePos=new Vector3(x, y, z);
            if (_currentMousePos != _lastMousePos)
        {
            scribbleLineRenderer.positionCount++;
            int pointIndex = scribbleLineRenderer.positionCount - 1;
            scribbleLineRenderer.SetPosition(pointIndex, _currentMousePos);
            _lastMousePos = _currentMousePos;
            UIManager.Instance.pencilFill.fillAmount -= 0.01f;
        }
    }
    IEnumerator Path()
    {  
        scribbleLineRenderer.Simplify(0.002f);
        
        for (int i = 0; i <  scribbleLineRenderer.positionCount; i++)
        {
            Vector3 lineposition = scribbleLineRenderer.GetPosition(i);
            float x = lineposition.x;
            float y= lineposition.y;
            float z = lineposition.z; 
            
            _currentMousePos=new Vector3(x, y, z);
            
            float xx = (x - 0.26f)*12.5f;
            xx=Mathf.Clamp(xx, -3.5f, 4);
            Vector3 worldPosition = new Vector3(xx, (y-1.61f)*18.75f, 0);
            listVector2.Add(worldPosition);
        }
  
        for (int i = 0; i < listVector2.Count; i++)
        {
            snakeHeadpostion.transform.position = listVector2[i];
            Deneme.positionCount ++;
            Deneme.SetPosition(i,listVector2[i]);
            yield return new WaitForSeconds(speed);
        }
        scribbleLineRenderer.positionCount = 0;
        listVector2.Clear(); 
        _snake.SnakeFinish();
        FishManager.Instance.FishReList();
        FishManager.Instance.EscapeFishes();
        BowlController.Instance.EnumerateFish();
       
    }
 
}
