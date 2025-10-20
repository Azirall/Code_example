using Zenject;

public class ChestService : ServiceBase
{
    
    private ChestGirldController _chestGirldController;
    
    [Inject] public void Construct(ChestGirldController chestGirldController){
        _chestGirldController = chestGirldController;
    }
    public override void Initialize()
    {
        for (int i = 0; i < 8; i++)
        {
            _cellData.Add(null);
        }
        _chestGirldController.Init();
    }
    public void AddNewItem(ItemData item) {

        for (int i = 0; i < _cellData.Count; i++)
        {
   
            if (_cellData[i] == null)
            {
                ItemStack newStack = new(item, 1);
                _cellData[i] = newStack;
                OnChanged(i, newStack);
                return;
            }
            else if (_cellData[i].Item == item) {
                if (_cellData[i].CanBeAdded(1))
                {
                    ItemStack newStack = new(item, _cellData[i].Count + 1);

                    _cellData[i] = newStack;
                    OnChanged(i, newStack);
                    return;
                }
                else continue;
            }
            else continue;           
        }
    }
    
}
