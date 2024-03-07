using UnityEngine;

public class OrderSlot : MonoBehaviour
{
    public OrderMaker.Order currentOrder;

    public void ReceiveOrder(OrderMaker.Order order)
    {
        currentOrder = order;
        // Here you could update UI elements to show the order to the player
        Debug.Log($"Received order: Flavor {order.flavor}, Color {order.color}, Glass {order.glasses}");
    }

    // This method would be called by the player's interaction script when they attempt to fulfill the order
    public bool CheckOrder(int flavor, int color, int size)
    {
        // Check if the player's creation matches the order
        return currentOrder.flavor == flavor && currentOrder.color == color && currentOrder.glasses == size;
    }
}
