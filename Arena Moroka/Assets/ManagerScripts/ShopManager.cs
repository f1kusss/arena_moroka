using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject player;
    public GameObject hammer;
    public GameObject pistol;
    public GameObject auto;

    public void HammerUp()
    {
        GameManager gameManager = GameManager.Instance;
        if ((gameManager.roubles >= gameManager.hpUp) && (gameManager.hammerUpMax < 20))
        {
            MeleeWeaponController hmControllerComponent = player.GetComponent<MeleeWeaponController>();
            hmControllerComponent.damage = hmControllerComponent.damage + 10;
            gameManager.hammer = hmControllerComponent.damage;
            gameManager.roubles = gameManager.roubles - gameManager.hammerUp;
            gameManager.hammerUpMax = gameManager.hammerUpMax + 1;
        }
    }
    public void hpUp()
    {
        GameManager gameManager = GameManager.Instance;
        if ((gameManager.roubles >= gameManager.hpUp) && (gameManager.hpUpMax < 20))
        {
            PlayerController hpControllerComponent = player.GetComponent<PlayerController>();
            hpControllerComponent.maxHealth = hpControllerComponent.maxHealth + 10;
            gameManager.health = hpControllerComponent.maxHealth;
            gameManager.roubles = gameManager.roubles - gameManager.hpUp;

            gameManager.hpUpMax = gameManager.hpUpMax + 1;
        }
    }
    public void PistolUp()
    {
        GameManager gameManager = GameManager.Instance;
        if ((gameManager.roubles >= gameManager.pistolUp) && (gameManager.pistolUpMax < 20))
        {
            SingleGunController gunControllerComponent = pistol.GetComponent<SingleGunController>();
            gunControllerComponent.damage = gunControllerComponent.damage + 10;
            gameManager.pistol = gunControllerComponent.damage;
            gameManager.roubles = gameManager.roubles - gameManager.pistolUp;
            gameManager.pistolUpMax = gameManager.pistolUpMax + 1;
        }
    }
    public void AutoUp()
    {
        GameManager gameManager = GameManager.Instance;
        if ((gameManager.roubles >= gameManager.autoUp) && (gameManager.autoUpMax < 20))
        {
            AutoGunController autoControllerComponent = auto.GetComponent<AutoGunController>();
            autoControllerComponent.damage = autoControllerComponent.damage + 1;
            gameManager.auto = autoControllerComponent.damage;
            gameManager.roubles = gameManager.roubles - gameManager.autoUp;
            gameManager.autoUpMax = gameManager.autoUpMax + 1;
        }
    }
    public void AmmoUp()
    {
        GameManager gameManager = GameManager.Instance;
        if ((gameManager.roubles >= gameManager.ammoUp) && (gameManager.ammoUpMax < 20))
        {
            AutoGunController ammoControllerComponent = auto.GetComponent<AutoGunController>();
            ammoControllerComponent.magazineSize = ammoControllerComponent.magazineSize + 100;
            gameManager.ammo = ammoControllerComponent.magazineSize;
            gameManager.roubles = gameManager.roubles - gameManager.ammoUp;
            gameManager.ammoUpMax = gameManager.ammoUpMax + 1;
        }
    }

}
