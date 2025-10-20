using System;
using UnityEngine;
public interface ICraftResultSlots
{
    void TryTakeAll(out ItemStack result);        

    event Action<ItemStack> Changed;
}