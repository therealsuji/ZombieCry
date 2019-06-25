using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    private CharacterController charachterController;
    private Animator animator;
    PlayerController playerController;
    Image playerHealthBar;

    int health = 100;
    void Start()
    {
        playerHealthBar = GameManager.instance.playerHealthBarCanvas.transform.GetChild(1).GetComponent<Image>();
        playerRigidbody = GetComponent<Rigidbody>();
        charachterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerHealthBar.fillAmount != 0)
        {
            playerHealthBar.fillAmount = health / 100f;
            if (playerHealthBar.fillAmount == 0)
            {
                GameManager.instance.playerIsDead = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "attack1")
        {
            health -= 10;
            print(health);
        }
    }
}
