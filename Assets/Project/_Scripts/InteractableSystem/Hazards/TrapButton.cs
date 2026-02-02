using UnityEngine;
using UnityEngine.Events;

public class TrapButton : InteractableBase
{
    public UnityEvent Event;
    
    public override void BeginInteraction()
    {
        Event?.Invoke();
        Destroy(gameObject);
    }

    public override void EndInteraction()
    {
    }

    
}
