using UnityEngine;

public class InventoryService : ServiceBase
{
    public override void Initialize()
    {
        for (int i = 0; i < 4; i++)
        {
            _cellData.Add(null);
        }
    }
    public void AddNewItem(ItemData item) {

        for (int i = 0; i <= _cellData.Count; i++)
        {
            if (_cellData[i] == null)
            {
                Debug.Log(i);
                ItemStack newStack = new(item, 1);
                _cellData[i] = newStack;
                OnChanged(i, newStack);
                return;
            }

   
            if (_cellData[i].Item != item) continue;
            {
                if (_cellData[i].CanBeAdded(1))
                {
                    ItemStack newStack = new(item, _cellData[i].Count + 1);

                    _cellData[i] = newStack;
                    OnChanged(i, newStack);
                    return;
                }
            }
        }
    }

    public void ExtractForBuilding(ItemStack requiredItem, out ItemStack extracted)
{
    int itemRequiredCount = requiredItem.Count;
    extracted = new(requiredItem.Item, 0);

    for (int j = 0; j < _cellData.Count; j++)
    {
        if (_cellData[j] == null) continue;

        ItemStack currentStack = _cellData[j];

        if (requiredItem.Item.name == currentStack.Item.name)
        {

            if (itemRequiredCount >= 1)
            {
                int difference = currentStack.Count - itemRequiredCount;

                if (difference > 0)
                {
                    _cellData[j] = _cellData[j].RemoveCount(itemRequiredCount);
                    OnChanged(j, _cellData[j]);
                    extracted = extracted.AddCountForBuilding(itemRequiredCount);
                    itemRequiredCount = 0;
                }

                if (difference <= 0)
                {
                    _cellData[j] = null;
                    OnChanged(j, _cellData[j]);
                    extracted = extracted.AddCountForBuilding(currentStack.Count);
                    itemRequiredCount = Mathf.Abs(difference);
                }
            }

            if (itemRequiredCount == 0)
            {
                return;
            }
        }
    }
}
    public bool TryExtractForContract(ItemStack requiredItem)
    {
        int itemRequiredCount = requiredItem.Count;
        int availableCount = 0;
        for (int j = 0; j < _cellData.Count; j++)
        {
            if (_cellData[j] == null) continue;

            if (_cellData[j].Item.name == requiredItem.Item.name)
            {
                availableCount += _cellData[j].Count;
                if (availableCount >= itemRequiredCount)
                    break;
            }
        }
        
        if (availableCount < itemRequiredCount)
            return false;
        
        for (int j = 0; j < _cellData.Count && itemRequiredCount > 0; j++)
        {
            if (_cellData[j] == null) continue;

            ItemStack currentStack = _cellData[j];
            if (currentStack.Item.name != requiredItem.Item.name) continue;

            int difference = currentStack.Count - itemRequiredCount;

            if (difference > 0)
            {
                _cellData[j] = currentStack.RemoveCount(itemRequiredCount);
                OnChanged(j, _cellData[j]);
                itemRequiredCount = 0;
            }
            else
            {
                _cellData[j] = null;
                OnChanged(j, null);
                itemRequiredCount = Mathf.Abs(difference);
            }
        }

        return true;
    }
    public bool CanAccepItem(ItemData item)
    {
        ItemStack currentStack = new(item, 1);
        for (int i = 0; i < _cellData.Count; i++)
        {
            if (CanAccept(i,currentStack))
            {
                return true;
            }
        }
        return false;
    }
}
