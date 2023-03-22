using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Boat : MonoBehaviour
{
    
    public float swingDuration = 1f; //salınım süresi
    public float swingAngle = 30f; //salınım açısı

    private void Start()
    {
        //dotween ile salınım hareketi animasyonunu oluşturuyoruz
        transform.DORotate(new Vector3(0f, 0f, swingAngle), swingDuration / 2f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
