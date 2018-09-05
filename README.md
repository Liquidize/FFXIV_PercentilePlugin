# FFXIV_PercentilePlugin
Percentile Plugin for ACT to allow overlays to display FFLogs percentile data in real time.

The plugin works by using data obtained from FFLogs and calculating the current percentile in real time using your current DPS. The data used should be fairly accurate +/- 1% of the Historical Percentile if you use the latest data. You can view the percentiles in real time via the "Percentile" column that is added to ACT or via a compatible overlay.

# How-To

1. Obtain the latest version (zip file) of the plugin from the [Releases](https://github.com/Liquidize/FFXIV_PercentilePlugin/releases)
2. Extract the zip file to where your "Advanced Combat Tracker.exe" is, usually something like "C:\Program Files (x86)\Advanced Combat Tracker"
3. Run ACT as administrator (incase of permissions errors for writing config files)
4. Go to the "Plugins" tab and the "PercentilePlugin.dll" as a plugin.
5. Go to the "PercentilePlugin" plugin tab, and with "Use Local Data" **unchecked** click update.
6. Use a compatible overlay if you want to see the information in the overlay.

**Note:** I've forked and added percentile functionality to the popular [Kagerou](https://github.com/hibiyasleep/kagerou) overlay, simply change the overlay URL to: **https://liquidize.github.io/kagerou/overlay/** and you should be able to add "Percentile" as a column in your tabs in the config. You will need to completely reconfigure your overlay though, unfortunately.


Clicking on update with "UseLocal" unchecked will download the "remotedata.json" file found on this repository. This is the easiest way to obtain the latest data for FFLogs inforamtion. This data is required to calculate the percentiles in real time. If you want to use "Local Data" see the section below on using local data. (NOT recommended FOR SLOW CONNECTIONS OR METERED CONNECTIONS)

# Local Data

Local Data is data that is not obtained via the remotedata.json file. It is obtained on your own by setting the plugin up to parse the FFLogs data via the FFLogs API. To do this you need an FFLogs API Key which can be obtained if you have an FFLogs account. Local Data is personalized towards your own needs to speed up the parsing process. This means if you only care about say Warrior percentiles on a specific encounter (That isn't "Frozen") you can configure the plugin to only parse that data. Due to the limitations of the FFLogs API parsing all the data for all the jobs and all the encounters could take upwards towards an hour on a moderately good connection. It also not recommended to use local data if you are on a metered connection. **Due to this it is recommended to not use local data, as the remote data will be updated on a daily basis anyway**

Steps below indicate how to get local data (this may change as the plugin is in beta).

1. Obtain an FFLogs API Key from FFLogs
2. In the plugin tab set the API Key to your API Key and then click Save Settings
3. Click "Configure Local"
4. In the new window click "Update Information"
5. Select the jobs and encounters you want to get data for and click "Save Config"
6. On the plugin tab check "Use Local Data"
7. Click Update
8. Wait...awhile...maybe

# Compatible Overlays

1. [Kagerou pct](https://github.com/Liquidize/kagerou) - Forked by me, this is the latest version of Kagerou with the added ability to add "Percentile" as a column to your tabs. Just like Kagerou you can easily set this as your overlay by using the following url: https://liquidize.github.io/kagerou/overlay/


If you want your overlay listed here, please message me in game or on discord.

* **Character in game:** Kaliya Y'mhitra (Goblin - NA)
* **Discord:** Kaliya#0001

# Credits

* LIquidize / Kaliya Y'mhitra (Goblin - NA) - Developer/Plugin Creator
* Kaliph Soren (Goblin - NA) - Co-developer
* [Hibiyasleep](https://github.com/hibiyasleep) - Creator of the Kagerou overlay which I forked and added functionality to.
