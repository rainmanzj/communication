using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;
using Cdo.Comon.Tool;


namespace UpLoadWebApplication
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected int progress = 0;
        protected void Button1_Click(object sender, EventArgs e)
        {
            //UpLoadService.UpLoadServiceClient ser = new UpLoadService.UpLoadServiceClient();
            //System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            //ser.UploadFile(FileUpload1.PostedFile.FileName, "pppp", stream);
            //UpLoadService.FileUploadMessage request = new UpLoadService.FileUploadMessage();
            //Console.WriteLine(FileUpload1);
            BatchRequest batchRequest = new BatchRequest();
            byte[] bytes = new byte[FileUpload1.FileContent.Length];

            FileUpload1.FileContent.Read(bytes, 0, bytes.Length);

            batchRequest.SendBuffer(bytes, FileUpload1.FileName, 1024);
            //UploadArr(FileUpload1.FileContent, FileUpload1.FileName,1024);
            //request.FileName = FileUpload1.FileName;
            //request.SavePath = "ooooooo";
            //request.Tranter = 6;
            //request.FileData = FileUpload1.FileContent;
            //request.length = 1024 * 1024;

            //UpLoadService.IUpLoadService channel = ser.ChannelFactory.CreateChannel();
            //channel.UploadFile(request);
        }
        public void TransferUpload(long iPkgIdx,List<long> PkgList, Stream fileStream, long Tranters, string uploadFileName,int uploadFileSize)
        {
            UpLoadService.UpLoadServiceClient ser = new UpLoadService.UpLoadServiceClient();
            UpLoadService.FileUploadMessage request = new UpLoadService.FileUploadMessage();
            UpLoadService.IUpLoadService channel = ser.ChannelFactory.CreateChannel();

            long bufferSize = PkgList[(int)iPkgIdx];
            byte[] buffer = new byte[bufferSize];
            int bytesRead = fileStream.Read(buffer, 0, (int)bufferSize);
            request.Tranter = iPkgIdx + 1;
            request.Tranters = Tranters;
            request.FileName = uploadFileName;
            request.length = uploadFileSize;
            request.FileData = new MemoryStream(buffer);
            UpLoadService.FileReturnMessage data = channel.UploadFile(request);
            progress = (int)data.Tranter * 100 / (int)data.Tranters;
            Console.Write(this);
            //this.UpdatePanel1.Update();
            if (data.IsOk)
            {
                TransferUpload(iPkgIdx + 1,PkgList,fileStream,Tranters,uploadFileName,uploadFileSize);
            }
            else
            {
                progress = 0;
            }
        }
        public void UploadArr(Stream uploadFileStream, string uploadFileName, int uploadFileSize)
        {
            Stream fileStream = null;
            try
            {
                using (fileStream = uploadFileStream)
                {


                    UpLoadService.UpLoadServiceClient ser = new UpLoadService.UpLoadServiceClient();
                    UpLoadService.FileUploadMessage request = new UpLoadService.FileUploadMessage();
                    UpLoadService.IUpLoadService channel = ser.ChannelFactory.CreateChannel();



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
                    //for (long iPkgIdx = 0; iPkgIdx < PkgList.Count; iPkgIdx++)
                    //{
                        //long bufferSize = PkgList[(int)iPkgIdx];
                        //byte[] buffer = new byte[bufferSize];
                        //int bytesRead = fileStream.Read(buffer, 0, (int)bufferSize);
                        //request.Tranter = iPkgIdx + 1;
                        //request.Tranters = PkgList.Count;
                        //request.FileName = uploadFileName;
                        //request.length = uploadFileSize;
                        //request.FileData = new MemoryStream(buffer);
                        //UpLoadService.FileReturnMessage data = channel.UploadFile(request);
                        //Console.Write(data);
                    //}
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
