using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayActions
{
    void FinishTurn();
    void HitBall(Vector3 _direction, float _force);
    void Undo();
    void Pause();

    bool IsOnTheGreen();
}