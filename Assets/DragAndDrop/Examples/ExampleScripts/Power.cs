using System;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Power", menuName = "Power")]
public abstract class Power : ScriptableObject {

    [SerializeField] private float duration = 5; // HARDCODE
    [SerializeField] protected GameObject prefab;
    
    protected static Action onSpellFinished;

    public Sprite icon;
    public Color color = Color.white;

    // in a properly fleshed out class, you could use the powers stats to produce something meaningful here
    public string GetDescription()
    {
        return "A power called " + name;
    }
    
    public void Cast()
    {
        Debug.Log("cast");
        PlayerCast();
    }

    private async void PlayerCast()
    {
        await this.Cast(PlayerController.Instance.transform);
    }
        
    public abstract Task Cast(Transform caster);
    

    protected async Task WaitDuration()
    {
        await Task.Delay(Mathf.RoundToInt(duration * 1000f));
    }
}
