using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CG.Web.MegaApiClient;
using HaloOnlineLib;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xdelta.Patch;

namespace getArgs
{
    class Program
    {
        internal static void Help()
        {
            Console.WriteLine("[/, -, --]help");
            Console.WriteLine("Displays this help\n");
            Console.WriteLine("[/, -, --]backup");
            Console.WriteLine("Backs up the selected maps folder\nExample: HaloOnlineModManager.exe [-b, -backup] 0.4.11.2 [-f] [-b]");
            Console.WriteLine("This will copy the .dats from maps\\0.4.11.2 to _dewbackup\\maps\\0.4.11.2\n");
            Console.WriteLine("[/, -, --]restore");
            Console.WriteLine("Restores the selected maps folder\nExample: HaloOnlineModManager.exe [-r, -restore] 0.4.11.2 [-f] [-b]");
            Console.WriteLine("This will copy the .dats from maps\\0.4.11.2 to _dewbackup\\maps\\0.4.11.2\n");
            Console.WriteLine("[/, -, --]patch");
            Console.WriteLine("Patches the .dat files in the selected maps folderExample: \nHaloOnlineModManager.exe [-p, -patch] 0.4.11.2 mods\\packs *HangemHigh* [-b]");
            Console.WriteLine("This will patch the .dats in maps\\0.4.11.2 with the xdelta patch files in mods\\packs\\0.4.11.2\\Maps\\HangemHigh\n");
            Console.WriteLine("[/, -, --]zip");
            Console.WriteLine("Displays the contents of a zip file\nExample: HaloOnlineModManager.exe [-z, -zip] mods\\packs *Lockout*1.1*");
            Console.WriteLine("This will display the contents of mods\\packs\\0.4.11.2\\LockoutV1.1.zip\n");
            Console.WriteLine("[/, -, --]download");
            Console.WriteLine("Downloads a zip to the selected folder\nExample: HaloOnlineModManager.exe [-d, -download] 0.4.11.2 http://AWebsiteLikeDropbox.com/MyAmazeBallsMod.zip MyAmazeBallsMod.zip [-b]");
            Console.WriteLine("This will download MyAmazeBallsMod.zip mods\\packs\\0.4.11.2\\MyAmazeBallsMod.zip");
            Console.WriteLine("Example: HaloOnlineModManager.exe [-d, -download] 0.4.11.2 https://mega.nz/#!49kjTKDT!8xfkZ-bFHYVkBiTcwTXgrp9HwYZq1p_3-ZD21r0uIIW H2ABR_V1.3.zip [-b]");
            Console.WriteLine("This will download MyAmazeBallsMod.zip mods\\packs\\0.4.11.2\\H2ABR_V1.3.zip\n");
            Console.WriteLine("[/, -, --]available");
            Console.WriteLine("Displays the contents of a zip file\nExample: HaloOnlineModManager.exe [-a, -available]");
            Console.WriteLine("This will display a list of mods for the current version of mtndew.dll");
            Console.WriteLine("\nPress Any Key To Exit");
            Console.ReadLine();
        }
        static string cwd = Directory.GetCurrentDirectory() + "\\";
        static string backupFolder = Directory.GetCurrentDirectory() + "\\_dewbackup\\maps\\";
        internal static void Backup(string arg1, bool force, bool batch)
        {
            string[] getTags = Directory.GetFiles(cwd + "maps\\" + arg1 + "\\", "*.dat", SearchOption.TopDirectoryOnly);
            Console.WriteLine("Backup started.");
            if (force == true)
            {
                foreach (string datFile in getTags)
                {
                    Stopwatch watcher = Stopwatch.StartNew();
                    string fileName = Path.GetFileName(datFile);
                    string tagsFolder = cwd + "maps\\" + arg1 + "\\";
                    Directory.CreateDirectory(backupFolder);
                    File.Copy(tagsFolder + fileName, backupFolder + arg1 + "\\" + fileName, true);
                    watcher.Stop();
                    Console.WriteLine(fileName + " has been backed up in {0}.", watcher.Elapsed);
                }
            }
            else
            {
                foreach (string datFile in getTags)
                {
                    Stopwatch watcher = Stopwatch.StartNew();
                    string fileName = Path.GetFileName(datFile);
                    string tagsFolder = cwd + "maps\\" + arg1 + "\\";
                    Directory.CreateDirectory(backupFolder);
                    File.Copy(tagsFolder + fileName, backupFolder + arg1 + "\\" + fileName);
                    watcher.Stop();
                    Console.WriteLine(fileName + " has been backed up in {0}.", watcher.Elapsed);
                }
            }
            Console.WriteLine("All .dat files have been backed up successfully.\n");
            if (batch == false)
            {
                Console.WriteLine("Press Any Key To Exit");
                Console.ReadLine();
            }
        }
        internal static void Restore(string arg1, bool force, bool batch)
        {
            string[] getTags = Directory.GetFiles(
                backupFolder + arg1 + "\\",
                "*.dat",
                SearchOption.TopDirectoryOnly
            );
            Console.WriteLine("Restore started.");
            if (force == true)
            {
                foreach (string datFile in getTags)
                {
                    Stopwatch watcher = Stopwatch.StartNew();
                    string fileName = Path.GetFileName(datFile);
                    string tagsFolder = cwd + "maps\\" + arg1 + "\\";
                    File.Copy(backupFolder + arg1 + "\\" + fileName, tagsFolder + fileName, true);
                    watcher.Stop();
                    Console.WriteLine(fileName + " has been restored in {0}.", watcher.Elapsed);
                }
            }
            else
            {
                foreach (string datFile in getTags)
                {
                    Stopwatch watcher = Stopwatch.StartNew();
                    string fileName = Path.GetFileName(datFile);
                    string tagsFolder = cwd + "maps\\" + arg1 + "\\";
                    File.Copy(backupFolder + arg1 + "\\" + fileName, tagsFolder + fileName);
                    watcher.Stop();
                    Console.WriteLine(fileName + " has been restored in {0}.", watcher.Elapsed);
                }
            }
            Console.WriteLine("All .dat files have been restored successfully.\n");
            if (batch == false)
            {
                Console.WriteLine("Press Any Key To Exit");
                Console.ReadLine();
            }
        }
        internal static void Patch(string arg1, string arg2, string arg3, bool batch)
        {
            string tagsFolder = cwd + "maps\\" + arg1 + "\\";
            string[] getModDir = Directory.GetDirectories(cwd + arg2, arg3, SearchOption.AllDirectories);
            Console.WriteLine("Patching started.");
            foreach (string modDir in getModDir)
            {
                string[] getPatchFile = Directory.GetFiles(modDir, "*.patch", SearchOption.AllDirectories);
                foreach (string patchFile in getPatchFile)
                {
                    Stopwatch watcher = Stopwatch.StartNew();
                    string[] getDatFile = Directory.GetFiles(tagsFolder, ".dat", SearchOption.AllDirectories);
                    string patchFileNameExt = Path.GetFileName(patchFile);
                    string patchFileName = Path.GetFileNameWithoutExtension(patchFileNameExt);
                    string datFileNameExt = patchFileName + ".dat";
                    PatchClass.Main(backupFolder + datFileNameExt, patchFile, tagsFolder + datFileNameExt, datFileNameExt);
                    watcher.Stop();
                    Console.WriteLine(datFileNameExt + " has been patched in {0}.", watcher.Elapsed);
                }
            }
            Console.WriteLine("All .dat files have been patched successfully.\n");
            if (batch == false)
            {
                Console.WriteLine("Press Any Key To Exit");
                Console.ReadLine();
            }

        }
        internal static void Zip(string arg1)
        {
            string[] getMods = Directory.GetFiles(cwd + arg1, "*.zip", SearchOption.AllDirectories);
            foreach (string modFile in getMods)
            {
                Console.WriteLine("Started showing " + modFile);
                using (var zipFile = new ZipFile(modFile))
                    foreach (ZipEntry file in zipFile)
                    {
                        if (!file.IsFile)
                            continue;   // Ignore directories
                        Console.WriteLine(file.Name);
                    }
                Console.WriteLine("Finished showing " + modFile + "\n");
            }
        }
        internal static void Download(string arg1, string arg2, string arg3, bool batch)
        {
            string dlLoc = cwd + "mods\\packs\\" + arg1 + "\\";
            if (arg2.Contains("google"))
                Console.WriteLine("There is currently no support for google drive or google docs.\n");
            else if (arg2.Contains("mega.co.nz") || arg2.Contains("mega.nz"))
            {
                MegaApiClient mega = new MegaApiClient();
                mega.LoginAnonymous();
                Uri uri = new Uri(arg2);
                Task t = mega.DownloadFileAsync(uri, dlLoc + arg3);
                using (var progress = new ProgressBar())
                {
                    while (!t.IsCompleted)
                    {
                        progress.Report((double)mega.Progress / 100);
                    }
                }
                mega.Logout();
            }
            else if (arg2.Contains("dropbox"))
            {
                if (arg2.Contains("dl.dropboxusercontent.com"))
                {
                    string[] file = Regex.Split(arg2, "/");
                    int num = file.Length - 1;
                    System.Net.WebClient wc = new System.Net.WebClient();
                    Directory.CreateDirectory(dlLoc);
                    Console.WriteLine("Download started for: " + file[num]);
                    Stopwatch watcher = Stopwatch.StartNew();
                    wc.DownloadFile(arg2, dlLoc + file[num]);
                    watcher.Stop();
                    Console.WriteLine("Download finished for: " + file[num] + " in {0}.\n ", watcher.Elapsed);
                    using (var zipFile = new ZipFile(dlLoc + file[num]))
                        foreach (ZipEntry inZip in zipFile)
                        {
                            if (!inZip.IsFile)
                                continue;   // Ignore directories
                            Console.WriteLine(inZip.Name);
                        }
                    Console.WriteLine("Successfully downloaded " + file[num] + "\n");
                }
                else
                {
                    string url = Regex.Replace(arg2, "www.dropbox.com", "dl.dropboxusercontent.com");
                    string[] url0 = Regex.Split(url, "\\?");
                    string[] file = Regex.Split(url0[0], "/");
                    int num = file[0].Length - 1;
                    System.Net.WebClient wc = new System.Net.WebClient();
                    Directory.CreateDirectory(dlLoc);
                    Console.WriteLine("Download started for: " + file[num]);
                    Stopwatch watcher = Stopwatch.StartNew();
                    wc.DownloadFile(url, dlLoc + file[num]);
                    watcher.Stop();
                    Console.WriteLine("Download finished for: " + file[num] + " in {0}.\n ", watcher.Elapsed);
                    using (var zipFile = new ZipFile(dlLoc + file[num]))
                        foreach (ZipEntry inZip in zipFile)
                        {
                            if (!inZip.IsFile)
                                continue;   // Ignore directories
                            Console.WriteLine(inZip.Name);
                        }
                    Console.WriteLine("Successfully downloaded " + file[num] + "\n");
                }
            }
            else
            {
                string[] file = Regex.Split(arg2, "/");
                int num = file.Length - 1;
                System.Net.WebClient wc = new System.Net.WebClient();
                Directory.CreateDirectory(dlLoc);
                Console.WriteLine("Download started for: " + file[num]);
                Stopwatch watcher = Stopwatch.StartNew();
                wc.DownloadFile(arg2, dlLoc + file[num]);
                watcher.Stop();
                Console.WriteLine("Download finished for: " + file[num] + " in {0}.\n ", watcher.Elapsed);
                using (var zipFile = new ZipFile(dlLoc + file[num]))
                    foreach (ZipEntry inZip in zipFile)
                    {
                        if (!inZip.IsFile)
                            continue;   // Ignore directories
                        Console.WriteLine(inZip.Name);
                    }
                Console.WriteLine("Successfully downloaded " + file[num] + "\n");
            }
            if (batch == false)
            {
                Console.WriteLine("Press Any Key To Exit");
                Console.ReadLine();
            }
        }
        internal static void ModsAvailable()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            dynamic mods = JsonConvert.DeserializeObject(wc.DownloadString("http://thetwist84.github.io/HaloOnlineModManager/game/game.json"));
            FileVersionInfo mtndewVersion = FileVersionInfo.GetVersionInfo(Path.Combine(Environment.CurrentDirectory, "mtndew.dll"));

            foreach (dynamic mod in mods.mods[mtndewVersion.FileVersion])
            {
                if (mod.Value.Dependencies == null)
                {
                    Console.WriteLine(mod.Value.Name + " " + mod.Value.Version);
                    if (mod.Value.Url != "")
                        Console.WriteLine(mod.Value.Url + " " + mod.Value.Filename);
                }
                else
                {
                    Console.WriteLine(mod.Value.Name + " " + mod.Value.Version);
                    if (mod.Value.Url != "")
                        Console.WriteLine(mod.Value.Url + " " + mod.Value.Filename);
                    int Count = 1;

                    foreach (string dependency in mod.Value.Dependencies)
                    {
                        if (dependency.Contains("http"))
                        {
                            Console.WriteLine("Dep " + Count + ": " + dependency);
                        }
                        else
                        {
                            dynamic dep = mods.mods[mtndewVersion.FileVersion][dependency];
                            if (dep.Version == "") 
                            {
                                Console.WriteLine("Dep " + Count + ": " + dep.Name);
                            }
                            else 
                            {
                                Console.WriteLine("Dep " + Count + ": " + dep.Name + " " + dep.Version);
                                if (mod.Value.Url != "")
                                    Console.WriteLine(dep.Url + " \"" + dep.Filename + "\"");
                            }
                        }
                        Count++;
                    }
                }
                Console.WriteLine();
            }
        }
        internal static void List(string arg0, string arg1)
        {
            string[] getMods = Directory.GetFiles(cwd + arg0, arg1, SearchOption.AllDirectories);
            foreach (string modFile in getMods)
                Console.WriteLine(modFile);
        }
    }
    /// <summary>
    /// An ASCII progress bar
    /// </summary>
    public class ProgressBar : IDisposable, IProgress<double>
    {
        private const int blockCount = 100;
        private readonly TimeSpan animationInterval = TimeSpan.FromSeconds(1.0 / 8);
        private const string animation = @"|/-\";

        private readonly Timer timer;

        private double currentProgress = 0;
        private string currentText = string.Empty;
        private bool disposed = false;
        private int animationIndex = 0;

        public ProgressBar()
        {
            timer = new Timer(TimerHandler);

            // A progress bar is only for temporary display in a console window.
            // If the console output is redirected to a file, draw nothing.
            // Otherwise, we'll end up with a lot of garbage in the target file.
            if (!Console.IsOutputRedirected)
            {
                ResetTimer();
            }
        }

        public void Report(double value)
        {
            // Make sure value is in [0..1] range
            value = Math.Max(0, Math.Min(1, value));
            Interlocked.Exchange(ref currentProgress, value);
        }

        private void TimerHandler(object state)
        {
            lock (timer)
            {
                if (disposed) return;

                int progressBlockCount = (int)(currentProgress * blockCount);
                int percent = (int)(currentProgress * 100);
                string text = string.Format("[{0}{1}] {2,3}% {3}",
                    new string('#', progressBlockCount), new string('-', blockCount - progressBlockCount),
                    percent,
                    animation[animationIndex++ % animation.Length]);
                UpdateText(text);

                ResetTimer();
            }
        }

        private void UpdateText(string text)
        {
            // Get length of common portion
            int commonPrefixLength = 0;
            int commonLength = Math.Min(currentText.Length, text.Length);
            while (commonPrefixLength < commonLength && text[commonPrefixLength] == currentText[commonPrefixLength])
            {
                commonPrefixLength++;
            }

            // Backtrack to the first differing character
            StringBuilder outputBuilder = new StringBuilder();
            outputBuilder.Append('\b', currentText.Length - commonPrefixLength);

            // Output new suffix
            outputBuilder.Append(text.Substring(commonPrefixLength));

            // If the new text is shorter than the old one: delete overlapping characters
            int overlapCount = currentText.Length - text.Length;
            if (overlapCount > 0)
            {
                outputBuilder.Append(' ', overlapCount);
                outputBuilder.Append('\b', overlapCount);
            }

            Console.Write(outputBuilder);
            currentText = text;
        }

        private void ResetTimer()
        {
            timer.Change(animationInterval, TimeSpan.FromMilliseconds(-1));
        }

        public void Dispose()
        {
            lock (timer)
            {
                disposed = true;
                UpdateText(string.Empty);
            }
        }

    }
}