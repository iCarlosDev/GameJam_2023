using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
   [Header("--- OTHER ---")]
   [Space(10)]
   [SerializeField] private GameObject sword;
   [SerializeField] private TrailRenderer trail;
   
   [Header("--- SCRIPTS ---")]
   [Space(10)]
   [SerializeField] private PlayerController _playerController;
   [SerializeField] private PlayerHealth _playerHealth;
   [SerializeField] private Weapon playerWeapon;
   
   //GETTERS && SETTERS//
   public GameObject Sword => sword;
   public TrailRenderer Trail => trail;

   public PlayerController PlayerController => _playerController;
   public PlayerHealth PlayerHealth => _playerHealth;
   public Weapon PlayerWeapon => playerWeapon;

   //////////////////////////////

   private void Awake()
   {
      _playerController = GetComponent<PlayerController>();
      _playerHealth = GetComponent<PlayerHealth>();
   }
}
