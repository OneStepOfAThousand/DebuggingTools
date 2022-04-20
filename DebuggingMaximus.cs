namespace DebuggingMaximus
{
    /// <summary>
    /// General debugger, easy to use for less complex debugging.
    /// </summary>
    public static class Debugging
    {
        static readonly FileManager fileManager = new();
        static readonly FileWriter fileWriter = new();
        static bool debugStarted = false;

        /// <summary>
        /// Sets the debugging folders directory, needs only to be called if you want to set a custom directory for where the debug log folder should be.
        /// </summary>
        /// <param name="directory"></param>
        public static void SetDir(string directory)
        {
            fileManager.SetDir(directory, string.Empty);
            fileWriter.SetFileDir(fileManager.FileDir);
            if(!debugStarted)fileWriter.Log("Debug Started");
            debugStarted = true;
        }

        /// <summary>
        /// Writes the debug message to file. OBS, the debug folder directory can be set with Debugging.SetDir().
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            if (!debugStarted) SetDir(string.Empty);
            fileWriter.Log(message);
        }

        /// <summary>
        /// Writes the error message to file. OBS, the debug folder directory can be set with Debugging.SetDir().
        /// </summary>
        /// <param name="message"></param>
        public static void LogError(string message)
        {
            if (!debugStarted) SetDir(string.Empty);
            fileWriter.Log("ERROR : "+message);
        }
    }

    /// <summary>
    /// Instance Debugger with the ability to set a unique debug log folder name. Unlike the static class Debugging.
    /// </summary>
    public class Debugger
    {
        readonly FileWriter fileWriter = new();
        readonly FileManager fileManager = new();
        bool hasDirectory = false;

        /// <summary>
        /// Sets the debuggers working directory and debug folders name. OBS, if this is not set the debugger will use the default paramaters, aka same as the static class Debugging.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="folderName"></param>
        public void SetDir(string directory, string folderName)
        {
            fileManager.SetDir(directory, folderName);
            fileWriter.SetFileDir(fileManager.FileDir);
            if (!hasDirectory) fileWriter.Log("Debug Started");
            hasDirectory = true;
        }

        /// <summary>
        /// Logs the messege to a debug file.
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            if (!hasDirectory) fileManager.SetDir(string.Empty, string.Empty);
            fileWriter.Log(message);
        }

        /// <summary>
        /// Writes the error message to file. OBS, the debug folder directory can be set with Debugging.SetDir().
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message)
        {
            if (!hasDirectory) SetDir(string.Empty,string.Empty);
            fileWriter.Log("ERROR : " + message);
        }
    }

    /// <summary>
    /// The FileWriter writes your debug messages to the debug file log.
    /// </summary>
    public class FileWriter
    {
        StreamWriter sw = StreamWriter.Null;
        string fileDir = string.Empty;
        string msg = string.Empty;
        int index = 0;

        /// <summary>
        /// Sets the file writers file directory, aka the files position. OBS, the FileManager class handles files and produces a fileDirectory for you, aka FileManager.FileDir.
        /// </summary>
        /// <param name="fileDirectory"></param>
        public void SetFileDir(string fileDirectory)
        {
            fileDir = fileDirectory;
        }

        /// <summary>
        /// Logs the messege to the set debug file. OBS, the FileManager class handles files.
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            sw = new StreamWriter(fileDir, true);
            msg = DateTime.Now.TimeOfDay.ToString();
            msg = msg.Remove(msg.LastIndexOf("."));
            msg += " :" + index + ": ";
            index++;
            msg += message;
            sw.WriteLine(msg);
            sw.Flush();
            sw.Close();
        }
    }

    /// <summary>
    /// The FileManager handles your debug log file's directory position and directory folder name.
    /// </summary>
    public class FileManager
    {       
        string dir = string.Empty;
        string dirName = string.Empty;
        string fileName = string.Empty;
        string fileDir = string.Empty;
        /// <summary>
        /// The active debug file's directory. OBS, recommended to set FileManager.SetDir() before using this variable or you will get the default directory and debug folder.
        /// </summary>
        public string FileDir
        {
            get
            {
                if (fileDir == string.Empty) GenerateFile();
                return fileDir;
            }
        }

        /// <summary>
        /// Sets the filemanagers working directory as well as the debug folders name. 
        /// Use the input, string.Empty, if you wish to use the default directory and/or folderName.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public bool SetDir(string directory, string folderName) //Return true if succesfull, creates debug folder in directory
        {
            if (directory == string.Empty) directory = Directory.GetCurrentDirectory();
            if (folderName == string.Empty) dirName = "Debugg";
            else dirName = folderName;
            directory = Path.Combine(directory, dirName);
            Directory.CreateDirectory(directory);
            if (!Directory.Exists(directory)) return false;
            dir = directory;
            return true;
        }

        void GenerateFile()
        {
            if (fileName == string.Empty)
            {
                fileName = "DebugLog_";
                fileName += DateTime.Now.ToString().Replace(" ", "_").Replace(":", "-");
                fileName += ".txt";
            }
            if (dir == string.Empty) SetDir(string.Empty, string.Empty);
            fileDir = Path.Combine(dir, fileName);
            File.Create(fileDir).Close();
        }
    }
}