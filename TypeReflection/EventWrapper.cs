using System.Reflection;

namespace TddEbook.TypeReflection
{
  public class EventWrapper : IEventWrapper
  {
    private readonly EventInfo _eventInfo;

    public EventWrapper(EventInfo eventInfo)
    {
      _eventInfo = eventInfo;
    }

    public string GenerateNonPublicExistenceMessage()
    {
      return "Type: " + _eventInfo.DeclaringType +
             " contains non public event " + _eventInfo.Name +
             " of type " + _eventInfo.EventHandlerType;

    }
  }
}