using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : MonoBehaviour
{
    [SerializeField]
    GameObject buttonPompt;
    public int componentIndex;
    bool repairButton, canRepair;
    ComponentStatusScr CSS;

    GameObject player;
    bool isMissileComputer;

    void Awake()
    {
        if (componentIndex == 2)
            isMissileComputer = true;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        CSS = ComponentStatusScr.COMPONENT_STATUS;
        canRepair = false;
    }

    void Update()
    {
        CheckInput();
        RepairMachine();
    }

    void RepairMachine()
    {
        if (canRepair && repairButton)
        {
            player.GetComponent<PlayerStats>().PickUpScrap(ComponentStatusScr.COMPONENT_STATUS.repair(componentIndex, player.GetComponent<PlayerStats>().SpendScrap(1)));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isMissileComputer)
            {
                player.GetComponent<PlayerStats>().LoadMissle();
                ComponentStatusScr.COMPONENT_STATUS.LoadMissile();
            }

            canRepair = true;
            buttonPompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canRepair = false;
            buttonPompt.SetActive(false);
        }
            
    }
    void CheckInput()
    {
        repairButton = Input.GetButtonDown("Repair"); //A button
    }
}
