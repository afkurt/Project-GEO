using System;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    public Action BeginInteractionCallback;
    public Action EndInteractionCallback;
    
    public abstract void BeginInteraction();
    public abstract void EndInteraction();

    public Rigidbody2D _playerRB;

    public virtual void OnEnable()
    {
        BeginInteractionCallback += BeginInteraction;
        EndInteractionCallback += EndInteraction;
    }

    public virtual void OnDisable()
    {
        BeginInteractionCallback -= BeginInteraction;
        EndInteractionCallback -= EndInteraction;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _playerRB = other.GetComponent<Rigidbody2D>();
        BeginInteractionCallback?.Invoke();
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        EndInteractionCallback?.Invoke();
    }

}
