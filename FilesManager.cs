using NT.Global.SystemConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NT.Global.IO
{
    /// <summary>
    /// Manages files and directories
    /// </summary>
    public class FilesManager
    {
        #region Files and directories management

        /// <summary>
        /// Delete file
        /// </summary>
        /// <param name="fileFullPath">Path of the file with its name</param>
        /// <returns></returns>
        public static ResultType DeleteFile(string fileFullPath)
        {
            ResultType result = ResultType.Failure;
            try
            {
                // check if the directory not exist (there is a foilder for this item), then create a new one
                if (File.Exists(fileFullPath))
                {
                    File.Delete(fileFullPath);
                    if (!File.Exists(fileFullPath))
                    {
                        result = ResultType.Success;
                        //ConnectionManager.Commit(ProcessNumber);
                    }
                    else // file not deleted
                        result = ResultType.Failure;
                }
                else // file not exist
                    result = ResultType.DoesNotExist;
            }
            catch (Exception ex)
            {
                NT.Global.Logging.LogHandler.PublishException(ex);
            }
            return result;
        }

        /// <summary>
        /// Delete list of files
        /// </summary>
        /// <param name="filesFullPathes">Pathes of the files with its names</param>
        public static void DeleteFiles(List<string> filesFullPathes)
        {
            try
            {
                foreach (string filePath in filesFullPathes)
                {
                    DeleteFile(filePath);
                }
            }
            catch (Exception ex)
            {
                NT.Global.Logging.LogHandler.PublishException(ex);
            }
        }

        /// <summary>
        /// Copy file
        /// </summary>
        /// <param name="sourceFullPath">the source path of file with file name</param>
        /// <param name="destinationPath">the destination path of file with file name</param>
        /// <returns></returns>
        public static ResultType CopyFile(string sourceFullPath, string destinationPath, string destinationFileName)
        {
            ResultType result = ResultType.Failure;
            try
            {
                if (!File.Exists(sourceFullPath))
                    return ResultType.DoesNotExist;
                if (!Directory.Exists(destinationPath))
                    Directory.CreateDirectory(destinationPath);

                string destinationFileFullPath = Path.Combine(destinationPath + destinationFileName);
                // check if the file already exists
                if (File.Exists(destinationFileFullPath))
                    return ResultType.AlreadyExist;

                File.Copy(sourceFullPath, destinationFileFullPath, true);
                if (File.Exists(destinationFileFullPath))
                    result = ResultType.Success;
                else
                    result = ResultType.Failure;
            }
            catch (Exception ex)
            {
                NT.Global.Logging.LogHandler.PublishException(ex);
            }
            return result;
        }

        /// <summary>
        /// Copy directory with its contents
        /// </summary>
        /// <param name="sourceFullPath">path source directory</param>
        /// <param name="destinationFullPath">Path of destination</param>
        /// <returns></returns>
        public static ResultType CopyDirectory(string sourceFullPath, string destinationFullPath, bool copySubDirs = false)
        {
            ResultType result = ResultType.Failure;
            List<string> copiedfilesPathes = new List<string>();
            try
            {
                if (!Directory.Exists(sourceFullPath))
                    return ResultType.DoesNotExist;

                if (!Directory.Exists(destinationFullPath))
                    Directory.CreateDirectory(destinationFullPath);

                // get the source directorty to obtain its sub directories
                DirectoryInfo sourceDirInf = new DirectoryInfo(sourceFullPath);
                // get sub directories
                DirectoryInfo[] subDirs = sourceDirInf.GetDirectories();
                // get files of the source directory to copy it
                FileInfo[] sourceDirFiles = sourceDirInf.GetFiles();
                // copy files
                foreach (FileInfo file in sourceDirFiles)
                {
                    string fullSourceFilePath = Path.Combine(sourceFullPath, file.Name);
                    string fullDestinationFilePath = Path.Combine(destinationFullPath, file.Name);
                    result = CopyFile(fullSourceFilePath, destinationFullPath, file.Name);
                    if (result == ResultType.Failure)
                    {
                        // delete file in the copied list
                        DeleteFiles(copiedfilesPathes);
                        return result;
                    }
                }

                if (copySubDirs)
                {
                    // copy sub directories
                    foreach (DirectoryInfo subDir in subDirs)
                    {
                        string fullSubDirSourcePath = Path.Combine(sourceFullPath, subDir.Name);
                        string fullSubDirDestinatioPath = Path.Combine(destinationFullPath, subDir.Name);
                        result = CopyDirectory(fullSubDirSourcePath, fullSubDirDestinatioPath);
                        if (result == ResultType.Failure)
                        {
                            // delete the copied directory
                            Directory.Delete(fullSubDirDestinatioPath);
                            return result;
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                NT.Global.Logging.LogHandler.PublishException(ex);
                //throw;
            }
            return result;
        }

        /// <summary>
        /// Move a file
        /// </summary>
        /// <param name="sourceFileFullPath">path of the file with its name</param>
        /// <param name="destinationPath">Path of destination directory</param>
        /// <param name="destinationFileName">name of the file in the destination</param>
        /// <returns></returns>
        public static ResultType MoveFile(string sourceFileFullPath, string destinationPath, string destinationFileName)
        {
            ResultType result = ResultType.Failure;
            try
            {
                if (!File.Exists(sourceFileFullPath))
                    return ResultType.DoesNotExist;
                if (!Directory.Exists(destinationPath))
                    Directory.CreateDirectory(destinationPath);

                string destinationFileFullPath = Path.Combine(destinationPath + destinationFileName);
                // check if the file already exists
                if (File.Exists(destinationFileFullPath))
                    return ResultType.AlreadyExist;
                // move the file
                File.Move(sourceFileFullPath, destinationFileFullPath);

                if (File.Exists(destinationFileFullPath))
                    result = ResultType.Success;
                else
                    result = ResultType.Failure;
            }
            catch (Exception ex)
            {
                NT.Global.Logging.LogHandler.PublishException(ex);
                //throw;
            }
            return result;
        }

        /// <summary>
        /// Move directory
        /// </summary>
        /// <param name="sourceDirFullPath"></param>
        /// <param name="destinationFullPath"></param>
        /// <returns></returns>
        public static ResultType MoveDirectory(string sourceDirFullPath, string destinationFullPath)
        {
            ResultType result = ResultType.Failure;
            try
            {
                if (!Directory.Exists(sourceDirFullPath))
                    return ResultType.DoesNotExist;
                //if (!Directory.Exists(destinationFullPath))
                //    Directory.CreateDirectory(destinationFullPath);

                Directory.Move(sourceDirFullPath, destinationFullPath);
                if (!Directory.Exists(destinationFullPath))
                    result = ResultType.Failure;
                else
                    result = ResultType.Success;
            }
            catch (Exception ex)
            {
                NT.Global.Logging.LogHandler.PublishException(ex);
                //throw;
            }
            return result;
        }

        #endregion

    }
}
