@echo off
C:
cd "\Program Files (x86)\Android\android-sdk\platform-tools"
adb.exe kill-server
adb.exe logcat -s Unity