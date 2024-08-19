using PowerArgs;
using System;
using Bitrix24RestApiTools.ArgsProcessing;

namespace Bitrix24RestApiTools
{
    class Program
    {
        static void Main(string[] args)
        {
            Args.InvokeAction<ConsoleApp>(args);
        }
    }
}
