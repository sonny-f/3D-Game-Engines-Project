using UnityEngine;

public class PlayerMagicSystem : MonoBehaviour
{
    [Header("Spell Casting")]
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
    }

    void CastSpell()
    {
        Instantiate(spellToCast, castPoint.position, castPoint.rotation);
    }
}
