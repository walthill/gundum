using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthScr : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    Text damageRecieved, part, total;

    // Start is called before the first frame update
    void Start()
    {
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
        healthBar.value -= damage;
        part.text = healthBar.value.ToString();
        damageRecieved.text = "-" + damage;
        StartCoroutine(waitToMakeDMGTextDisapear(4));
    }

    IEnumerator waitToMakeDMGTextDisapear(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        damageRecieved.text = "";
    }


}
