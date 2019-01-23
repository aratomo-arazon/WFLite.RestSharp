using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WFLite.Activities;
using WFLite.Activities.Console;
using WFLite.Interfaces;
using WFLite.RestSharp.Activities.Activities;
using WFLite.RestSharp.Variables;
using WFLite.Variables;

namespace WFLite.RestSharp.HelloWorld
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var statusCode = new AnyVariable();
            var data = new AnyVariable();

            var activity = new SequenceActivity()
            {
                Activities = new List<IActivity>()
                {
                    // start WFLite.AspNetCore.HelloWorld first.
                    new RestAsyncActivity<string>()
                    {
                        BaseUrl = new AnyVariable("http://localhost:51115/"),
                        Request = new RequestVariable()
                        {
                            Resource = new AnyVariable("api/values")
                        },
                        Response = new ResponseVariable<string>()
                        {
                            StatusCode = statusCode,
                            Data = data
                        }
                    },
                    new ConsoleWriteLineActivity()
                    {
                        Value = data
                    }
                }
            };

            await activity.Start();

            Console.ReadKey();
        }
    }
}
