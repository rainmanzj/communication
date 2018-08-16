using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdo.Comon.Tool
{
    public class BatchRequest
    {
        protected void Button1_Click(byte[] buffer, string uploadFileName, int uploadFileSize)
        {
            Stream fileStream = null;
            try
            {
                using (fileStream = new MemoryStream(buffer))
                {


                    WcfBatchRequest.UpLoadServiceClient ser = new WcfBatchRequest.UpLoadServiceClient();
                    WcfBatchRequest.FileUploadMessage request = new WcfBatchRequest.FileUploadMessage();
                    WcfBatchRequest.IUpLoadService channel = ser.ChannelFactory.CreateChannel();



                    long FileLength = fileStream.Length;
                    List<long> PkgList = new List<long>();
                    long PkgNum = FileLength / Convert.ToInt64(uploadFileSize);
                    for (long iIdx = 0; iIdx < FileLength / Convert.ToInt64(uploadFileSize); iIdx++)
                    {
                        PkgList.Add(Convert.ToInt64(uploadFileSize));
                    }
                    long s = FileLength % Convert.ToInt64(uploadFileSize);
                    if (s != 0)
                    {
                        PkgList.Add(s);
                    }
                    TransferUpload(0, PkgList, fileStream, PkgList.Count, uploadFileName, uploadFileSize);
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
        public void TransferUpload(long iPkgIdx, List<long> PkgList, Stream fileStream, long Tranters, string uploadFileName, int uploadFileSize)
        {
            WcfBatchRequest.UpLoadServiceClient ser = new WcfBatchRequest.UpLoadServiceClient();
            WcfBatchRequest.FileUploadMessage request = new WcfBatchRequest.FileUploadMessage();
            WcfBatchRequest.IUpLoadService channel = ser.ChannelFactory.CreateChannel();
            long bufferSize = PkgList[(int)iPkgIdx];
            byte[] buffer = new byte[bufferSize];
            int bytesRead = fileStream.Read(buffer, 0, (int)bufferSize);
            request.Tranter = iPkgIdx + 1;
            request.Tranters = Tranters;
            request.FileName = uploadFileName;
            request.length = uploadFileSize;
            request.FileData = new MemoryStream(buffer);
            WcfBatchRequest.FileReturnMessage data = channel.UploadFile(request);
            if (data.IsOk)
            {
                TransferUpload(iPkgIdx + 1, PkgList, fileStream, Tranters, uploadFileName, uploadFileSize);
            }
        }

    }


}
