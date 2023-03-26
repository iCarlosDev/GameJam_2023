using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
   public static PlayerStorage instance;
   
   [Header("--- OTHER ---")]
   [Space(10)]
   [SerializeField] private GameObject sword;
   [SerializeField] private TrailRenderer trail;
   [SerializeField] private GameObject playerCanvas;
   
   [Header("--- SCRIPTS ---")]
   [Space(10)]
   [SerializeField] private PlayerController _playerController;
   [SerializeField] private PlayerHealth _playerHealth;
   [SerializeField] private Weapon playerWeapon;
   [SerializeField] private HealthBarController healthBarController;
   
   //GETTERS && SETTERS//
   public GameObject Sword => sword;
   public TrailRenderer Trail => trail;
   public GameObject PlayerCanvas
   {
      get => playerCanvas;
      set => playerCanvas = value;
   }

   public PlayerController PlayerController => _playerController;
   public PlayerHealth PlayerHealth => _playerHealth;
   public Weapon PlayerWeapon => playerWeapon;
   public HealthBarController HealthBarController => healthBarController;

   //////////////////////////////

   private void Awake()
   {
      instance = this;
      
      _playerController = GetComponent<PlayerController>();
      _playerHealth = GetComponent<PlayerHealth>();
      healthBarController = GetComponentInChildren<HealthBarController>();
   }
}
