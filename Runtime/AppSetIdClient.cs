using System;
using System.Threading.Tasks;
using UnityEngine;

namespace PrimePeaks.AppSet {

	public class AppSetIdClient : IDisposable {

		private readonly AndroidJavaObject client;

		public AppSetIdClient() {
			var ctx = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			var appSetClass = new AndroidJavaClass("com.google.android.gms.appset.AppSet");
			client = appSetClass.CallStatic<AndroidJavaObject>("getClient", ctx);
		}

		public Task<AppSetIdInfo> GetAppSetIdInfoAsync() {
			var tsc = new TaskCompletionSource<AppSetIdInfo>();

			try {
				var appSetIdInfoTask = client.Call<AndroidJavaObject>("getAppSetIdInfo");
				appSetIdInfoTask.Call<AndroidJavaObject>("addOnSuccessListener", new OnSuccessListener(tsc));
				appSetIdInfoTask.Call<AndroidJavaObject>("addOnFailureListener", new OnFailureListener(tsc));
			} catch (Exception e) {
				tsc.SetException(e);
			}

			return tsc.Task;
		}

		public void Dispose() {
			client?.Dispose();
		}

	}

	internal class OnSuccessListener : AndroidJavaProxy {

		private readonly TaskCompletionSource<AppSetIdInfo> tsc;

		internal OnSuccessListener(TaskCompletionSource<AppSetIdInfo> tsc) : base("com.google.android.gms.tasks.OnSuccessListener") {
			this.tsc = tsc;
		}

		public void onSuccess(AndroidJavaObject result) {
			tsc.SetResult(new AppSetIdInfo {
				Id = result.Call<string>("getId"),
				Scope = (AppSetIdScope) result.Call<int>("getScope")
			});
		}

	}

	internal class OnFailureListener : AndroidJavaProxy {

		private readonly TaskCompletionSource<AppSetIdInfo> tsc;

		internal OnFailureListener(TaskCompletionSource<AppSetIdInfo> tsc) : base("com.google.android.gms.tasks.OnFailureListener") {
			this.tsc = tsc;
		}

		public void onFailure(AndroidJavaObject exception) {
			tsc.SetException(new Exception(exception.Call<string>("getMessage")));
		}

	}

}
