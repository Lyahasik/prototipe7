using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(fileName = "AreaSpell", menuName = "ScriptableObjects/AreaSpell", order = 1)]
public class AreaSpell : Power
{
    public override async Task Cast(Transform caster)
    {
        Vector3? target = await PlayerController.Instance.WaitForTarget();

        if (target != null)
        {
            GameObject spell = Instantiate(prefab, (Vector3)target + prefab.transform.position, Quaternion.identity);
            // Spell duration? Spell Hit Enemy?

            await WaitDuration();
            
            Destroy(spell);
        }
        
        onSpellFinished?.Invoke();
    }
}
