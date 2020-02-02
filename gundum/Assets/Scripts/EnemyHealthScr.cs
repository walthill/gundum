using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealthScr : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    Text damageRecieved, part, total;

    ShakeCamera shake;

    // Start is called before the first frame update
    void Start()
    {
        shake = Camera.main.GetComponent<ShakeCamera>();
        total.text = healthBar.value.ToString();
        part.text= healthBar.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            int ran = Random.Range(0, 100);
            RecieveDMG(ran);
        }
    }

    public void RecieveDMG(float damage)
    {
        if (healthBar.value > 0)
        {
            healthBar.value -= damage;
            part.text = healthBar.value.ToString();
            damageRecieved.text = "-" + damage;
            StartCoroutine(waitToMakeDMGTextDisapear(4));
        }

        if(healthBar.value <= 0)
        {
            //shake the fucking camera
            shake.AddTrauma(3, 1);
            ComponentStatusScr.COMPONENT_STATUS.winSound();
            //make win courroutine
            StartCoroutine(waitToEnd(3));
        }
            
    }

    IEnumerator waitToMakeDMGTextDisapear(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        damageRecieved.text = "";
    }

    IEnumerator waitToEnd(float waitTime)
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(waitTime);
        //damageRecieved.text = "";
        SceneManager.LoadScene("win");
    }

}
