namespace TddEbook.TddToolkitSpecification.Fixtures
{
  public interface MyService
  {
    void VoidCall(int i);
    void VoidCallNotExited(int i);
    void VoidCallNotExitedOnException(int i);
    void VoidCallNotEntered(int i);
    int CallWithResult(string alabama);
    int CallWithResultNotEntered(string alabama);
    int CallWithResultNotExited(string alabama);
    int CallWithResultNotExitedOnException(string alabama);
  }
}