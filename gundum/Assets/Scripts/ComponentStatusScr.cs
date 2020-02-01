using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentStatusScr : MonoBehaviour
{
    public static ComponentStatusScr COMPONENT_STATUS;
    EnemyHealthScr EHS;
    MechAudioScr MAS;
    ShakeCamera SHAKE;
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
    [SerializeField]
    List<GameObject> SysFailWarnings;

    // Start is called before the first frame update
    void Start()
    {
        //Camera.main
        COMPONENT_STATUS = this;
        EHS = GetComponent<EnemyHealthScr>();
        MAS = GetComponent<MechAudioScr>();
        SHAKE = Camera.main?.GetComponent<ShakeCamera>();
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
        //TODO MAKE A CHECK TO SEE IF THE TARGET IS STILL ALIVE

        if (compSliders[ranTarget].value > 0)
        {
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
        else
        {
            RollDice();
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
            SHAKE?.AddTrauma(.5f, .3f);
            //med damng
        }
        if (DMG_severity >= 24 && DMG_severity < 44)
        {
            DMG = MinDMG;
            MAS.DamageSound(0);
            SHAKE?.AddTrauma(.2f, .1f);
            //minDMG
        }
        if(DMG_severity>=44)
        {
            MAS.DamageSound(2);
            DMG = MaxDMG;
            SHAKE?.AddTrauma(2f, .8f);
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
        checkSystemFailure(Tar);
        StartCoroutine(waitToMakeDamageNumberDisapear(3, Tar));


    }

    void checkSystemFailure(int sliderIndex)
    {
        if (compSliders[sliderIndex].value <= 0)
        {
            MAS.PlayWarningSound(1);
            SysFailWarnings[sliderIndex].SetActive(true);
            StartCoroutine(waitToMakeGameobject(5, SysFailWarnings[sliderIndex]));
        }
        //if core is critical
        if (compSliders[0].value < 40)
        {
            MAS.PlayWarningSound(0);
            StartCoroutine(waitToMakeGameobject(5, SysFailWarnings[3]));
        }
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
    IEnumerator waitToMakeGameobject(float waitTime, GameObject warningLable)
    {
        yield return new WaitForSeconds(waitTime);
        warningLable.SetActive(false);
    }
}
