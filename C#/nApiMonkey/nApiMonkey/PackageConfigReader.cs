﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGet;
using System.IO;

namespace nApiMonkey
{
    //This class reads from and writes to packages.config files.
    class PackageConfigReader
    {
        //reads all the packages.config files provided as input
        //and returns a map which contains every package mapped to list of project locations in which it is referenced.
        public Dictionary<PackageElement, List<string>> readConfig(List<string> packageFilePaths, string root)//string path)
        {
            Dictionary<PackageElement, List<string>> map = new Dictionary<PackageElement, List<string>>();
            string fileName;
            Console.WriteLine("In read config");
            foreach (string path in packageFilePaths)
            {
                //Ignore packages.config files in these folders
                if (path.Contains(@"\.nuget") || path.Contains(@"\packages") || path.Contains(@"\Samples\"))
                    continue;
                fileName = path + "packages.config";
                var file = new PackageReferenceFile(fileName);

                string path1 = path.Replace(root, "");
                Console.WriteLine(path1);
                foreach (PackageReference packageReference in file.GetPackageReferences())
                {
                    PackageElement pe = new PackageElement(packageReference.Id, packageReference.Version);
                    if (!map.ContainsKey(pe))
                    {
                        //Console.WriteLine("add to map: "+packageReference.Id);
                        List<string> ans = new List<string>();
                        ans.Add(path1);
                        map.Add(pe, ans);
                    }
                    else
                    {
                        List<string> ans= map[pe];
                        ans.Add(path1);
                        map[pe] = ans;
                        //Console.WriteLine(" contains key "+ packageReference .Id+" "+ ans.Count);
                    }
                    Console.WriteLine("PackageId={0}, Version={1}", packageReference.Id, packageReference.Version);
                }
            }

            return map;
        }
        //reads all packages.config files under the root location
        public List<string> readAllConfigs(string root)
        {
            Console.WriteLine("In read all config");
            List<string> configList=new List<string>();
            string[] delimiter = new string[] {"packages.config"};
              foreach (string fileName in Directory.EnumerateFiles(root, "packages.config", SearchOption.AllDirectories))
              {
                Console.WriteLine(fileName.Split(delimiter, System.StringSplitOptions.None)[0]);
                configList.Add(fileName.Split(delimiter, System.StringSplitOptions.None)[0]);
              }
            return configList;
        }

        //writes new version of the package to packages.config file
        public void writeToConfig(string fileName, string packageId, SemanticVersion newversion)
        {
            var file = new PackageReferenceFile(fileName);
            file.DeleteEntry(packageId, null);
            file.AddEntry(packageId, newversion);
        }
    }
}
