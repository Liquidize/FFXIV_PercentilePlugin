# FFXIV_PercentilePlugin (beta)
Percentile Plugin for ACT to allow overlays to display FFLogs percentile data in real time.

The plugin works by using data obtained from FFLogs and calculating the current percentile in real time using your current DPS. The data used should be fairly accurate +/- 1% (give or take) of the Historical Percentile if you use the latest data. You can view the percentiles in real time via the "Percentile" column that is added to ACT or via a compatible overlay.

Percentiles displayed in a modified Kagerou overlay (the Pct column):
![Example](https://i.imgur.com/lrgGFzG.png)

**Note:** The plugin is still in development and while fully functional for displaying percentiles for known fights, future fights and some current fights may need to be adjusted.

# How-To

1. Obtain the latest version (zip file) of the plugin from the [Releases](https://github.com/Liquidize/FFXIV_PercentilePlugin/releases)
2. Extract the zip file to where your "Advanced Combat Tracker.exe" is, usually something like "C:\Program Files (x86)\Advanced Combat Tracker"
3. Run ACT as administrator (incase of permissions errors for writing config files)
4. Go to the "Plugins" tab and the "PercentilePlugin.dll" as a plugin.
5. Go to the "PercentilePlugin" plugin tab, and click update.
6. Use a compatible overlay if you want to see the information in the overlay.

**Note:** I've forked and added percentile functionality to the popular [Kagerou](https://github.com/hibiyasleep/kagerou) overlay, simply change the overlay URL to: **https://liquidize.github.io/kagerou/overlay/** and you should be able to add "Percentile" as a column in your tabs in the config. You will need to completely reconfigure your overlay though, unfortunately.

Clicking the "Update" button will download the "parsedata.bin" file that is stored in this repository. This file contains all the data needed to calculate your parses, and is updated daily.

# Compatible Overlays

1. [Kagerou pct](https://github.com/Liquidize/kagerou) - Forked by me, this is the latest version of Kagerou with the added ability to add "Percentile" as a column to your tabs. Just like Kagerou you can easily set this as your overlay by using the following url: https://liquidize.github.io/kagerou/overlay/


If you want your overlay listed here, please message me in game or on discord.

* **Character in game:** Kaliya Y'mhitra (Goblin - NA)
* **Discord:** Kaliya#0001

# Credits

* Liquidize / Kaliya Y'mhitra (Goblin - NA) - Developer/Plugin Creator
* Kaliph Soren (Goblin - NA) - Co-developer
* [Hibiyasleep](https://github.com/hibiyasleep) - Creator of the Kagerou overlay which I forked and added functionality to.
* The update functionality of the plugin was taken from Cactbot an open source Raid helper found [here](https://github.com/quisquous/cactbot).
