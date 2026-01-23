using UnityEngine;
using UnityEngine.UI;

public class PlayerMagicSystem : MonoBehaviour
{
    [Header("Spell Casting")]
    public GameObject Fireball;
    public GameObject WaterProj;
    public GameObject Psychic;
    public GameObject Lightning;
    [Space(10)]
    public GameObject fireballBorder;
    public GameObject waterProjBorder;
    public GameObject lightningBorder;
    public GameObject psychicBorder;
    [Space(10)]
    public GameObject staffColourFire;
    public GameObject staffColourWater;
    public GameObject staffColourLightning;
    public GameObject staffColourPsychic;
    [Space(10)]
    [SerializeField] private Spell spellToCast;
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana;
    [SerializeField] private float manaRegenRate = 2f;
    [SerializeField] private float timeBetweenCasts = 0.25f;
    private float castTimer;
    private bool castingMagic = false;
    public Image manaBar;
    public AudioSource spellCastSound;

    [SerializeField] private Transform castPoint;

    private InputSystem_Actions controls;

    private void Awake()
    {
        controls = new InputSystem_Actions();

        currentMana = maxMana;

        //set base spell to fireball
        ShootFireball();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        manaBar.fillAmount = currentMana / maxMana;

        bool isSpellCastHeldDown = controls.Player.SpellCast.ReadValue<float>() > 0.1;
        bool isNotBroke = currentMana - spellToCast.spellToCast.manaCost >= 0;

        //cast spell
        if (!castingMagic && isSpellCastHeldDown && isNotBroke)
        {
            castingMagic = true;
            currentMana -= spellToCast.spellToCast.manaCost;
            castTimer = 0f;
            CastSpell();
        }

        //manage mana regen
        if (castingMagic)
        {
            castTimer += Time.deltaTime;

            if (castTimer > timeBetweenCasts)
            {
                castingMagic = false;
            }
        }


        //regenerate mana
        if (currentMana < maxMana && !castingMagic)
        {
            currentMana += manaRegenRate * Time.deltaTime;
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShootFireball();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShootWaterProj();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShootLightning();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ShootPsychic();
        }
    }

    void CastSpell()
    {
        Instantiate(spellToCast, castPoint.position, castPoint.rotation);
        spellCastSound.Play();
    }

    public void ShootFireball()
    { 
        fireballBorder.SetActive(true);
        waterProjBorder.SetActive(false);
        lightningBorder.SetActive(false);
        psychicBorder.SetActive(false);

        spellToCast = Fireball.GetComponent<Spell>();

        staffColourFire.SetActive(true);
        staffColourWater.SetActive(false);
        staffColourLightning.SetActive(false);
        staffColourPsychic.SetActive(false);
    }

    public void ShootWaterProj()
    {
        waterProjBorder.SetActive(true);
        fireballBorder.SetActive(false);
        lightningBorder.SetActive(false);
        psychicBorder.SetActive(false);

        spellToCast = WaterProj.GetComponent<Spell>();

        staffColourFire.SetActive(false);
        staffColourWater.SetActive(true);
        staffColourLightning.SetActive(false);
        staffColourPsychic.SetActive(false);
    }

    public void ShootLightning()
    {
        lightningBorder.SetActive(true);
        fireballBorder.SetActive(false);
        waterProjBorder.SetActive(false);
        psychicBorder.SetActive(false);

        spellToCast = Lightning.GetComponent<Spell>();

        staffColourFire.SetActive(false);
        staffColourWater.SetActive(false);
        staffColourLightning.SetActive(true);
        staffColourPsychic.SetActive(false);
    }

    public void ShootPsychic()
    {
        psychicBorder.SetActive(true);
        fireballBorder.SetActive(false);
        waterProjBorder.SetActive(false);
        lightningBorder.SetActive(false);

        spellToCast = Psychic.GetComponent<Spell>();

        staffColourFire.SetActive(false);
        staffColourWater.SetActive(false);
        staffColourLightning.SetActive(false);
        staffColourPsychic.SetActive(true);
    }
}
