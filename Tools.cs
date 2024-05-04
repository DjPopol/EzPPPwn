namespace Ez_PPPwn
{
    public static class Tools
    {
        public static readonly string PathTmp = Path.Combine(Path.GetTempPath(), "Ez_PPPwn");
        public async static void CleanPathTmp()
        {
            try
            {
                if (Directory.Exists(PathTmp))
                {
                    Directory.Delete(PathTmp, true);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Delete PathTmp\n{ex.Message}");   
            }
            await Task.Delay(100);
        }
        public static string GetStringBetween(string text, string start, string end = "")
        {
            if (end == string.Empty || end == "")
            {
                end = start;
            }
            int startIndex = text.IndexOf(start) + start.Length;
            string str = text[startIndex..];
            int endIndex = str.IndexOf(end);
            return str[..endIndex];
        }
        
        public static string[] GetFirmwareFromOffsets(string offsetsPath)
        {
            List<string> firmwares = [];
            foreach (string line in File.ReadLines($"{offsetsPath}"))
            {
                if (line.Contains("class OffsetsFirmware_"))
                {
                    string firmware = GetStringBetween(line, "class OffsetsFirmware_", ":");
                    if (firmware == "903_904" || firmware == "903" || firmware == "904")
                    {
                        if(!firmwares.Contains("903"))
                        {
                            firmwares.Add("903");
                        }
                        if (!firmwares.Contains("904"))
                        {
                            firmwares.Add("904");
                        }
                    }
                    else if (firmware == "950_960" || firmware == "950" || firmware == "960")
                    {
                        if (!firmwares.Contains("950"))
                        {
                            firmwares.Add("950");
                        }
                        if (!firmwares.Contains("960"))
                        {
                            firmwares.Add("960");
                        }
                    }
                    else if (firmware == "1000_1001" || firmware == "1000" || firmware == "1001")
                    {
                        if (!firmwares.Contains("1000"))
                        {
                            firmwares.Add("1000");
                        }
                        if (!firmwares.Contains("1001"))
                        {
                            firmwares.Add("1001");
                        }
                    }
                    else if (firmware == "1050_1070_1071" || firmware == "1050_1070" || firmware == "1050_1071" || firmware == "1050" || firmware == "1070")
                    {
                        if (!firmwares.Contains("1050"))
                        {
                            firmwares.Add("1050");
                        }
                        if (!firmwares.Contains("1070"))
                        {
                            firmwares.Add("1070");
                        }
                        if (!firmwares.Contains("1071"))
                        {
                            firmwares.Add("1071");
                        }
                    }
                    else if (!firmwares.Contains(firmware))
                    {
                        firmwares.Add(firmware);
                    }
                }
            }
            return [.. firmwares];
        }
        public static FirmwaresScriptInfos? GetFirmwareFromScript(string scriptPath)
        {
            List<string> firmwares = [];
            string defaultFirmware = string.Empty;
            bool lineFound = false;
            bool defaultFirmwareFound = false;
            string listFirmware = string.Empty;
            foreach (string line in File.ReadLines($"{scriptPath}"))
            {
                if (line.Contains("choices=["))
                {
                    lineFound = true;
                    if (line.Contains(']'))
                    {
                        lineFound = false;
                        string[] f = GetStringBetween(line, "[", "]").Replace("'", "").Split(',');
                        foreach (string firmware in f)
                        {
                            firmwares.Add(firmware.Trim());
                        }
                        if(line.Contains("default='"))
                        {
                            defaultFirmwareFound = true;
                            defaultFirmware = GetStringBetween(line, "default='", "'");
                        }
                    }

                }
                else if(lineFound && !line.Contains(']'))
                {
                    listFirmware += line.Trim();
                }
                else if (lineFound && line.Contains(']'))
                {
                    lineFound = false;
                    string str = line.Substring(0, line.IndexOf(']'));
                    listFirmware += str;
                    string[] f = listFirmware.Replace("'", "").Split(',');
                    foreach (string firmware in f)
                    {
                        firmwares.Add(firmware.Trim());
                    }
                    if (line.Contains("default='"))
                    {
                        defaultFirmwareFound = true;
                        defaultFirmware = GetStringBetween(line, "default='", "'");
                    }
                }
                else if (!defaultFirmwareFound &&  line.Contains("default='"))
                {
                    defaultFirmwareFound = true;
                    defaultFirmware = GetStringBetween(line, "default='", "'");
                }


            }
            if (defaultFirmware != string.Empty && firmwares.Count > 0)
            {
                return new FirmwaresScriptInfos(defaultFirmware, [.. firmwares]);
            }
            else
            {
                return null;
            }
        }
        public static string ReplaceStringBetween(string text, string replace, string start, string end = "")
        {
            if (end == string.Empty || end == "")
            {
                end = start;
            }
            int startIndex = text.IndexOf(start) + start.Length;
            string str = text[startIndex..];
            int endIndex = str.IndexOf(end);

            string strBetween = str[..endIndex];
            return text.Replace(strBetween, replace);
        }
        public async static Task<bool> SaveAllScripts(string path, string pppwnPath, string offsetsPath, string networkInterface, string firmware, string stage1Path, string stage2Path, bool showResult = false)
        {
            try
            {
                await SavePythonScripts(Path.Combine(path,Path.GetFileName(pppwnPath)), pppwnPath, offsetsPath, firmware);
                if (!Directory.Exists(Path.Combine(path, $"{firmware}")))
                {
                    Directory.CreateDirectory(Path.Combine(path, $"{firmware}"));
                }
                string stage1PathOut = Path.Combine(path, firmware, "stage1.bin");
                File.Copy(stage1Path, stage1PathOut, true);
                string stage2PathOut = Path.Combine(path, firmware, "stage2.bin");
                File.Copy(stage2Path, stage2PathOut, true);
                string shellPathOut = Path.Combine(path, $"pppwn{firmware}.sh");
                if (File.Exists(shellPathOut))
                {
                    File.Delete(shellPathOut);
                }
                await SaveShell(shellPathOut, pppwnPath, networkInterface, firmware, stage1PathOut, stage2PathOut);
                await Task.Delay(100);
                if(showResult)
                {
                    MessageBox.Show($"Successfully created in {path}!\nYou can execute pppwn{firmware}.bat everywhere!!!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                return true;
            }
            catch(IOException ex)
            {
                MessageBox.Show($"Error SaveScripts : {ex.Message}");
                return false;
            }
        }

        public async static Task<bool> SaveShell(string pathFilename, string pppwnPath, string networkInterface, string firmware, string stage1Path, string stage2Path, bool showResult = false)
        {
            try
            {
                StreamWriter sw;
                if (File.Exists(pathFilename))
                {
                    File.Delete(pathFilename);
                }
                sw = new(pathFilename);
                sw.WriteLine($"python -u \"{pppwnPath}\" --interface=\"{networkInterface}\" --fw={firmware.Replace(".", "")} --stage1=\"{stage1Path}\" --stage2=\"{stage2Path}\"\npause");
                sw.Close();
                await Task.Delay(100);
                if (showResult)
                {
                    MessageBox.Show($"{pathFilename} Successfully created!\nYou can execute pppwn{Path.GetFileName(pathFilename)} everywhere!!!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                return true;
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error SaveBat : {ex.Message}");
                return false;
            }
        }
        public async static Task<bool> SavePythonScripts(string pathFilename, string pppwnPath, string offsetsPath, string firmware, bool showResult = false)
        {
            try
            {
                string? directoryName = Path.GetDirectoryName(pathFilename);
                if (pathFilename != null  && pathFilename != string.Empty)
                {
                    FirmwaresScriptInfos? firmwaresScriptInfos = GetFirmwareFromScript(pppwnPath);
                    StreamWriter sw;
                    List<string> firmwaresToAdd = [];
                    if (firmwaresScriptInfos != null)
                    {
                        string[] offsetsFirmware = GetFirmwareFromOffsets(offsetsPath);
                        if (offsetsFirmware != null && offsetsFirmware.Length > 0)
                        {
                            foreach (string fo in offsetsFirmware)
                            {
                                if (!firmwaresScriptInfos.Firmwares.Contains(fo))
                                {
                                    firmwaresToAdd.Add(fo);
                                }
                            }
                        }
                    }
                    string script = string.Empty;
                    bool condFind = false;
                    bool f903_904found = false;
                    bool f950_960found = false;
                    bool f1000_1001Found = false;
                    bool f1050_1070_1071Found = false;
                    if (firmwaresToAdd.Count > 0 && firmwaresScriptInfos != null)
                    {
                        firmwaresToAdd.Sort((string x, string y) => x.CompareTo(y));
                        foreach (string line in File.ReadLines($"{pppwnPath}"))
                        {
                            if (line.Contains("parser.add_argument('--fw', choices=["))
                            {
                                string firmwares = GetStringBetween(line, "[", "]");
                                foreach (string f in firmwaresToAdd)
                                {
                                    firmwares += $", '{f}'";
                                }
                                string str = ReplaceStringBetween(line, firmwares, "[", "]");
                                if(firmware != null && firmware != string.Empty)
                                {
                                    str = ReplaceStringBetween(str, firmware.Replace(".", ""), "default='", "'");
                                }
                                
                                script += str + "\n";
                            }
                            
                            else if (!line.Contains("elif args.fw == '") && line.Contains("if args.fw == '"))
                            {
                                condFind = true;
                                script += line + "\n";
                            }
                            else if (!f903_904found && (line.Contains("elif args.fw in ('903', '904')") || line.Contains("elif args.fw == '903'") || line.Contains("elif args.fw == '904'")))
                            {
                                f903_904found = true;
                                if (!line.Contains("elif args.fw in ('903', '904')"))
                                {
                                    script += $"    elif args.fw in ('903', '904'):\n";
                                    script += $"        offs = OffsetsFirmware_903_904()\n";
                                }
                                else
                                {
                                    script += line + "\n";
                                }

                            }
                            else if (!f950_960found && (line.Contains("elif args.fw in ('950', '960')") || line.Contains("elif args.fw == '950'") || line.Contains("elif args.fw == '960'")))
                            {
                                f950_960found = true;
                                if (!line.Contains("elif args.fw in ('950', '960')"))
                                {
                                    script += $"    elif args.fw in ('950', '960'):\n";
                                    script += $"        offs = OffsetsFirmware_950_960()\n";
                                }
                                else
                                {
                                    script += line + "\n";
                                }

                            }
                            else if (!f1000_1001Found && (line.Contains("elif args.fw in ('1000', '1001')") || line.Contains("elif args.fw == '1000'") || line.Contains("elif args.fw == '1001'")))
                            {
                                f1000_1001Found = true;
                                if(!line.Contains("elif args.fw in ('1000', '1001')"))
                                {
                                    script += $"    elif args.fw in ('1000', '1001'):\n";
                                    script += $"        offs = OffsetsFirmware_1000_1001()\n";
                                }
                                else
                                {
                                    script += line + "\n";
                                }

                            }
                            else if (!f1050_1070_1071Found && (line.Contains("elif args.fw in ('1050', '1070', '1071')") ||
                                line.Contains("elif args.fw in ('1050', '1070')") || line.Contains("elif args.fw in ('1070', '1071')") || line.Contains("elif args.fw in ('1050', '1071')") ||
                                line.Contains("elif args.fw == '1050'") || line.Contains("elif args.fw == '1070'") || line.Contains("elif args.fw == '1071'")))
                            {
                                f1050_1070_1071Found = true;
                                if (!line.Contains("elif args.fw in ('1050', '1070', '1071' )"))
                                {
                                    script += $"    elif args.fw in ('1050', '1070', '1071'):\n";
                                    script += $"        offs = OffsetsFirmware_1050_1070_1071()\n";
                                }
                                else
                                {
                                    script += line + "\n";
                                }

                            }
                            else if (condFind && (!line.Contains("elif args.fw == '") && !line.Contains("offs = OffsetsFirmware_")))
                            {
                                condFind = false;
                                foreach (string f in firmwaresToAdd)
                                {
                                    if(f != "903" && f != "904" && f != "950" && f != "960" && f != "1000" && f != "1001" && f != "1050" && f != "1070" && f != "1071") 
                                    {
                                        script += $"    elif args.fw == '{f}':\n";
                                        script += $"        offs = OffsetsFirmware_{f}()\n";
                                    }
                                    else if (!f903_904found && (f == "903" || f == "904"))
                                    {
                                        f903_904found = true;
                                        script += $"    elif args.fw in ('903', '904'):\n";
                                        script += $"        offs = OffsetsFirmware_903_904()\n";
                                    }
                                    else if (!f950_960found && (f == "950" || f == "960"))
                                    {
                                        f950_960found = true;
                                        script += $"    elif args.fw in ('950', '960'):\n";
                                        script += $"        offs = OffsetsFirmware_950_960()\n";
                                    }
                                    else if(!f1000_1001Found && (f == "1000" || f == "1001"))
                                    {
                                        f1000_1001Found = true;
                                        script += $"    elif args.fw in ('1000', '1001'):\n";
                                        script += $"        offs = OffsetsFirmware_1000_1001()\n";
                                    }
                                    else if (!f1050_1070_1071Found && (f == "1050" || f == "1070" || f == "1071"))
                                    {
                                        f1050_1070_1071Found = true;
                                        script += $"    elif args.fw in ('1050', '1070', '1071'):\n";
                                        script += $"        offs = OffsetsFirmware_1050_1070_1071()\n";
                                    }
                                }
                                script += line + "\n";
                            }
                            else
                            {
                                script += line + "\n";
                            }
                        }
                        await Task.Delay(100);
                        sw = new(pathFilename);
                        sw.WriteLine(script);
                        sw.Close();
                    }
                    else
                    {
                        File.Copy(pppwnPath, pathFilename, true);
                    }
                    if (directoryName != null)
                    {
                        script = string.Empty;
                        f903_904found = false;
                        f950_960found = false;
                        f1000_1001Found = false;
                        f1050_1070_1071Found = false;
                        foreach (string line in File.ReadLines($"{offsetsPath}"))
                        {
                            if (line.Contains("class OffsetsFirmware_"))
                            {
                                string f = GetStringBetween(line, "class OffsetsFirmware_", ":");
                                if (f == "903_904" && !f903_904found)
                                {
                                    f903_904found = true;
                                    script += line + "\n";
                                }
                                else if (f == "903" && !f903_904found)
                                {
                                    f903_904found = true;
                                    script += ReplaceStringBetween(line, "903_904", "class OffsetsFirmware_", ":") + "\n";
                                }
                                else if (f == "904" && !f903_904found)
                                {
                                    f903_904found = true;
                                    script += ReplaceStringBetween(line, "903_904", "class OffsetsFirmware_", ":") + "\n";
                                }
                                if (f == "950_960" && !f950_960found)
                                {
                                    f950_960found = true;
                                    script += line + "\n";
                                }
                                else if (f == "950" && !f950_960found)
                                {
                                    f950_960found = true;
                                    script += ReplaceStringBetween(line, "950_960", "class OffsetsFirmware_", ":") + "\n";
                                }
                                else if (f == "960" && !f950_960found)
                                {
                                    f950_960found = true;
                                    script += ReplaceStringBetween(line, "950_960", "class OffsetsFirmware_", ":") + "\n";
                                }
                                else if (f == "1000_1001" && !f1000_1001Found)
                                {
                                    f1000_1001Found = true;
                                    script += line;
                                }
                                else if (f == "1000" && !f1000_1001Found)
                                {
                                    f1000_1001Found = true;
                                    script += ReplaceStringBetween(line, "1000_1001", "class OffsetsFirmware_", ":") + "\n";
                                }
                                else if (f == "1001" && !f1000_1001Found)
                                {
                                    f1000_1001Found = true;
                                    script += ReplaceStringBetween(line, "1000_1001", "class OffsetsFirmware_", ":") + "\n";
                                }
                                else if (f == "1050_1070_1071" && !f1050_1070_1071Found)
                                {
                                    f1050_1070_1071Found = true;
                                    script += line;
                                }
                                else if (f == "1050" && !f1050_1070_1071Found)
                                {
                                    f1050_1070_1071Found = true;
                                    script += ReplaceStringBetween(line, "1050_1070_1071", "class OffsetsFirmware_", ":") + "\n";
                                }
                                else if (f == "1070" && !f1050_1070_1071Found)
                                {
                                    f1050_1070_1071Found = true;
                                    script += ReplaceStringBetween(line, "1050_1070_1071", "class OffsetsFirmware_", ":") + "\n";
                                }
                                else if (f == "1071" && !f1050_1070_1071Found)
                                {
                                    f1050_1070_1071Found = true;
                                    script += ReplaceStringBetween(line, "1050_1070_1071", "class OffsetsFirmware_", ":") + "\n";
                                }
                                else
                                {
                                    script += line + "\n";
                                }
                            }
                            else
                            {
                                script += line + "\n";
                            }
                        }
                        sw = new(Path.Combine(directoryName, "offsets.py"));
                        sw.WriteLine(script);
                        sw.Close();
                    }
                    await Task.Delay(100);
                    if (showResult)
                    {
                        MessageBox.Show($"Successfully created in {directoryName}!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    return true;
                }
                return false;
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error SavePythonScripts : {ex.Message}");
                return false;
            }
        }
    }
}
