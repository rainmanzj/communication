using System;
using System.IO;
using System.Collections.Generic;

namespace FileStreamRead
{
    class FileStreamArr
    {
        public void  UploadArr(Stream uploadFileStream, string uploadFileName,int uploadFileSize)
        {
            Stream fileStream = null;
            try
            {
                using (fileStream = uploadFileStream)
                {


                    UpLoadWebApplication.UpLoadService.UpLoadServiceClient ser = new UpLoadWebApplication.UpLoadService.UpLoadServiceClient();
                    UpLoadWebApplication.UpLoadService.FileUploadMessage request = new UpLoadWebApplication.UpLoadService.FileUploadMessage();
                    UpLoadWebApplication.UpLoadService.IUpLoadService channel = ser.ChannelFactory.CreateChannel();



                    long FileLength = fileStream.Length;
                    List<long> PkgList = new List<long>();
                    long PkgNum = FileLength / Convert.ToInt64(uploadFileSize);
                    for (long iIdx = 0;iIdx < FileLength / Convert.ToInt64(uploadFileSize);iIdx++)
                    {
                        PkgList.Add(Convert.ToInt64(uploadFileSize));
                    }
                    long s = FileLength % Convert.ToInt64(uploadFileSize);
                    if (s != 0)
                    {
                        PkgList.Add(s);
                    }
                    for(long iPkgIdx = 0;iPkgIdx < PkgList.Count; iPkgIdx++)
                    {
                        long bufferSize = PkgList[(int)iPkgIdx];
                        byte[] buffer = new byte[bufferSize];
                        int bytesRead = fileStream.Read(buffer, 0, (int)bufferSize);
                        request.Tranter = iPkgIdx + 1;
                        request.Tranters = PkgList.Count;
                        request.FileName = uploadFileName;
                        request.length = uploadFileSize;
                        request.FileData = new MemoryStream(buffer);
                        UpLoadWebApplication.UpLoadService.FileReturnMessage data = channel.UploadFile(request);
                        Console.Write(data);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }
    }
}



