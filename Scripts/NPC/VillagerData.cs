using _Project.Scripts.NPC;

public class VillagerData {

    private JobEnum _job;
    private NpcStatus _status;
    private ItemData _workTool;
    private bool _hasUnlocked = false;
    
    public JobEnum GetJob => _job;
    public bool HasUnlocked => _hasUnlocked;
    public VillagerData(JobEnum job,NpcStatus status, ItemData tool) {
        _job = job;
        _status = status;
        _workTool = tool;
    }
    public void ChangeTool(ItemData tool) {
        _workTool = tool;
    }
    public void ChangeStatus(NpcStatus status) {
        _status = status;
    }
    public void ChangeJob(JobEnum job) {
        _job = job;
    }
}