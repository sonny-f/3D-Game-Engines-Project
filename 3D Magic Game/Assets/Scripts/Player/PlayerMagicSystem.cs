using UnityEngine;

public class PlayerMagicSystem : MonoBehaviour
{
    [Header("Spell Casting")]
    public GameObject SpellSelector;
    public GameObject Fireball;
    public GameObject WaterProj;
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpellSelector.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void CastSpell()
    {
        Instantiate(spellToCast, castPoint.position, castPoint.rotation);
    }

    public void ShootFireball()
    {
        spellToCast = Fireball.GetComponent<Spell>();
        SpellSelector.SetActive(false);
    }

    public void ShootWaterProj()
    {
        spellToCast = WaterProj.GetComponent<Spell>();
        SpellSelector.SetActive(false);
    }
}
