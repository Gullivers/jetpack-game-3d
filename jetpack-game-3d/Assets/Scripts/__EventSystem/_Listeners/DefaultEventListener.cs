using UnityEngine;
using UnityEngine.Events;

namespace GG.EventManager
{
    public abstract class DefaultEventListener<T, BE, UER > : MonoBehaviour, IEventListener<T>
    where BE : DefaultEvent<T> where UER : UnityEvent<T>
    {

        [SerializeField] protected BE GameEvent;
        [SerializeField] protected UER unityEventResponse;
        public void OnEventRaised(T item)
        {
            if(unityEventResponse == null) { Debug.LogWarning(this + " // There is no UER!!"); return;}
            unityEventResponse.Invoke(item);

            Debug.Log("This Game Event Raised : " + GameEvent.name);
            int eventCount = this.unityEventResponse.GetPersistentEventCount();
            for (int i = 0; i < eventCount; i++)
            {
                Debug.Log("On object (listener)" + this +  " || This is invoked : " + this.unityEventResponse.GetPersistentTarget(i) + " / " + this.unityEventResponse.GetPersistentMethodName(i) + " with this parameter : " + item.ToString());
            }
            
        }
        protected void OnEnable()
        {
            if(GameEvent == null) { Debug.LogWarning(this  + " // There is no GAMEEVENT!!"); return;}
            GameEvent.Register(this);
            int eventCount = this.unityEventResponse.GetPersistentEventCount();
            for (int i = 0; i < eventCount; i++)
            {
                Debug.Log("Which components are responding: " + this.unityEventResponse.GetPersistentTarget(i) + " " + this.unityEventResponse.GetPersistentMethodName(i));    
            }
            
        }

        protected void OnDisable()
        {
            if(GameEvent == null) { Debug.LogWarning(this.name + " // There is no GAMEEVENT!!"); return;}
            GameEvent.UnRegister(this);
        }
    }
}
