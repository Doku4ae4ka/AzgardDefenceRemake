using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Scripts.Managers.ProjectSettings.Loaders
{
    public class ResourceLoader : ILoader
    {
        /// <summary>
        /// Загружает ресурс по пути.
        /// </summary>
        public T ResourceLoad<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public async void ResourceLoadAsync<T>(string path, Action<T> onSuccess, Action<string> onFalse) where T : Object
        {
            ResourceRequest request = Resources.LoadAsync<GameObject>(path);
            var task = WaitForLoad<T>(request);
            await task;
            if (task.Result) onSuccess?.Invoke(task.Result);
            else onFalse?.Invoke("Error");
        }

        public async Task<T> ResourceLoadAsync<T>(string path) where T : Object
        {
            ResourceRequest request = Resources.LoadAsync<GameObject>(path);
            var task = WaitForLoad<T>(request);
            await task;
            return task.Result;
        }
        
        private async Task<T> WaitForLoad<T>(ResourceRequest request) where T : Object
        {
            var tcs = new TaskCompletionSource<T>();

            while (!request.isDone)
            {
                await Task.Delay(10);
            }
            if (request.asset != null)
            {
                tcs.SetResult((T)request.asset);
            }
            else
            {
                tcs.SetResult(null);
            }

            return await tcs.Task;
        }
    }
}