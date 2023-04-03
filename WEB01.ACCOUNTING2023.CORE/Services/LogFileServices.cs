using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB01.ACCOUNTING2023.CORE.Services
{
    public class LogFileServices
    {
        private FileStream _objectFile;

        public LogFileServices()
        {
            _objectFile = new FileStream(path: @"../Logger/logger.text", mode: FileMode.OpenOrCreate);
        }

        public void Close()
        {
            _objectFile.Close();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public int ReadFile()
        {
            throw new NotImplementedException();
        }
        public int WriteFile()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///     ghi lỗi và file
        /// </summary>
        /// <param name="key">mã lỗi</param>
        /// <param name="value">giá tri (tên ) lỗi</param>
        /// <returns>trạng thái</returns>
        public int WriteFile(string key, string value)
        {
            try
            {
                string text = key + "--" + value + "--" + new DateTime().ToString();

                StreamWriter write = new StreamWriter(_objectFile);
                write.BaseStream.Seek(0, SeekOrigin.End);
                write.WriteLine(Environment.NewLine);
                write.WriteLine(text);
                write.Flush();
                write.Close();
                return 1;
            }
            catch
            {
                return 0;
            }
            finally { _objectFile.Close(); }
        }
        ~LogFileServices()
        {
            _objectFile.Close();
            _objectFile.Dispose();
        }
    }
}
