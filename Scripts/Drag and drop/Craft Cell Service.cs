using Zenject;

public class CraftCellService: ServiceBase
{
    private CraftService _craftService;

    [Inject]
    public void Construct(CraftService craftCellService) {
        _craftService = craftCellService;
    }
    public override void Initialize()
    {
        for (int i = 0; i < 9; i++)
        {
            _cellData.Add(null);
        }
    }
    public void ExtractForCraft()
    {
        for (int j = 0; j < _cellData.Count; j++)
        {
            var stack = _cellData[j];
            if (stack == null) continue;

            if (stack.Count > 1)
            {
                _cellData[j] = stack.RemoveCount(1);
            }
            else
            {
                _cellData[j] = null;
            }

            OnChanged(j, _cellData[j]);
        }
    }
}
