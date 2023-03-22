using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MeshGenerator : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>(); // Kapalı alanın pozisyon noktaları
    public Mesh mesh;
    public Material MaterialisNet;
    public GameObject FishingNet;

 

    private void Awake()
    {
        mesh = FishingNet.GetComponent<MeshFilter>().mesh;
    } 
    public void GenerateMesh()
    {
        Vector3 centre = Vector3.zero;
         List<int> triangles = new List<int>();
         List<Vector3> vertices = new List<Vector3>();
         List<Vector2> uvs = new List<Vector2>();

           // Pozisyon noktalarını verteks listesine ekle
          foreach (Vector3 position in positions)
          {
              vertices.Add(position);
              uvs.Add(new Vector2(position.x, position.z));
              centre += position;
          }
          centre /= positions.Count;
          vertices.Add(centre);

          // Kapalı alanın kenarlarını birbirine bağlayan üçgenleri oluştur
          for (int i = 0; i < positions.Count - 3; i++)
          {
              triangles.Add(i);
              triangles.Add(positions.Count - 1);
              triangles.Add(i + 1);
          }
          for (int i = positions.Count - 3; i > 0; i--)
          {
              triangles.Add(i);
              triangles.Add(positions.Count - 1);
              triangles.Add(i - 1);
          }

          // Mesh'i ayarla
          mesh.Clear();
          mesh.vertices = vertices.ToArray();
          mesh.triangles = triangles.ToArray();
          // UV koordinatlarının boyutunu vertices dizisinin boyutuna eşitle
          if (uvs.Count != vertices.Count)
          {
              uvs.Clear();
              for (int i = 0; i < vertices.Count; i++)
              {
                  uvs.Add(new Vector2(vertices[i].x, vertices[i].z));
              }
          }

          mesh.uv = uvs.ToArray();
          mesh.RecalculateNormals();

          // Mesh bileşenine mesh'i atayarak görselleştir
          FishingNet.GetComponent<MeshFilter>().mesh = mesh;
          FishingNet.GetComponent<MeshRenderer>().material = MaterialisNet;
          FishingNet.transform.position = centre;
          FishingNet.transform.localScale = Vector3.one * 0.1f;
          FishingNet.transform.DOScale(Vector3.one, 3);

          positions.Clear();
    }
}