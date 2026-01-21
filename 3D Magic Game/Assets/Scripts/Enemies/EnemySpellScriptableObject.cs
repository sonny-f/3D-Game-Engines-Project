using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Spell", menuName = "EnemySpells")]
public class EnemySpellScriptableObject : ScriptableObject
{
    public float Damage = 25f;
    public float Lifetime = 2f;
    public float Speed = 10f;
    public float SpellRadius = 0.1f;
}
