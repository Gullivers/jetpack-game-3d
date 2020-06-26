namespace GG.EventManager
{
    public interface IEventListener<T>
    {
        void OnEventRaised(T item);
    }    

}


