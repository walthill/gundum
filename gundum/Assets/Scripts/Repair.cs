using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : MonoBehaviour
{
    public int componentIndex;
    bool repairButton, canRepair;
    ComponentStatusScr CSS;

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
            CSS?.repair(componentIndex, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            canRepair = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            canRepair = false;
    }
    void CheckInput()
    {
        repairButton = Input.GetButtonDown("Repair");
    }
}
