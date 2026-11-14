using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ModularAssets/GameEvent")]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> _listeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        Debug.Log(listener.gameObject.name+ " Has registered on " + this.name);
        _listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        Debug.Log(listener.gameObject.name + " Has Unregistered on " + this.name);

        _listeners.Remove(listener);
    }
}