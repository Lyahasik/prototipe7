using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSpell", menuName = "ScriptableObjects/ProjectileSpell", order = 1)]
public class ProjectileSpell : Power
{
    public override async Task Cast(Transform caster)
    {
        Vector3? target = await PlayerController.Instance.WaitForTarget();

        if (target != null)
        {
            Vector3 dir = (Vector3)target - caster.position;
            dir.y = 0;
            dir = Quaternion.Euler(0, 90, 0) * dir;

            GameObject spell = Instantiate(prefab, caster.position + Vector3.up, Quaternion.LookRotation(dir.normalized));
            
            // Spell duration? Spell Hit Enemy?

            await WaitDuration();
            
            Destroy(spell);
        }

        onSpellFinished?.Invoke();
    }
}
