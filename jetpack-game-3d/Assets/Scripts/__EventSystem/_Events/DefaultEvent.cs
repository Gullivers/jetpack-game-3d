using System.Collections.Generic;
using UnityEngine;

namespace GG.EventManager
{
    [CreateAssetMenu (menuName = "Game Events/DoNotUseThis")]
    public abstract class DefaultEvent<T> : ScriptableObject 
    {
        private readonly List<IEventListener<T>> eventListeners = new List<IEventListener<T>>();
        
        public void Raise(T item){
            
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventRaised(item);
            }
        }
        public void Register(IEventListener<T> listener){
            
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
            Debug.Log("Which components are listening: " + listener);
        }
        public void UnRegister(IEventListener<T> listener){

            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    }
}

