using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProgress
{
    private readonly BuildData Recipe;
    private readonly Dictionary<ItemData, int> _required = new();
    private readonly Dictionary<ItemData, int> _given = new();

    public BuildingProgress(BuildData recipe)
    {
        Recipe = recipe;
        foreach (var req in Recipe.Requirements)
        {
            _required[req.Item] = req.Count;
            _given[req.Item] = 0;
        }
    }

    public List<ItemStack> GetRemaining()
    {
        var list = new List<ItemStack>();
        foreach (var kv in _required)
        {
            int need = kv.Value - _given[kv.Key];
            if (need > 0)
                list.Add(new ItemStack(kv.Key, need));
        }
        return list;
    }

    public void Contribute(ItemStack stack, out int amount)
    {
        if (!_given.ContainsKey(stack.Item)) {
            amount = 0;
            return;

        }
        int req = _required[stack.Item];
        int have = _given[stack.Item];
        _given[stack.Item] = Math.Min(req, have + Math.Max(0, stack.Count));
        amount = _given[stack.Item];
    }

    public bool CheckProgress()
    {
        foreach (var kv in _required)
            if (!_given.TryGetValue(kv.Key, out var g) || g < kv.Value) return false;
        return true;
    }
}
