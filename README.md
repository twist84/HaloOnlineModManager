# HaloOnlineModManager
[/, -, --]help

Displays this help


[/, -, --]backup

Backs up the selected maps folder

Example: HaloOnlineModManager.exe [-b, -backup] 0.4.11.2 [-f] [-b]

This will copy the .dats from maps\0.4.11.2 to _dewbackup\maps\0.4.11.2


[/, -, --]restore

Restores the selected maps folder

Example: HaloOnlineModManager.exe [-r, -restore] 0.4.11.2 [-f] [-b]

This will copy the .dats from maps\0.4.11.2 to _dewbackup\maps\0.4.11.2


[/, -, --]patch

Patches the .dat files in the selected maps folderExample:

HaloOnlineModManager.exe [-p, -patch] 0.4.11.2 mods\packs *HangemHigh* [-b]

This will patch the .dats in maps\0.4.11.2 with the xdelta patch files in mods\packs\0.4.11.2\Maps\HangemHigh


[/, -, --]zip

Displays the contents of a zip file

Example: HaloOnlineModManager.exe [-z, -zip] mods\packs *Lockout*1.1*

This will display the contents of mods\packs\0.4.11.2\LockoutV1.1.zip

[/, -, --]download

Downloads a zip to the selected folder

Example: HaloOnlineModManager.exe [-d, -download] 0.4.11.2 http://AWebsiteLikeDropbox.com/MyAmazeBallsMod.zip MyAmazeBallsMod.zip [-b]

This will download MyAmazeBallsMod.zip mods\packs\0.4.11.2\MyAmazeBallsMod.zip

Example: HaloOnlineModManager.exe [-d, -download] 0.4.11.2 https://mega.nz/#!49kjTKDT!8xfkZ-bFHYVkBiTcwTXgrp9HwYZq1p_3-ZD21r0uIIW H2ABR_V1.3.zip [-b]

This will download MyAmazeBallsMod.zip mods\packs\0.4.11.2\H2ABR_V1.3.zip


[/, -, --]available

Displays the contents of a zip file

Example: HaloOnlineModManager.exe [-a, -available]

This will display a list of mods for the current version of mtndew.dll
