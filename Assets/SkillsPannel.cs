using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsPannel : MonoBehaviour
{
    private CanvasGroup[] _raws;
    
     // Start is called before the first frame update
    void Start()
    {
        _raws = GetComponentsInChildren<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        bool noPoints = PlayerController.Instance.PlayerStats.Points == 0;
        if (noPoints != _raws[0].interactable) return;
        
        foreach (var cg in _raws)
        {
            cg.alpha = noPoints ? 0.5f : 1f;
            cg.interactable = !noPoints;
        }
    }
}
