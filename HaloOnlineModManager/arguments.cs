using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using CG.Web.MegaApiClient;
using HaloOnlineLib;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xdelta.Patch;
using System.Threading.Tasks;
using System.Threading;

namespace getArgs
{
    class Program
    {
        internal static void Help()
        {
            Console.WriteLine("[/, -, --]help");
            Console.WriteLine("Displays this help\n");
            Console.WriteLine("[/, -, --]backup");
            Console.WriteLine("Backs up the selected maps folder\nExample: getMods.exe [-b, -backup] 0.4.11.2 [-f] [-b]");
            Console.WriteLine("This will copy the .dats from maps\\0.4.11.2 to _dewbackup\\maps\\0.4.11.2\n");
            Console.WriteLine("[/, -, --]restore");
            Console.WriteLine("Restores the selected maps folder\nExample: getMods.exe [-r, -restore] 0.4.11.2 [-f] [-b]");
            Console.WriteLine("This will copy the .dats from maps\\0.4.11.2 to _dewbackup\\maps\\0.4.11.2\n");
            Console.WriteLine("[/, -, --]patch");
            Console.WriteLine("Patches the .dat files in the selected maps folderExample: \ngetMods.exe [-p, -patch] 0.4.11.2 mods\\packs *HangemHigh* [-b]");
            Console.WriteLine("This will patch the .dats in maps\\0.4.11.2 with the xdelta patch files in mods\\packs\\0.4.11.2\\Maps\\HangemHigh\n");
            Console.WriteLine("[/, -, --]zip");
            Console.WriteLine("Displays the contents of a zip file\nExample: getMods.exe [-z, -zip] mods\\packs *Lockout*1.1*");
            Console.WriteLine("This will display the contents of mods\\packs\\0.4.11.2\\LockoutV1.1.zip\n");
            Console.WriteLine("[/, -, --]download");
            Console.WriteLine("Downloads a zip to the selected folder\nExample: getMods.exe [-d, -download] 0.4.11.2 http://AWebsiteLikeDropbox.com/MyAmazeBallsMod.zip MyAmazeBallsMod.zip [-b]");
            Console.WriteLine("This will download MyAmazeBallsMod.zip mods\\packs\\0.4.11.2\\MyAmazeBallsMod.zip");
            Console.WriteLine("Example: getMods.exe [-d, -download] 0.4.11.2 https://mega.nz/#!49kjTKDT!8xfkZ-bFHYVkBiTcwTXgrp9HwYZq1p_3-ZD21r0uIIW H2ABR_V1.3.zip [-b]");
            Console.WriteLine("This will download MyAmazeBallsMod.zip mods\\packs\\0.4.11.2\\H2ABR_V1.3.zip");
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
                MegaApiClient client = new MegaApiClient();
                client.LoginAnonymous();
                Uri uri = new Uri(arg2);
                Task task = client.DownloadFileAsync(uri, dlLoc + arg3);
                while (!task.IsCompleted)
                {
                    using (var progress = new ProgressBar())
                    {
                        for (; client.Progress <= 100; )
                        {
                            progress.Report((double)client.Progress / 100);
                            Thread.Sleep(20);
                            if (client.Progress == 100)
                                break;
                        }
                    }
                }
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
        internal static void Available()
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
}