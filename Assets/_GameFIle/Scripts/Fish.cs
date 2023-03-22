using System;
using DG.Tweening;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private ParticleSystem runBobble;
    private int ID;
    GameObject _temp;
    private Animator _animator;
    private bool _iscatch;
    public enum  FishTyp
    {   Goldfish,
        Phrina,
        Crap,
        Mercan,
        rubber
        
    }

    public FishTyp fishTyp;

    private void Awake()
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
        if (col.CompareTag("FishingNet") && (fishTyp == FishTyp.Goldfish || fishTyp == FishTyp.Crap || fishTyp == FishTyp.Mercan))
        { if(_iscatch)  return;
            EscapeAction();
            transform.SetParent(col.transform);
            BowlController.Instance.catchFishes.Add(this.transform);
            _iscatch = true;
        }
        if (col.CompareTag("FishingNet") && fishTyp==FishTyp.Phrina)
        {
            UIManager.Instance.lose = true;
            UIManager.Instance.Finish();
        }
        if (col.CompareTag("FishingNet") && fishTyp==FishTyp.rubber)
        {
            UIManager.Instance.lose = true;
            UIManager.Instance.Finish();
        }
       
    } 
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Fish") && fishTyp==FishTyp.Phrina)
        {   
            runBobble.Play();
            Transform target = col.transform;
            transform.DOLookAt(target.position, 0).OnComplete((() =>
            {   target.GetChild(3).gameObject.SetActive(true);
                /*target.GetChild(3).transform.localScale=Vector3.one*300;*/
                target.GetChild(3).SetParent(null);
                /*target.GetChild(2).transform.localScale=Vector3.one*1;*/
                target.GetChild(2).GetComponent<ParticleSystem>().Play();
                target.GetChild(2).SetParent(null);
                target.gameObject.SetActive(false);
              
            }));
            transform.DOMove(target.position, 1).SetEase(Ease.OutSine);
        }
    }

    public void EscapeAction()
    {
        /*float x = _animator.speed;
        _animator.speed = x * 3;*/
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
