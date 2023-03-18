using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingCollider : MonoBehaviour
{
    public int ID;
    string x = "FishingNet";

    private PolygonCollider2D _polygonCollider;

    private List<Transform> circle;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {  
          DrawLine.Instance.KesisimPolygonHesaplamasi(transform.position);
        }
    }

    public void ColliderPolygon()
    {
        Vector2[] vector2s=new Vector2[5];
        _polygonCollider.SetPath(5, vector2s);
    }
}
