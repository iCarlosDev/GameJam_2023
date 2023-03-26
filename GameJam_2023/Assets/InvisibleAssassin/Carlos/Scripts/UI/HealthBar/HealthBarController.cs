using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    
    [Header("--- HEALTH BAR ---")] 
    [Space(10)] 
    [SerializeField] private Transform heartsBox;
    [SerializeField] private RectTransform habilityRecT;
    
    //GETTERS && SETTERS//
    public Transform HeartsBox => heartsBox;

    ///////////////////////////////
    
    void Start()
    {
        SetHealthBarParameters();
        SetHabilityParameters();
    }

    public void SetHealthBarParameters()
    {
        foreach (Transform hearts in heartsBox)
        {
            Destroy(hearts.gameObject);
        }
        
        for (int i = 0; i < GameManager.instance.CurrentLives; i++)
        {
            Instantiate(GameManager.instance.HeartPrefab, heartsBox);
        }
    }

    public void SetHabilityParameters()
    {
        habilityRecT.sizeDelta = new Vector2(habilityRecT.sizeDelta.x, GameManager.instance.CurrentHability * 8);
    }
}
