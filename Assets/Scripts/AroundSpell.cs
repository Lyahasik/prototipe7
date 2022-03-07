using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "AroundSpell", menuName = "ScriptableObjects/AroundSpell", order = 1)]
public class AroundSpell : Power
{
    public override async Task Cast(Transform caster)
    {
        GameObject spell = Instantiate(prefab, caster);
        // Spell duration? Spell Hit Enemy?

        await WaitDuration();

        Destroy(spell);

        onSpellFinished?.Invoke();
    }
}
