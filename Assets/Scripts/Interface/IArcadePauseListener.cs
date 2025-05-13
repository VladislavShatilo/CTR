using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArcadePauseListener 
{
    void OnArcadePaused();
    void OnArcadeContinued();
    void OnArcadeRestart();
}
