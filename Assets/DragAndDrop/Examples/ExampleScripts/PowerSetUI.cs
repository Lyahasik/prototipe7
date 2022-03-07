using DragAndDrop;

public class PowerSetUI : ObjectContainerArray {

    public PowerSet powerSet;

    private bool canDrag = false;

	// Use this for initialization
	void Start () {
        CreateSlots(powerSet.powers);
        
	}

    // can't change the contents of this one by dragging/dropping
    public override bool IsReadOnly()
    {
        return true;
    }

    public override bool CanDrag(Draggable dragged)
    {
        return canDrag;
    }

    public void SetCanDrag(bool drag)
    {
        canDrag = drag;
    }
}
