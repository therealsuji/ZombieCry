using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    new Camera camera;
    public GameObject healthBar;
    Vector3 offset;

    Image healthImage;
    WalkerZombieController healthVal;

    void Start()
    {
        camera = GameManager.instance.camera.GetComponent<Camera>();
        healthVal = GetComponent<WalkerZombieController>();
        healthBar = Instantiate(healthBar);
        healthImage = healthBar.transform.GetChild(1).GetComponent<Image>();
        offset = new Vector3(0, 2, 0);
    }

    void Update()
    {
        if (healthImage.fillAmount != 0)
        {
            healthBar.transform.position = this.transform.position + offset;

            Vector3 lookAt = camera.transform.position - transform.position;
            lookAt.x = lookAt.z = 0;
            healthBar.transform.LookAt(camera.transform.position - lookAt);
            healthBar.transform.Rotate(0, 180, 0);
            healthImage.fillAmount = healthVal.health / 100f;
            if (healthImage.fillAmount == 0)
            {
                Destroy(healthBar.gameObject, 0);
            }
        }


    }



}