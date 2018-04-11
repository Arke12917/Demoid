/**
 * PlayerPrefsAutoSync v1.0.  Copyright: Coding Jar Studios Inc. 2013.
 * 
 * This file ensures that Unity's PlayerPrefs gets sent up to iCloud, and properly syncs from
 * iCloud back to Unity's PlayerPrefs.
 *
 * Unity Instructions:
 *   There are NO code changes needed to ensure this works.  Just export your typical iOS build and everything should just work.
 * You will receive log statements in the debugger output to ensure everything is working.
 * 
 * Xcode Instructions:
 *   You must ensure to have your iCloud entitlements set-up properly.
 * This is easily done in XCode5 by clicking on your project, then clicking "Capabilities".  Xcode will tell you if it's not setup correctly.
 * Make sure to enable Key-Value Store.  That's how we're achieving the synchronization.  For older versions of Xcode, check the documentation.
 *
 * Troubleshooting:
 *   There's a couple of duh-moments I've had.  First, make sure your iOS devices are connected to the internet and that their iCloud services are enabled in the Settings.
 * If that's all setup, just double-check the debugger output.  It will tell you what's going wrong, usually that the entitlements aren't set-up properly.
 * Make sure there's at least the "PlayerPrefsAutoSync: Loaded." log is output.  Of course, all of the code is below and you can check us out on the support forum: http://forum.unity3d.com/threads/201410-AutoSync-PlayerPrefs-to-iCloud
 *
 * Notes:
 *   Since you cannot control exactly when iCloud updates your local PlayerPrefs, you shouldn't rely on displaying
 * critical information stored in PlayerPrefs upon application start.
 */

#import <Foundation/Foundation.h>

NSTimer* syncTimer = nil;

@interface PlayerPrefsAutoSync : NSObject
@end

@implementation PlayerPrefsAutoSync

/**
 * This is called on start-up and will properly initialize the syncing of PlayerPrefs to iCloud.
 */
+ (void)load
{
    NSLog( @"PlayerPrefsAutoSync: Loaded." );
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(start:) name:UIApplicationDidFinishLaunchingNotification object:nil];
}

/**
 * This call starts the auto-syncing system.  This will get called on start-up from the load function.
 */
+(void) start:(NSNotification*)notification
{
    // Do we support iCloud?
    if(NSClassFromString(@"NSUbiquitousKeyValueStore"))
    {
        if([NSUbiquitousKeyValueStore defaultStore])
        {
            NSLog(@"PlayerPrefsAutoSync: iCloud Supported. Initializing.");
            [self startSyncing];
            
            // Request latest version of saved PlayerPrefs from iCloud
            [[NSUbiquitousKeyValueStore defaultStore] synchronize];
        }
        else
        {
            NSLog(@"PlayerPrefsAutoSync: iCloud is not enabled.");
        }
    }
    else
    {
        NSLog(@"PlayerPrefsAutoSync: iCloud is not supported on this device/OS version.");
    }
}

/**
 * Start listening for changes to PlayerPrefs or from iCloud and sync them
 */
+ (void) startSyncing
{
    [[NSNotificationCenter defaultCenter] addObserver:self
                                             selector:@selector(iCloudChanged:)
                                                 name:NSUbiquitousKeyValueStoreDidChangeExternallyNotification
                                               object:nil];

    [[NSNotificationCenter defaultCenter] addObserver:self
                                             selector:@selector(playerPrefsChanged:)
                                                 name:NSUserDefaultsDidChangeNotification                                                    object:nil];
}

/**
 * Stop synchronizing changes between PlayerPrefs and iCloud
 */
+ (void) stopSyncing
{
    [[NSNotificationCenter defaultCenter] removeObserver:self
                                                    name:NSUbiquitousKeyValueStoreDidChangeExternallyNotification
                                                  object:nil];
    
    [[NSNotificationCenter defaultCenter] removeObserver:self
                                                    name:NSUserDefaultsDidChangeNotification
                                                  object:nil];
}

/**
 * Clean ourselves up
 */
+ (void) dealloc
{
    [self stopSyncing];
}

/**
 * Actually synchronize the PlayerPrefs to iCloud
 */
+ (void) syncToCloud
{
    NSLog( @"PlayerPrefsAutoSync: Sending to iCloud" );

    // We store the PlayerPrefs in iCloud as the entry "PlayerPrefs"
    NSDictionary* playerPrefsDict = [[NSUserDefaults standardUserDefaults] dictionaryRepresentation];
    [[NSUbiquitousKeyValueStore defaultStore] setDictionary:playerPrefsDict forKey: @"PlayerPrefs"];
    [[NSUbiquitousKeyValueStore defaultStore] synchronize];
    
    syncTimer = nil;
}

/**
 * PlayerPrefs have changed, so sync to iCloud.
 * Note:  Due to how we get a notification, we get a massive amount of these
 * at once when doing PlayerPrefs.Save().  For performance reasons, we must
 * buffer the updates to iCloud and send them after all of the Unity ones are done.
 */
+ (void) playerPrefsChanged: (NSNotification*) notificationObject
{
    // Cancel a previous timer if it was set...
    if ( syncTimer != nil && [syncTimer isValid] )
    {
        [syncTimer invalidate];
    }
 
    // Create a timer to synchronize to iCloud.
    // We do this because during a PlayerPrefs.Save() we will get a TON of these notifications...
    syncTimer = [NSTimer scheduledTimerWithTimeInterval:1.0 target:self selector:@selector(syncToCloud) userInfo:nil repeats:false];
}

/**
 * iCloud has been updated, so sync to our PlayerPrefs
 */
+ (void) iCloudChanged:(NSNotification*) notificationObject
{
    NSLog(@"PlayerPrefsAutoSync: Receiving from iCloud");
    
    // Stop listening for changes since we're about to cause a lot right now...
    [self stopSyncing];

    // Clear out the PlayerPrefs since I don't want to write code to detect deleted keys...
    NSUserDefaults* playerPrefs = [NSUserDefaults standardUserDefaults];
    NSDictionary* playerPrefsDict = [playerPrefs dictionaryRepresentation];
    for (NSString *key in [playerPrefsDict allKeys]) {
        [playerPrefs removeObjectForKey:key];
    }
    
    // Now all of the keys from iCloud into PlayerPrefs
    NSDictionary *iCloudPrefs = NSUbiquitousKeyValueStore.defaultStore.dictionaryRepresentation;
    NSDictionary* playerPrefsSync = [iCloudPrefs objectForKey:@"PlayerPrefs"];
    if ( playerPrefsSync != nil )
    {
        [playerPrefsSync enumerateKeysAndObjectsUsingBlock:^(id key, id obj, BOOL *stop) {
            [playerPrefs setObject:obj forKey:key];
        }];
    }
    [playerPrefs synchronize];

    // Listen for changes again...
    [self startSyncing];
}

@end