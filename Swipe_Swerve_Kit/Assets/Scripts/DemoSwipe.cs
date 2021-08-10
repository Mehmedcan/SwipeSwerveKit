using UnityEngine;
using Listeners;

public class DemoSwipe : MonoBehaviour
{
    [SerializeField] private SwipeListener swipeListener;


    private void OnEnable()
    {
        swipeListener.RunFunction += Swipe;
    }

    private void OnDisable()
    {
        swipeListener.RunFunction -= Swipe;
    }

    private void Swipe(SwipeType swipeType)
    {
        Debug.Log(swipeType.ToString());

        // Stop listening with        -> swipeListener.StopListening();
        // Start listening again with -> swipeListener.StartListening();
    }
}