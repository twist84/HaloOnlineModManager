# HaloOnlineModManager
[/, -, --]help -- Displays this help

[/, -, --]backup -- Backs up the selected maps folder
 
Example: getMods.exe [-b, -backup] 0.4.11.2 [-f]
This will copy the .dats from maps\0.4.11.2 to _dewbackup\maps\0.4.11.2


[/, -, --]restore -- Restores the selected maps folder
 
Example: getMods.exe [-r, -restore] 0.4.11.2 [-f]
This will copy the .dats from maps\0.4.11.2 to _dewbackup\maps\0.4.11.2


[/, -, --]patch -- Patches the .dat files in the selected maps folder
 
Example: getMods.exe [-p, -patch] 0.4.11.2 mods\packs *HangemHigh*
This will patch the .dats in maps\0.4.11.2 with the xdelta patch files in mods\packs\0.4.11.2\Maps\HangemHigh


[/, -, --]zip -- Displays the contents of a zip file

Example: getMods.exe [-z, -zip] mods\packs *Lockout*1.1*
This will display the contents of mods\packs\0.4.11.2\LockoutV1.1.zip


[/, -, --]download -- Downloads a zip to the selected folder

Example: getMods.exe [-d, -download] 0.4.11.2 http://AWebsiteLikeDropbox.com/MyAmazeBallsMod.zip
This will download MyAmazeBallsMod.zip mods\packs\0.4.11.2\MyAmazeBallsMod.zip
