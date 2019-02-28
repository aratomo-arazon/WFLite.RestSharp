/*
 * RestAsyncActivity.cs
 *
 * Copyright (c) 2019 aratomo-arazon
 *
 * This software is released under the MIT License.
 * http://opensource.org/licenses/mit-license.php
 */
 
using RestSharp;
using System;
using System.Threading;
using System.Threading.Tasks;
using WFLite.Activities;
using WFLite.Bases;
using WFLite.Interfaces;

namespace WFLite.RestSharp.Activities.Activities
{
    public class RestAsyncActivity : AsyncActivity
    {
        public IOutVariable BaseUrl
        {
            private get;
            set;
        }

        public IOutVariable<IRestRequest> Request
        {
            private get;
            set;
        }

        public IInVariable<IRestResponse> Response
        {
            private get;
            set;
        }

        public RestAsyncActivity()
        {
        }

        public RestAsyncActivity(IOutVariable baseUrl, IOutVariable<IRestRequest> request, IInVariable<IRestResponse> response)
        {
            BaseUrl = baseUrl;
            Request = request;
            Response = response;
        }

        protected sealed override async Task<bool> run(CancellationToken cancellationToken)
        {
            var client = default(RestClient);

            if (BaseUrl != null)
            {
                var baseUrl = BaseUrl.GetValueAsObject();
                if (baseUrl is Uri)
                {
                    client = new RestClient(baseUrl as Uri);
                }
                else if (baseUrl is string)
                {
                    client = new RestClient(baseUrl as string);
                }
                else
                {
                    client = new RestClient();
                }
            }
            else
            {
                client = new RestClient();
            }

            var request = Request.GetValue();

            var response = await executeTaskAsync(client, request);

            Response.SetValue(response);

            return true;
        }

        protected virtual async Task<object> executeTaskAsync(IRestClient client, IRestRequest request)
        {
            return await client.ExecuteTaskAsync(request);
        }
    }

    public class RestAsyncActivity<TData> : RestAsyncActivity
    {
        public RestAsyncActivity()
        {
        }

        public RestAsyncActivity(IOutVariable baseUrl, IOutVariable<IRestRequest> request, IInVariable<IRestResponse> response)
            : base(baseUrl, request, response)
        {
        }

        protected override async Task<object> executeTaskAsync(IRestClient client, IRestRequest request)
        {
            return await client.ExecuteTaskAsync<TData>(request);
        }
    }
}