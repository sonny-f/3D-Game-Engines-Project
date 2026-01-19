using UnityEngine;

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

    [SerializeField] private Transform castPoint;

    private InputSystem_Actions controls;

    private void Awake()
    {
        controls = new InputSystem_Actions();

        currentMana = maxMana;

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
        bool isSpellCastHeldDown = controls.Player.SpellCast.ReadValue<float>() > 0.1;
        bool isNotBroke = currentMana - spellToCast.spellToCast.manaCost >= 0;
        if (!castingMagic && isSpellCastHeldDown && isNotBroke)
        {
            castingMagic = true;
            currentMana -= spellToCast.spellToCast.manaCost;
            castTimer = 0f;
            CastSpell();
        }

        if(castingMagic)
        {
            castTimer += Time.deltaTime;

            if(castTimer > timeBetweenCasts)
            {
                castingMagic = false;
            }
        }

        if(currentMana < maxMana && !castingMagic)
        {
            currentMana += manaRegenRate * Time.deltaTime;
            if(currentMana > maxMana)
            {
                currentMana = maxMana;
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShootFireball();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShootWaterProj();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShootLightning();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            ShootPsychic();
        }
    }

    void CastSpell()
    {
        Instantiate(spellToCast, castPoint.position, castPoint.rotation);
    }

    public void ShootFireball()
    {
        spellToCast = Fireball.GetComponent<Spell>();

        lightningBorder.SetActive(false);
        fireballBorder.SetActive(true);
        waterProjBorder.SetActive(false);
        psychicBorder.SetActive(false);

        staffColourFire.SetActive(true);
        staffColourWater.SetActive(false);
        staffColourLightning.SetActive(false);
        staffColourPsychic.SetActive(false);
    }

    public void ShootWaterProj()
    {
        spellToCast = WaterProj.GetComponent<Spell>();

        lightningBorder.SetActive(false);
        fireballBorder.SetActive(false);
        waterProjBorder.SetActive(true);
        psychicBorder.SetActive(false);

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

        staffColourFire.SetActive(false);
        staffColourWater.SetActive(false);
        staffColourLightning.SetActive(true);
        staffColourPsychic.SetActive(false);
    }

    public void ShootPsychic()
    {
        lightningBorder.SetActive(false);
        fireballBorder.SetActive(false);
        waterProjBorder.SetActive(false);
        psychicBorder.SetActive(true);

        staffColourFire.SetActive(false);
        staffColourWater.SetActive(false);
        staffColourLightning.SetActive(false);
        staffColourPsychic.SetActive(true);
    }
}
