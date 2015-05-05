using System;

namespace JiggonDodger
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (JiggonDodger _JiggonDodger = new JiggonDodger())
            {
                _JiggonDodger.Run();
            }
        }
    }
#endif
}

