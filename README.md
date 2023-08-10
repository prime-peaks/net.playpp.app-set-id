# Play App Set ID SDK Wrapper for Unity

This wrapper is for the [Play App Set ID SDK](https://developer.android.com/training/articles/app-set-id#java). It provides a C# API for accessing app set ID from a Unity app running on Android.

## Usage

Add this repository URL to the Unity Package Manager.

```csharp
var appSetIdClient = new AppSetIdClient();
var appSetIdInfoTask = appSetIdClient.GetAppSetIdInfoAsync();
await appSetIdInfoTask.ContinueWith(task => {
    if (!task.IsCompletedSuccessfully) return;
    var appSetIdInfo = task.Result;
    Debug.Log($"App set ID: {appSetIdInfo.Id}");
    Debug.Log($"App set ID scope: {appSetIdInfo.Scope}");
});
```

## How to Modify This Package

These instructions are for macOS. Additional research is required for other platforms.

### Steps

1. Check out the package from the remote repository. For convenience, use the package name as the folder name.
2. Open Terminal.
3. Change the current working directory to the `Packages` folder in the Unity project where you've used this package.
```
cd /path/to/UnityProject/Packages
```
4. Create a symbolic link to the package folder.
```
ln -s /path/to/net.playpp.app-set-id .
```
5. Open the Unity project to allow Unity to recognize the changes.
6. Modify the package and test it in the Unity project.
7. Commit and push the changes to the remote repository.
8. Remove the symbolic link.
```
rm net.playpp.app-set-id
```
9. Open the Unity project again so Unity can recognize the changes.
10. Commit the updated `packages-lock.json` in the Unity project.
