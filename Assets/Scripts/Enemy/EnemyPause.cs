using UnityEngine;
using Services.Pause;

public class EnemyPause : CarPause
{
    [SerializeField] private CarDriverAI carDriverAI;
    [SerializeField] private MainEnemy mainEnemy;

    protected override void PauseOffExtend()
    {
        carDriverAI.enabled = true;
        mainEnemy.enabled = true;
    }

    protected override void PauseOnExtend()
    {
        carDriverAI.enabled = false;
        mainEnemy.enabled = false;
    }

    protected override void StartExtend()
    {
        
    }
}
