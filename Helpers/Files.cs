using DpLib.Enums;
using DpLib.Models;
using System.Globalization;
using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace DpLib.Helpers
{
    public static class Files
    {
        const int BufferSize = 4096;
        public static string ConvertSize(long size, int decimals = 2)
        {
            string[] strArray =
            [
                "o",
                "Ko",
                "Mo",
                "Go",
                "To"
            ];
            decimal[] numArray =
            [
                1M,
                1024M,
                1048576M,
                1073741824M,
                1099511627776M
            ];
            string str = string.Empty;
            for (int index = numArray.Length - 1; index >= 0; --index)
            {
                if (size >= numArray[index])
                {
                    str = Round(size / numArray[index], decimals) + " " + strArray[index];
                    break;
                }
            }
            if (string.IsNullOrEmpty(str))
                str = size.ToString() + " o";
            return str;
        }
        public static async Task<bool> DownloadFileAsync(string url, string destPath, IProgress<DownloadProgress>? progress, CancellationToken? cancellationToken = null)
        {
            string filename = Path.GetFileName(new Uri(url).LocalPath);
            destPath = Path.Combine(destPath, filename);
            try
            {
                using HttpClient client = new();

                using HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken ?? CancellationToken.None);
                response.EnsureSuccessStatusCode();

                long totalBytes = response.Content.Headers.ContentLength ?? 0L;
                byte[] buffer = new byte[8192];
                long totalBytesRead = 0L;
                int bytesRead;

                var progressStatus = new DownloadProgress() { Filename = filename, TotalBytes = totalBytes, BytesRead = totalBytesRead, Status = DOWNLOAD_PROGRESS_STATUS.DOWNLOADING };
                progress?.Report(progressStatus);

                using (Stream contentStream = await response.Content.ReadAsStreamAsync(cancellationToken ?? CancellationToken.None), fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None, buffer.Length, true))
                {
                    while ((bytesRead = await contentStream.ReadAsync(buffer, cancellationToken ?? CancellationToken.None)) > 0)
                    {
                        await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken ?? CancellationToken.None);
                        totalBytesRead += bytesRead;

                        progressStatus = new DownloadProgress() { Filename = filename, TotalBytes = totalBytes, BytesRead = totalBytesRead, Status = DOWNLOAD_PROGRESS_STATUS.DOWNLOADING };
                        progress?.Report(progressStatus);
                    }
                }

                progressStatus = new DownloadProgress() { Filename = filename, TotalBytes = totalBytes, BytesRead = totalBytesRead, Status = DOWNLOAD_PROGRESS_STATUS.COMPLETED };
                progress?.Report(progressStatus);
                return true;
            }
            catch (Exception ex)
            {
                var progressStatus = new DownloadProgress() { ErrorMessage = ex.Message, Filename = filename, Status = DOWNLOAD_PROGRESS_STATUS.FAILED };
                progress?.Report(progressStatus);
                return false;
            }
        }
        public static async Task<bool> ExtractZipEmbeddedResourceAsync(string resourceName, string destPath, IProgress<ExtractProgress>? progress = null, CancellationToken? cancellationToken = null)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string? assemblyName = assembly.GetName().Name;
                if (assemblyName != null)
                {
                    string resourcePath = $"{assemblyName.Replace(" ", "_")}.Resources.{resourceName}";
                    using Stream? resourceStream = assembly.GetManifestResourceStream(resourcePath) ?? throw new FileNotFoundException("Resource not found.", resourceName);
                    long totalBytes = resourceStream.Length;
                    long bytesRead = 0;
                    int totalEntries = 0;
                    int entriesProcessed = 0;
                    if (!Directory.Exists(destPath))
                    {
                        Directory.CreateDirectory(destPath);
                    }

                    resourceStream.Seek(0, SeekOrigin.Begin);

                    using (ZipArchive archive = new(resourceStream, ZipArchiveMode.Read))
                    {
                        totalEntries = archive.Entries.Count;
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            try
                            {
                                string outFileName = Path.Combine(destPath, entry.FullName);
                                string? directoryPath = Path.GetDirectoryName(outFileName);
                                if (directoryPath != null && !Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }


                                if (!entry.FullName.EndsWith('/'))
                                {
                                    using FileStream outFileStream = new(outFileName, FileMode.Create);
                                    using Stream entryStream = entry.Open();
                                    byte[] buffer = new byte[BufferSize];
                                    int read;
                                    while ((read = await entryStream.ReadAsync(buffer)) > 0)
                                    {
                                        await outFileStream.WriteAsync(buffer.AsMemory(0, read));
                                        bytesRead += read;
                                        progress?.Report(new ExtractProgress()
                                        {
                                            Filename = resourceName,
                                            TotalFiles = totalEntries,
                                            FilesProcessed = entriesProcessed,
                                            BytesRead = bytesRead,
                                            TotalBytes = totalBytes,
                                            CurrentFile = entry.FullName,
                                            Status = EXTRACT_PROGRESS_STATUS.EXTRACTING
                                        });
                                    }
                                }

                                entriesProcessed++;
                                progress?.Report(new ExtractProgress()
                                {
                                    Filename = resourceName,
                                    TotalFiles = totalEntries,
                                    FilesProcessed = entriesProcessed,
                                    BytesRead = bytesRead,
                                    TotalBytes = totalBytes,
                                    CurrentFile = entry.FullName,
                                    Status = EXTRACT_PROGRESS_STATUS.EXTRACTING
                                });
                            }
                            catch (Exception ex)
                            {
                                progress?.Report(new ExtractProgress()
                                {
                                    Filename = resourceName,
                                    Status = EXTRACT_PROGRESS_STATUS.FAILED,
                                    ErrorMessage = ex.Message
                                });
                                return false;
                            }
                        }
                    }

                    progress?.Report(new ExtractProgress()
                    {
                        Filename = resourceName,
                        TotalFiles = totalEntries,
                        FilesProcessed = entriesProcessed,
                        BytesRead = bytesRead,
                        TotalBytes = totalBytes,
                        Status = EXTRACT_PROGRESS_STATUS.COMPLETED
                    });
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                progress?.Report(new ExtractProgress()
                {
                    Filename = resourceName,
                    Status = EXTRACT_PROGRESS_STATUS.FAILED,
                    ErrorMessage = ex.Message
                });
                return false;
            }
        }
        public static async Task<bool> ExtractZipEmbeddedResourceAsync(string resourceName, string searchPathFilename, string destPath, IProgress<ExtractProgress>? progress = null)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string? assemblyName = assembly.GetName().Name;
                if (assemblyName != null)
                {
                    string resourcePath = $"{assemblyName.Replace(" ", "_")}.Resources.{resourceName}";
                    using Stream? resourceStream = assembly.GetManifestResourceStream(resourcePath) ?? throw new FileNotFoundException("Resource not found.", resourceName);
                    long totalBytes = resourceStream.Length;
                    long bytesRead = 0;
                    if (!Directory.Exists(destPath))
                    {
                        Directory.CreateDirectory(destPath);
                    }

                    resourceStream.Seek(0, SeekOrigin.Begin);

                    using (ZipArchive archive = new(resourceStream, ZipArchiveMode.Read))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            try
                            {
                                if (!entry.FullName.EndsWith('/') && entry.FullName.Replace("/", "\\") == searchPathFilename)
                                {
                                    string? directoryName = Path.GetDirectoryName(entry.FullName);
                                    totalBytes = entry.Length;
                                    if (directoryName != null)
                                    {
                                        string outFileName = Path.Combine(destPath, directoryName, entry.Name);
                                        if (!Directory.Exists(Path.Combine(destPath, directoryName)))
                                        {
                                            Directory.CreateDirectory(Path.Combine(destPath, directoryName));
                                        }
                                        using FileStream outFileStream = new(outFileName, FileMode.Create);
                                        using Stream entryStream = entry.Open();
                                        byte[] buffer = new byte[BufferSize];
                                        int read;
                                        while ((read = await entryStream.ReadAsync(buffer)) > 0)
                                        {
                                            await outFileStream.WriteAsync(buffer.AsMemory(0, read));
                                            bytesRead += read;
                                            progress?.Report(new ExtractProgress()
                                            {
                                                Filename = resourceName,
                                                TotalFiles = 1,
                                                FilesProcessed = 0,
                                                BytesRead = bytesRead,
                                                TotalBytes = totalBytes,
                                                CurrentFile = entry.FullName,
                                                Status = EXTRACT_PROGRESS_STATUS.EXTRACTING
                                            });
                                        }
                                        break;
                                    }
                                }
                                progress?.Report(new ExtractProgress()
                                {
                                    Filename = resourceName,
                                    TotalFiles = 1,
                                    FilesProcessed = 0,
                                    CurrentFile = entry.FullName,
                                    Status = EXTRACT_PROGRESS_STATUS.SEARCHING
                                });
                            }
                            catch (Exception ex)
                            {
                                progress?.Report(new ExtractProgress()
                                {
                                    Filename = resourceName,
                                    Status = EXTRACT_PROGRESS_STATUS.FAILED,
                                    ErrorMessage = ex.Message
                                });
                                return false;
                            }
                        }
                    }

                    progress?.Report(new ExtractProgress()
                    {
                        Filename = resourceName,
                        TotalFiles = 1,
                        FilesProcessed = 1,
                        BytesRead = bytesRead,
                        TotalBytes = totalBytes,
                        Status = EXTRACT_PROGRESS_STATUS.COMPLETED
                    });
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                progress?.Report(new ExtractProgress()
                {
                    Filename = resourceName,
                    Status = EXTRACT_PROGRESS_STATUS.FAILED,
                    ErrorMessage = ex.Message
                });
                return false;
            }
        }
        public static async Task<bool> ExtractTarGzAsync(string tarGzFilePath, string destPath, IProgress<ExtractProgress>? progress = null)
        {
            string tarGzFilename = Path.GetFileName(tarGzFilePath);
            try
            {
                using FileStream fs = new(tarGzFilePath, FileMode.Open, FileAccess.Read);
                using GZipStream gzipStream = new(fs, CompressionMode.Decompress);
                long totalBytes = fs.Length;
                long bytesRead = 0;
                byte[] buffer = new byte[8192];
                int read;

                using MemoryStream memoryStream = new();
                while ((read = await gzipStream.ReadAsync(buffer)) > 0)
                {
                    await memoryStream.WriteAsync(buffer.AsMemory(0, read));
                    bytesRead += read;
                    progress?.Report(new ExtractProgress
                    {
                        Filename = tarGzFilename,
                        BytesRead = bytesRead,
                        TotalBytes = totalBytes,
                        Status = EXTRACT_PROGRESS_STATUS.READING
                    });
                }

                memoryStream.Seek(0, SeekOrigin.Begin);
                await ExtractTarAsync(memoryStream, destPath, progress, tarGzFilename, bytesRead);
            }
            catch (Exception ex)
            {
                progress?.Report(new ExtractProgress
                {
                    Filename = tarGzFilename,
                    Status = EXTRACT_PROGRESS_STATUS.FAILED,
                    ErrorMessage = ex.Message
                });
                return false;
            }

            return true;
        }
        static async Task<bool> ExtractTarAsync(Stream stream, string outputDir, IProgress<ExtractProgress>? progress = null, string tarGzFilename = "", long totalBytesRead = 0)
        {
            var buffer = new byte[100];
            var progressData = new ExtractProgress
            {
                Filename = tarGzFilename,
                TotalBytes = totalBytesRead,
                Status = EXTRACT_PROGRESS_STATUS.EXTRACTING
            };
            while (true)
            {
                int read = await stream.ReadAsync(buffer.AsMemory(0, 100));
                if (read == 0)
                {
                    break;
                }
                var name = Encoding.ASCII.GetString(buffer).Trim('\0');
                if (string.IsNullOrWhiteSpace(name))
                {
                    break;
                }
                stream.Seek(24, SeekOrigin.Current);
                await stream.ReadAsync(buffer.AsMemory(0, 12));
                var size = Convert.ToInt64(Encoding.UTF8.GetString(buffer, 0, 12).Trim('\0').Trim(), 8);
                stream.Seek(376L, SeekOrigin.Current);
                var output = Path.Combine(outputDir, name);
                if (!Directory.Exists(Path.GetDirectoryName(output)))
                {
                    string? directoryName = Path.GetDirectoryName(output);
                    if (directoryName != null)
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    else
                    {
                        return false;
                    }
                }
                progressData.CurrentFile = name;
                progressData.Status = EXTRACT_PROGRESS_STATUS.EXTRACTING;
                if (!name.Equals("./", StringComparison.InvariantCulture))
                {
                    using var str = File.Open(output, FileMode.OpenOrCreate, FileAccess.Write);
                    var buf = new byte[size];
                    await stream.ReadAsync(buf);
                    await str.WriteAsync(buf);
                    progressData.BytesRead += size;
                }
                progressData.FilesProcessed++;
                progress?.Report(progressData);
                var pos = stream.Position;
                var offset = 512 - (pos % 512);
                if (offset == 512)
                {
                    offset = 0;
                }
                stream.Seek(offset, SeekOrigin.Current);
            }
            progressData.Status = EXTRACT_PROGRESS_STATUS.COMPLETED;
            progress?.Report(progressData);
            return true;
        }
        public static async Task<bool> ExtractTarGzAsync(string tarGzFilePath, string searchPathFilename, string destPath, IProgress<ExtractProgress>? progress = null)
        {
            string tarGzFilename = Path.GetFileName(tarGzFilePath);
            try
            {
                using FileStream fs = new(tarGzFilePath, FileMode.Open, FileAccess.Read);
                using GZipStream gzipStream = new(fs, CompressionMode.Decompress);
                long totalBytes = fs.Length;
                long bytesRead = 0;
                byte[] buffer = new byte[8192];
                int read;
                using MemoryStream memoryStream = new();
                while ((read = await gzipStream.ReadAsync(buffer)) > 0)
                {
                    await memoryStream.WriteAsync(buffer.AsMemory(0, read));
                    bytesRead += read;
                    progress?.Report(new ExtractProgress
                    {
                        Filename = tarGzFilename,
                        BytesRead = bytesRead,
                        TotalBytes = totalBytes,
                        Status = EXTRACT_PROGRESS_STATUS.READING
                    });
                }
                memoryStream.Seek(0, SeekOrigin.Begin);
                bool fileFound = await ExtractTarAsync(memoryStream, searchPathFilename, destPath, progress, tarGzFilename, bytesRead);
                if (!fileFound)
                {
                    progress?.Report(new ExtractProgress
                    {
                        Filename = tarGzFilename,
                        Status = EXTRACT_PROGRESS_STATUS.FAILED,
                        ErrorMessage = "File not found in the archive"
                    });
                    return false;
                }
            }
            catch (Exception ex)
            {
                progress?.Report(new ExtractProgress
                {
                    Filename = tarGzFilename,
                    Status = EXTRACT_PROGRESS_STATUS.FAILED,
                    ErrorMessage = ex.Message
                });
                return false;
            }
            return true;
        }
        static async Task<bool> ExtractTarAsync(Stream stream, string searchPathFilename, string outputDir, IProgress<ExtractProgress>? progress = null, string tarGzFilename = "", long totalBytesRead = 0)
        {
            var buffer = new byte[100];
            var progressData = new ExtractProgress
            {
                Filename = tarGzFilename,
                TotalBytes = totalBytesRead,
                Status = EXTRACT_PROGRESS_STATUS.EXTRACTING
            };
            bool fileFound = false;
            while (true)
            {
                int read = await stream.ReadAsync(buffer.AsMemory(0, 100));
                if (read == 0)
                {
                    break;
                }
                var name = Encoding.ASCII.GetString(buffer).Trim('\0');
                if (string.IsNullOrWhiteSpace(name))
                {
                    break;
                }
                stream.Seek(24, SeekOrigin.Current);
                await stream.ReadAsync(buffer.AsMemory(0, 12));
                var size = Convert.ToInt64(Encoding.UTF8.GetString(buffer, 0, 12).Trim('\0').Trim(), 8);
                stream.Seek(376L, SeekOrigin.Current);
                if (name.Equals(searchPathFilename, StringComparison.InvariantCultureIgnoreCase))
                {
                    var output = Path.Combine(outputDir, name);
                    if (!Directory.Exists(Path.GetDirectoryName(output)))
                    {
                        string? directoryName = Path.GetDirectoryName(output);
                        if (directoryName != null)
                        {
                            Directory.CreateDirectory(directoryName);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    progressData.CurrentFile = name;
                    progressData.Status = EXTRACT_PROGRESS_STATUS.EXTRACTING;
                    if (!name.Equals("./", StringComparison.InvariantCulture))
                    {
                        using var str = File.Open(output, FileMode.OpenOrCreate, FileAccess.Write);
                        var buf = new byte[size];
                        await stream.ReadAsync(buf);
                        await str.WriteAsync(buf);
                        progressData.BytesRead += size;
                    }
                    fileFound = true;
                    progressData.FilesProcessed++;
                    progress?.Report(progressData);
                }
                else
                {
                    // Skip this file
                    stream.Seek(size, SeekOrigin.Current);
                }

                var pos = stream.Position;
                var offset = 512 - (pos % 512);
                if (offset == 512)
                {
                    offset = 0;
                }
                stream.Seek(offset, SeekOrigin.Current);
            }
            progressData.Status = fileFound ? EXTRACT_PROGRESS_STATUS.COMPLETED : EXTRACT_PROGRESS_STATUS.FAILED;
            progress?.Report(progressData);
            return fileFound;
        }
        public static async Task<bool> ExtractZipFileAsync(string zipFilePath, string destPath, IProgress<ExtractProgress>? progress = null)
        {
            try
            {
                string zipFilename = Path.GetFileName(zipFilePath);
                using ZipArchive archive = ZipFile.OpenRead(zipFilePath);
                int totalEntries = archive.Entries.Count;
                int entriesProcessed = 0;
                long totalBytes = archive.Entries.Where(e => !e.FullName.EndsWith('/')).Sum(e => e.Length);
                long bytesRead = 0;

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    try
                    {
                        string dest = Path.Combine(destPath, entry.FullName);
                        string? directoryPath = Path.GetDirectoryName(dest);
                        if (!string.IsNullOrEmpty(entry.Name) && directoryPath != null) // Not a directory entry
                        {
                            Directory.CreateDirectory(directoryPath); // Ensure directory exists

                            entry.ExtractToFile(dest, true);
                            bytesRead += entry.Length;
                        }

                        entriesProcessed++;
                        progress?.Report(new ExtractProgress()
                        {
                            Filename = zipFilename,
                            TotalFiles = totalEntries,
                            FilesProcessed = entriesProcessed,
                            BytesRead = bytesRead,
                            TotalBytes = totalBytes,
                            CurrentFile = entry.FullName
                        });
                        await Task.Delay(100);
                    }
                    catch (IOException ioEx)
                    {
                        Console.WriteLine($"I/O error while extracting file {entry.FullName}: {ioEx.Message}");
                        progress?.Report(new ExtractProgress()
                        {
                            Filename = zipFilename,
                            TotalFiles = totalEntries,
                            FilesProcessed = entriesProcessed,
                            BytesRead = bytesRead,
                            TotalBytes = totalBytes,
                            CurrentFile = entry.FullName,
                            ErrorMessage = ioEx.Message
                        });
                        return false;
                    }
                    catch (UnauthorizedAccessException uaEx)
                    {
                        Console.WriteLine($"Access denied while extracting file {entry.FullName}: {uaEx.Message}");
                        progress?.Report(new ExtractProgress()
                        {
                            Filename = zipFilename,
                            TotalFiles = totalEntries,
                            FilesProcessed = entriesProcessed,
                            BytesRead = bytesRead,
                            TotalBytes = totalBytes,
                            CurrentFile = entry.FullName,
                            ErrorMessage = uaEx.Message
                        });
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error while extracting file {entry.FullName}: {ex.Message}");
                        progress?.Report(new ExtractProgress()
                        {
                            Filename = zipFilename,
                            TotalFiles = totalEntries,
                            FilesProcessed = entriesProcessed,
                            BytesRead = bytesRead,
                            TotalBytes = totalBytes,
                            CurrentFile = entry.FullName,
                            ErrorMessage = ex.Message
                        });
                        return false;
                    }
                }
                return true;
            }
            catch (FileNotFoundException fnfEx)
            {
                Console.WriteLine($"ZIP file not found: {fnfEx.Message}");
                progress?.Report(new ExtractProgress()
                {
                    ErrorMessage = fnfEx.Message
                });
                return false;
            }
            catch (InvalidDataException idEx)
            {
                Console.WriteLine($"Invalid ZIP file: {idEx.Message}");
                progress?.Report(new ExtractProgress()
                {
                    ErrorMessage = idEx.Message
                });
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while opening ZIP file: {ex.Message}");
                progress?.Report(new ExtractProgress()
                {
                    ErrorMessage = ex.Message
                });
                return false;
            }
        }
        public static async Task<bool> ExtractZipFileAsync(string zipFilePath, string searchPathFilename, string destPath, IProgress<ExtractProgress>? progress = null)
        {
            try
            {
                string zipFilename = Path.GetFileName(zipFilePath);
                using ZipArchive archive = ZipFile.OpenRead(zipFilePath);
                int totalEntries = archive.Entries.Count;
                int entriesProcessed = 0;
                long totalBytes = archive.Entries.Where(e => !e.FullName.EndsWith('/')).Sum(e => e.Length);
                long bytesRead = 0;

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    try
                    {
                        destPath = Path.Combine(destPath, entry.FullName);
                        string? directoryPath = Path.GetDirectoryName(destPath);
                        if (!string.IsNullOrEmpty(entry.Name) && directoryPath != null && entry.FullName.Replace("/", "\\") == searchPathFilename) // Not a directory entry
                        {
                            Directory.CreateDirectory(directoryPath); // Ensure directory exists

                            entry.ExtractToFile(destPath, true);
                            bytesRead += entry.Length;
                            entriesProcessed++;
                            progress?.Report(new ExtractProgress()
                            {
                                Filename = zipFilename,
                                TotalFiles = 1,
                                FilesProcessed = 1,
                                BytesRead = bytesRead,
                                TotalBytes = totalBytes,
                                CurrentFile = entry.FullName
                            });
                            return true;
                        }
                        await Task.Delay(100);
                    }
                    catch (IOException ioEx)
                    {
                        Console.WriteLine($"I/O error while extracting file {entry.FullName}: {ioEx.Message}");
                        progress?.Report(new ExtractProgress()
                        {
                            Filename = zipFilename,
                            TotalFiles = totalEntries,
                            FilesProcessed = entriesProcessed,
                            BytesRead = bytesRead,
                            TotalBytes = totalBytes,
                            CurrentFile = entry.FullName,
                            ErrorMessage = ioEx.Message
                        });
                        return false;
                    }
                    catch (UnauthorizedAccessException uaEx)
                    {
                        Console.WriteLine($"Access denied while extracting file {entry.FullName}: {uaEx.Message}");
                        progress?.Report(new ExtractProgress()
                        {
                            Filename = zipFilename,
                            TotalFiles = totalEntries,
                            FilesProcessed = entriesProcessed,
                            BytesRead = bytesRead,
                            TotalBytes = totalBytes,
                            CurrentFile = entry.FullName,
                            ErrorMessage = uaEx.Message
                        });
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error while extracting file {entry.FullName}: {ex.Message}");
                        progress?.Report(new ExtractProgress()
                        {
                            Filename = zipFilename,
                            TotalFiles = totalEntries,
                            FilesProcessed = entriesProcessed,
                            BytesRead = bytesRead,
                            TotalBytes = totalBytes,
                            CurrentFile = entry.FullName,
                            ErrorMessage = ex.Message
                        });
                        return false;
                    }
                }
                return false;
            }
            catch (FileNotFoundException fnfEx)
            {
                Console.WriteLine($"ZIP file not found: {fnfEx.Message}");
                progress?.Report(new ExtractProgress()
                {
                    ErrorMessage = fnfEx.Message
                });
                return false;
            }
            catch (InvalidDataException idEx)
            {
                Console.WriteLine($"Invalid ZIP file: {idEx.Message}");
                progress?.Report(new ExtractProgress()
                {
                    ErrorMessage = idEx.Message
                });
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while opening ZIP file: {ex.Message}");
                progress?.Report(new ExtractProgress()
                {
                    ErrorMessage = ex.Message
                });
                return false;
            }
        }
        public static string GetFileNameFromUrl(string downloadUrl)
        {
            try
            {
                Uri uri = new (downloadUrl);
                return Path.GetFileName(uri.LocalPath);
            }
            catch (UriFormatException ex)
            {
                Console.WriteLine($"Error parsing URL: {ex.Message}");
                return string.Empty;
            }
        }
        public static string Round(decimal value, int decimals)
        {
            string format = "0." + new string('#', decimals);
            return value.ToString(format, CultureInfo.InvariantCulture);
        }
    }
}
