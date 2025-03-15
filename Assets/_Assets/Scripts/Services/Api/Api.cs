using System;
using System.Collections.Generic;
using System.Text;
using _Assets.Scripts.Gameplay;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

namespace _Assets.Scripts.Services.Api
{
    public class Api
    {
        public async UniTask<TResponse> SendPostRequest<TRequest, TResponse>(string url, TRequest request,
            Dictionary<string, string> body, string authHeaderName = "Authorization")
        {
            bool isRequestExist = typeof(TRequest) != typeof(Unit);

            string json = isRequestExist
                ? JsonConvert.SerializeObject(request)
                : string.Empty;


            UnityWebRequest webRequest = body == null
                ? UnityWebRequest.PostWwwForm(url, json)
                : UnityWebRequest.Post(url, body);

            if (isRequestExist)
            {
                webRequest.SetRequestHeader("Content-Type", "application/json");

                webRequest.SetRequestHeader(authHeaderName,
                    "Bearer kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP");

                webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            }


            var result = await webRequest.SendWebRequest().ToUniTask();

            if (!string.IsNullOrEmpty(result.error))
            {
                Debug.LogError($"POST REQUEST {url} ERROR: {result.error}  MESSAGE: {result.downloadHandler.text}");
                return default(TResponse);
            }

            string responseJson = result.downloadHandler.text;
            Debug.Log(responseJson);

            return JsonConvert.DeserializeObject<TResponse>(responseJson);
        }

        public async UniTask SendInventory(ulong id, Inventory inventory)
        {
            var saveData = new InventorySaveData
            {
                id = id,
                items = inventory.GetItems()
            };

            var response =
                await SendPostRequest<InventorySaveData, InventoryResponse>(
                    "https://wadahub.manerai.com/api/inventory/status", saveData, null);
            Debug.Log(
                $"Response: {response.response}; Status: {response.status}; Data Id: {response.data_submitted.id}; Data Items Count: {response.data_submitted.items.Count}");
        }

        [Serializable]
        public class EmptyBody
        {
        }

        [Serializable]
        public class InventoryResponse
        {
            public string response;
            public string status;
            public InventorySaveData data_submitted;
        }
    }
}