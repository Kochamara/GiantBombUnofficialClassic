﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombApi.Services
{
    public class ApiKeyRetrievalAgent
    {
        /// <summary>
        /// Step One: Get a key from http://www.giantbomb.com/app/Giant%20Bomb%20Unofficial%20Classic/
        /// Step Two: Run it through this method, which will get that account's API key
        /// </summary>
        /// <param name="linkCode"></param>
        /// <returns></returns>
        public static async Task<string> GetApiKeyFromCodeAsync(string linkCode)
        {
            string apiKey = null;

            try
            {
                var uri = new Uri("http://www.giantbomb.com/app/Giant%20Bomb%20Unofficial%20Classic/get-result?format=json&regCode=" + linkCode);
                var response = await Utilities.HttpRequestAgent.GetDeserializedResponseAsync<Models.ApiKeyResponse>(uri);
                if ((response != null) && (!String.IsNullOrWhiteSpace(response.ApiKey)))
                {
                    apiKey = response.ApiKey;
                }
                else
                {
                    // TODO: add a logger
                }
            }
            catch (Exception e)
            {
                // TODO: add a logger
            }

            return apiKey;
        }
    }
}
