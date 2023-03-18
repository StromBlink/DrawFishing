using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int fishLevel=2;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text levelText_2;
   
    [SerializeField] private ParticleSystem particleSystem_2;
    [SerializeField] private ParticleSystem runBobble;
    private int ID;
    GameObject _temp;
    private Animator _animator;

    private void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    /*private void  OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("FishingNet"))
        {  
            
            BowlController.Instance.SpawnFish(transform);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("FishingNet"))
        {  
            
            BowlController.Instance.SpawnFish(transform);
        }
    }
    public void EscapeAction()
    {
        float x = _animator.speed;
        _animator.speed = x * 3;
        runBobble.Play();
    }

    
        public  void EarnMoney()
        {      
                Transform child =transform;
                Vector3 scale= child.localScale;
                child.DORotate(new Vector3(0, 90f, 0), 0);
                child.DOMove (new Vector3(-3.2700808f,3.58365202f,0f), 0.6f);
                child.DOScale(scale*0.5f, 0.4f).OnComplete((() =>
                {
                    child.gameObject.SetActive(false);
                }));
                UIManager.Instance.moneyICon.DOPunchScale(Vector3.one * .1f, 0.6f).OnComplete((() => 
                {         
                    UIManager.Instance.moneyICon.localScale = BowlController.Instance.Moneyscale;
                    UIManager.Instance.moneyParticule.Play();
                }));
                GameManager.Instance.money++;
                UIManager.Instance.money.text = GameManager.Instance.money.ToString();
               
            
        }
    
}
