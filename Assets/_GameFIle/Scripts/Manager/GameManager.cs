 
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public float PencilVoule;
    public float BowlCapacity;
    public float RopeStrength;

    public float money;


    [SerializeField] private GameObject[] fishes;
    [SerializeField] private GameObject[] quests;
    private void Awake()
    {   
        Instance = this;
    }
    private void Start()
    {
        int x = Random.Range(0, 3);
        fishes[x].SetActive(true);
        quests[x].SetActive(true);
    }
}
