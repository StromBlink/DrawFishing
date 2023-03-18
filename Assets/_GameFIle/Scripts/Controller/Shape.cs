using System;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public static Shape Instance;
    public PolygonCollider2D polygonCollider2D;
    public List<GameObject> insideObjects = new List<GameObject>();
    public List<GameObject> objectsToCheck = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public  void Ttart()
    {
        // Bozuk halkanın tüm noktalarını alın
        Vector2[] polygonPoints = polygonCollider2D.points;

        // Tüm objelerin konumunu kontrol edin ve içinde kalanları belirleyin
        foreach (GameObject obj in objectsToCheck)
        {
            Collider2D collider = obj.GetComponent<Collider2D>();
            if (collider == null) continue;
            print("   colliderlar bulundu");
            Vector2 objPosition = obj.transform.position;
            bool isInside = true;

            foreach (Vector2 point in polygonPoints)
            {
                Vector2 worldPoint = obj.transform.TransformPoint(point);
                if (!collider.bounds.Contains(worldPoint))
                {  print(" foreach (Vector2 point in polygonPoints)");
                    isInside = false;
                    break;
                }
            }

            if (isInside)
            {  print(" if (isInside)");
                insideObjects.Add(obj);
            }
        }

        // İçinde kalan objeleri kullanın veya listeye ekleyin
        foreach (GameObject obj in insideObjects)
        {
            print(obj.name +"  "+"yakalandi");
        }
    }
}