using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentStatusScr : MonoBehaviour
{
    public static ComponentStatusScr COMPONENT_STATUS;
    EnemyHealthScr EHS;
    MechAudioScr MAS;
    [SerializeField]
    List<Slider> compSliders;
    //Slider core, shield, weaponSys;

    [SerializeField]
    float timeBetweenDiceRolls, timeBetweenDecay;
    float countDown;
    [SerializeField]
    int DecayAmount, MinDMG, MidDMG, MaxDMG, ShieldMod;
    [SerializeField]
    List<Text> CompStatusValue;
    [SerializeField]
    Text CountDownText;
    [SerializeField]
    List<Text> DamageIndicator, repairIndicatorText;

    // Start is called before the first frame update
    void Start()
    {
        COMPONENT_STATUS = this;
        EHS = GetComponent<EnemyHealthScr>();
        MAS = GetComponent<MechAudioScr>();
        //StartCoroutine(damageWait(timeBetweenDiceRolls));
        countDown = timeBetweenDiceRolls;
        //CountDownText
        for(int i=0; i < compSliders.Count; i++)
        {
            CompStatusValue[i].text =compSliders[i].value.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();
        }
        TimerDown();
    }

    void TimerDown()
    {
        countDown -= Time.deltaTime;
        CountDownText.text = "" + (Mathf.Round(countDown * 100)) / 100;
        if (countDown < 0)
        {
            countDown = timeBetweenDiceRolls;
            RollDice();
        }
    }


    void RollDice()
    {
        int ranTarget = Random.Range(0, compSliders.Count);//getting the max is just a miss
        int ranDmg = Random.Range(0, 49);
        if (ranTarget != compSliders.Count)
        {
            DoDMG(ranTarget, ranDmg);
            Debug.Log("there was damage");
        }
        else
        {
            Debug.Log("no damage");
        }


    }

    //this deals damage to friendlyMech
    void DoDMG(int Tar, int DMG_severity)
    {
        int DMG=0;//idk why I called it this
        Debug.Log("sev is: " + DMG_severity);
        if (DMG_severity < 24)
        {
            DMG = MidDMG;
            MAS.DamageSound(1);
            //med damng
        }
        if (DMG_severity >= 24 && DMG_severity < 44)
        {
            DMG = MinDMG;
            MAS.DamageSound(0);
            //minDMG
        }
        if(DMG_severity>=44)
        {
            MAS.DamageSound(2);
            DMG = MaxDMG;
            //maxDMg
        }

        //if you still have shields
        if (compSliders[1].value > 0)
        {
            DMG -= ShieldMod;
            if (DMG < 0)
            {
                DMG = 0;
            }
        }

        //actually deal the damage
        compSliders[Tar].value -= DMG;
        Debug.Log("dmg was:" + DMG);
        CompStatusValue[Tar].text = compSliders[Tar].value.ToString();
        //make damage text appear
        DamageIndicator[Tar].text="-"+DMG;
        StartCoroutine(waitToMakeDamageNumberDisapear(3, Tar));

    }



    public void repair(int SliderIndex,float repairAmount)
    {
        //increase slider val
        compSliders[SliderIndex].value += repairAmount;
        //display repair text
        repairIndicatorText[SliderIndex].text = "+" + repairAmount;
        //disapearText
        StartCoroutine(waitToMakeRepairNumberDisapear(.1f, SliderIndex));
    }

    //starts decay timer
    IEnumerator DecayWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

    }
    //starts timer between next hit
    IEnumerator damageWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        RollDice();
        StartCoroutine(damageWait(timeBetweenDiceRolls));
    }
    IEnumerator waitToMakeDamageNumberDisapear(float waitTime, int textIndex)
    {
        yield return new WaitForSeconds(waitTime);
        DamageIndicator[textIndex].text = "";
    }
    IEnumerator waitToMakeRepairNumberDisapear(float waitTime, int textIndex)
    {
        yield return new WaitForSeconds(waitTime);
        repairIndicatorText[textIndex].text = "";
    }
}
