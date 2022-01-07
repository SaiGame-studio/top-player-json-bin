using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace SaiHttpRequest
{
    public abstract class ApiCall
    {
        [Header("Api Call")]
        public bool isDebug = false;
        public string jsonStringRequest;
        public string jsonStringResponse;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="jsonString"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public virtual IEnumerator JsonGet(string uri, string jsonString = "{}", Action<UnityWebRequest, string> callback = null)
        {
            yield return RequestJson("GET", uri, jsonString, callback);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="jsonString"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public virtual IEnumerator JsonPost(string uri, string jsonString = "{}", Action<UnityWebRequest, string> callback = null)
        {
            yield return RequestJson("POST", uri, jsonString, callback);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="jsonString"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public virtual IEnumerator JsonPut(string uri, string jsonString = "{}", Action<UnityWebRequest, string> callback = null)
        {
            yield return RequestJson("PUT", uri, jsonString, callback);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="jsonString"></param>
        /// <param name="method"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public virtual IEnumerator RequestJson(string method, string uri, string jsonString, Action<UnityWebRequest, string> callback = null)
        {
            this.jsonStringRequest = jsonString;

            string url = this.Url(uri);

            if (this.IsDebug())
            {
                Debug.Log(method + ": " + url);
                Debug.Log("jsonString Request: " + jsonString);
                Debug.Log("Loading:...");
            }

            UnityWebRequest request = new UnityWebRequest(url, method);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            this.SetHeaders(request);

            yield return request.SendWebRequest();
            this.jsonStringResponse = request.downloadHandler.text;

            if (callback == null)
            {
                if (request.result == UnityWebRequest.Result.Success) this.OnRequestSuccess(request, this.jsonStringResponse);
                else this.OnRequestError(request, this.jsonStringResponse);
            }
            else
            {
                callback(request, this.jsonStringResponse);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="jsonString"></param>
        public virtual void OnRequestError(UnityWebRequest request, string jsonStringResponse)
        {
            UnityWebRequest.Result re = request.result;
            Debug.Log("Result: " + re.ToString());

            long code = request.responseCode;
            Debug.Log("code: " + code.ToString());

            Debug.LogError("Json Response Error: " + jsonStringResponse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="jsonString"></param>
        public virtual void OnRequestSuccess(UnityWebRequest request, string jsonStringResponse)
        {

            UnityWebRequest.Result re = request.result;
            Debug.Log("Result: " + re.ToString());

            long code = request.responseCode;
            Debug.Log("code: " + code.ToString());

            Debug.Log("Json Response Success: " + jsonStringResponse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public virtual string Url(string uri)
        {
            return this.Protocol() + this.Domain() + uri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string Protocol()
        {
            return "https://";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool IsDebug()
        {
            return this.isDebug;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public virtual void SetHeaders(UnityWebRequest request)
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");
        }

        public abstract string Domain();
    }
}