using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Advanced_Combat_Tracker;

namespace PercentilePlugin
{
    // This class can determine the current plugin version, as well as the latest version
    // released of the plugin on GitHub. It is inspired by the work of anoyetta in
    // https://github.com/anoyetta/ACT.SpecialSpellTimer/blob/master/ACT.SpecialSpellTimer.Core/UpdateChecker.cs
    public class VersionChecker
    {
        public const string kReleaseUrl = "https://github.com/Liquidize/FFXIV_PercentilePlugin/releases/latest";
        public const string kIssueUrl = "https://github.com/Liquidize/FFXIV_PercentilePlugin/releases/issues";

        public VersionChecker()
        {
        }

        public Version GetLocalVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }

        public string GetLocalLocation()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().Location;
        }

        public Version GetACTVersion()
        {
            return System.Reflection.Assembly.GetAssembly(typeof(Advanced_Combat_Tracker.ActGlobals)).GetName().Version;
        }

        public string GetACTLocation()
        {
            return System.Reflection.Assembly.GetAssembly(typeof(Advanced_Combat_Tracker.ActGlobals)).Location;
        }

        public Version GetRemoteVersion()
        {
            string html;
            try
            {
                var web = new System.Net.WebClient();
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;
                var page_stream = web.OpenRead(kReleaseUrl);
                var reader = new System.IO.StreamReader(page_stream);
                html = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                PercentilePlugin.Logger.Log(LogLevel.Error,"Error fetching most recent github release: " + e.Message + "\n" + e.StackTrace);
                return new Version();
            }

            var pattern = @"href=""/liquidize/FFXIV_PercentilePlugin/releases/download/v?(?<Version>.*?)/";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var match = regex.Match(html);
            if (!match.Success)
            {
                PercentilePlugin.Logger.Log(LogLevel.Error, "Error parsing most recent github release, no match found. Please report an issue at " + kIssueUrl);
                return new Version();
            }

            string version_string = match.Groups["Version"].Value;

            pattern = @"(?<VersionNumber>(?<Major>[0-9]+)\.(?<Minor>[0-9]+)\.(?<Revision>[0-9+]))";
            regex = new Regex(pattern);
            match = regex.Match(version_string);
            if (!match.Success)
            {
                PercentilePlugin.Logger.Log(LogLevel.Error, "Error parsing most recent github release, no version number found. Please report an issue at " + kIssueUrl);
                return new Version();
            }

            return new Version(int.Parse(match.Groups["Major"].Value), int.Parse(match.Groups["Minor"].Value), int.Parse(match.Groups["Revision"].Value));
        }
    }
}
