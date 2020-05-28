using System;
namespace fondomerende.Main.Utilities
{
    public interface HapticFeedbackGen
    {
           void HapticFeedbackGenSuccessAsync();

           void HapticFeedbackGenErrorAsync();
    }
}
