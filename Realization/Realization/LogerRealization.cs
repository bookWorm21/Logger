using System;
using System.Collections.Generic;
using System.IO;

namespace Logger
{
    class Logger : ILog
    {
        private readonly string _logFilesPath;
        private readonly string _infoPath;
        private readonly string _errorPath;

        private List<string> _exceptions = new List<string>();
        private List<string> _warnings = new List<string>();

        public Logger(string logPath)
        {
            _logFilesPath = logPath;
            _infoPath = _logFilesPath + "/info.txt";
            _errorPath = _logFilesPath + "/error.txt";
            Directory.CreateDirectory(_logFilesPath);
        }

        public void Debug(string message)
        {
            WriteFile(_infoPath, message);
        }

        public void Debug(string message, Exception e)
        {
            string exceptionType = e.GetType().Name;
            WriteFile(_infoPath, message, exceptionType, e.Message);
        }

        public void DebugFormat(string message, params object[] args)
        {
            string objInfo = "";

            for (int i = 0; i < args.Length; i++)
            {
                objInfo += args[i].ToString() + " ";
            }

            WriteFile(_infoPath, message, objInfo);
        }

        public void Error(string message)
        {
            WriteFile(_errorPath, message);
        }

        public void Error(string message, Exception e)
        {
            string exceptionType = e.GetType().Name;
            _exceptions.Add(exceptionType);
            WriteFile(_errorPath, message, exceptionType, e.Message);
        }

        public void Error(Exception ex)
        {
            string exceptionType = ex.GetType().Name;
            _exceptions.Add(exceptionType);
            WriteFile(_errorPath, exceptionType, ex.Message);
        }

        public void ErrorUnique(string message, Exception e)
        {
            if (_exceptions.Contains(e.GetType().Name) == false)
            {
                string exceptionType = e.GetType().Name;
                _exceptions.Add(exceptionType);
                WriteFile(_errorPath, exceptionType, e.Message);
            }
        }

        public void Fatal(string message)
        {
            WriteFile(_errorPath, "fatal: ", message);
        }

        public void Fatal(string message, Exception e)
        {
            string exceptionType = e.GetType().Name;
            WriteFile(_errorPath, "fatal", message, exceptionType, e.Message);
        }

        public void Info(string message)
        {
            WriteFile(_infoPath, message);
        }

        public void Info(string message, Exception e)
        {
            WriteFile(_infoPath, e.GetType().Name, message);
        }

        public void Info(string message, params object[] args)
        {
            string objInfo = "";
            for (int i = 0; i < args.Length; i++)
            {
                objInfo += args[i].ToString() + " ";
            }

            WriteFile(_infoPath, message, objInfo);
        }

        public void SystemInfo(string message, Dictionary<object, object> properties = null)
        {
            string objInfo = "";
            foreach (var item in properties)
            {
                objInfo += item.GetType().Name;
            }

            WriteFile(_infoPath, message, objInfo);
        }

        public void Warning(string message)
        {
            WriteFile(_errorPath, message);
        }

        public void Warning(string message, Exception e)
        {
            string exceptionType = e.GetType().Name;
            _warnings.Add(exceptionType);
            WriteFile(_errorPath, message, exceptionType, e.Message);
        }

        public void WarningUnique(string message)
        {
            if (_warnings.Contains(message) == false)
            {
                _warnings.Add(message);
                WriteFile(_errorPath, message);
            }
        }

        private void WriteFile(string path, params string[] args)
        {
            using (FileStream fstream = new FileStream(path, FileMode.Append))
            {
                string text = "";
                for (int i = 0; i < args.Length; i++)
                {
                    text += string.Concat(DateTime.Now, " ", args[i], "\n");
                }
                byte[] data = System.Text.Encoding.Default.GetBytes(text);
                fstream.Write(data, 0, data.Length);
            }
        }
    }
}
