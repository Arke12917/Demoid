iCloud PlayerPrefs AutoSync Documentation v1.0

Description:  This plug-in ensures Unity's PlayerPrefs is sync'd to iCloud.
Copyright: Coding Jar Studios Inc.
Support:  http://forum.unity3d.com/threads/201410-AutoSync-PlayerPrefs-to-iCloud
Confidential Support: Jodon@CodingJar.com

Unity Instructions:

  There are NO code changes needed to ensure this works.  Just export your typical iOS build and everything should just work.  You will receive log statements in the debugger output to ensure everything is working.
  
  The magic to the plug-in is the PlayerPrefsAutoSync.mm file which lives in the Assets/Plugins/iOS folder.  Unity will ensure this gets included in your exported project.  When this file is compiled with your project, all of the iCloud syncing for PlayerPrefs is taken care of for you.  Full source code is included.

Xcode Instructions:

  You must ensure to have your iCloud entitlements set-up properly.  This is easily done in XCode5 by clicking on your project, then clicking "Capabilities".  Xcode will tell you if it's not setup correctly.  Make sure to enable Key-Value Store.  That's how we're achieving the synchronization.  For older versions of Xcode, check the documentation.

Troubleshooting:

  There's a couple of duh-moments I've had.  First, make sure your iOS devices are connected to the internet and that their iCloud services are enabled in the Settings on your iOS device.
  If that's all setup, just double-check the debugger output.  It will tell you what's going wrong, usually that the entitlements aren't set-up properly.
  Make sure there's at least the "PlayerPrefsAutoSync: Loaded." log is output.  Of course, all of the code is available in PlayerPrefsAutoSync.mm and you can check us out on the support forum:  http://forum.unity3d.com/threads/201410-AutoSync-PlayerPrefs-to-iCloud

Notes:

  Since you cannot control exactly when iCloud updates your local PlayerPrefs, you shouldn't rely on displaying critical information stored in PlayerPrefs upon application start.