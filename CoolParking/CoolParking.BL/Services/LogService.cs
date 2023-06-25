// TODO: implement the LogService class from the ILogService interface.
//       One explicit requirement - for the read method, if the file is not found, an InvalidOperationException should be thrown
//       Other implementation details are up to you, they just have to match the interface requirements
//       and tests, for example, in LogServiceTests you can find the necessary constructor format.

using CoolParking.BL.Interfaces;

namespace CoolParking.BL.Services
{
    public class LogService : ILogService
    {
        public LogService(string logPath)
        {
            _logPath = logPath;
        }

        #region  ---  Interface ILogService implementation   ---

        private string _logPath;
        public string LogPath
        {
            get { return _logPath; }
        }

        public string Read()
        {
            string readTransactions = string.Empty;

            if (IsExistFile(LogPath))
            {
                using (var file = new StreamReader(LogPath))
                {
                    readTransactions = file.ReadToEnd();
                }
            }

            return readTransactions;
        }

        public void Write(string logInfo)
        {
            if (!string.IsNullOrEmpty(_logPath) && !string.IsNullOrEmpty(logInfo))
            {
                string formattedString = string.Concat(logInfo, "\n");

                using (StreamWriter write = new StreamWriter(_logPath, File.Exists(_logPath)))
                {
                    write.Write(formattedString);
                }
            }
        }

        #endregion

        #region ---Helpers---

        private bool IsExistFile(string logPath)
        {
            if (!File.Exists(logPath))
            {
                throw new System.InvalidOperationException();
            }

            return true;
        }

        #endregion
    }
}
