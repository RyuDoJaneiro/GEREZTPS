using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitteable
{
    public void ReceiveDamage(int damageAmount);
}
