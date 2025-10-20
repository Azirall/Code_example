using UnityEngine;

public class Contract
{
    private ItemStack _stack;
    public Contract(ItemStack item)
    {
        _stack = item;
    }
    public ItemStack Stack => _stack;
    public Sprite Sprite => _stack.Item.Sprite; 
    public string Name => _stack.Item.Name;
    public int Count => _stack.Count;
}
