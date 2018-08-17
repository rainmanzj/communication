using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace UploadWcfService
{
    //[ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    // 注意: 如果更改此处的类名 "UpLoadService"，也必须更新 Web.config 中对 "UpLoadService" 的引用。
    public class UpLoadService : IUpLoadService
    {
        //[OperationContract]
        //[WebInvoke(Method ="GET", ResponseFormat =WebMessageFormat.Json)]
        public FileReturnMessage UploadFile(FileUploadMessage request)
        {
            bool IsOk = true;
            string uploadFolder = @"C:\kkk\";
            string savaPath = request.SavePath;
            string dateString = DateTime.Now.ToShortDateString() + @"\";
            string fileName = request.FileName;
            long tranter = request.Tranter;
            long tranters = request.Tranters;
            int length = request.length;
            string fileNamePath = "";
            if (fileName.Contains("."))
            {
                fileNamePath = fileName.Substring(0, fileName.LastIndexOf("."));
            }
            else
            {
                fileNamePath = fileName;
            }
            Stream sourceStream = request.FileData;
            FileStream targetStream = null;
           
            if (!sourceStream.CanRead)
            {
                IsOk = false;
                throw new Exception("数据流不可读!");
            }
            if (savaPath == null) savaPath = @"Photo\";
            if (!savaPath.EndsWith("\\")) savaPath += "\\";

            uploadFolder = uploadFolder + savaPath + fileNamePath;
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string filePath = Path.Combine(uploadFolder, fileName);
            using (targetStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                //targetStream.Write(Buffer, 0, length);
                //read from the input stream in 4K chunks
                //and save to output stream
                const int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                {
                    targetStream.Write(buffer, 0, count);
                }
                targetStream.Close();
                sourceStream.Close();
            }
            if(tranter == tranters)
            {
                string Str1 = File.ReadAllText(filePath);
                return new FileReturnMessage { IsOk = !IsOk, FileName = fileName, MsgData = Str1, Tranter = tranter, Tranters = tranters };
            }
            return new FileReturnMessage { IsOk = IsOk, FileName = fileName, MsgData = "上传成功", Tranter = tranter, Tranters = tranters };
        }

    }
}
