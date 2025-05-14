using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArcadeStateListener 
{
    void OnArcadePaused();
    void OnArcadeContinued();
    void OnArcadeRestart();
}
