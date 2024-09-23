using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject swordCollider;

    public void EnableWeapon()
    {
        swordCollider.SetActive(true);
    }

    public void DisableWeapon()
    {
        swordCollider.SetActive(false);
    }
}
