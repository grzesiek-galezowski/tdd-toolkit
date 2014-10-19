using System.Reflection;
using TddEbook.TypeReflection.Interfaces;

namespace TddEbook.TypeReflection.ImplementationDetails
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