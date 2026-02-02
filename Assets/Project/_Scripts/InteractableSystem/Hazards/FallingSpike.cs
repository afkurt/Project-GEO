using UnityEngine;

public class FallingSpike : InteractableBase
{
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Interact()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public override void BeginInteraction()
    {
        Destroy(gameObject);
    }

    public override void EndInteraction() { }
    

    
}
