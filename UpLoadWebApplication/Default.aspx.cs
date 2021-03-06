﻿using System;
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
            beginProgress();
            //for (int i = 1; i <= 100; i++)
            //{
            //    setProgress(i);

            //    //此处用线程休眠代替实际的操作，如加载数据等
            //    System.Threading.Thread.Sleep(50);
            //}
        }

        protected int progress = 0;
        BatchRequest batchRequest = new BatchRequest();
        protected void Button1_Click(object sender, EventArgs e)
        {
            //UpLoadService.UpLoadServiceClient ser = new UpLoadService.UpLoadServiceClient();
            //System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            //ser.UploadFile(FileUpload1.PostedFile.FileName, "pppp", stream);
            //UpLoadService.FileUploadMessage request = new UpLoadService.FileUploadMessage();
            //Console.WriteLine(FileUpload1);
            if (batchRequest == null)
                batchRequest = new BatchRequest();
            byte[] bytes = new byte[FileUpload1.FileContent.Length];

            string Str1 = System.Text.Encoding.Default.GetString(FileUpload1.FileBytes);
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(Str1);

            FileUpload1.FileContent.Read(bytes, 0, bytes.Length);
            batchRequest.OnBatchRequestProcesEvent += batchRequest_BatchRequestProcesEvent;  
            batchRequest.SendBuffer(byteArray, FileUpload1.FileName, 1024000);
            //UploadArr(FileUpload1.FileContent, FileUpload1.FileName,1024);
            //request.FileName = FileUpload1.FileName;
            //request.SavePath = "ooooooo";
            //request.Tranter = 6;
            //request.FileData = FileUpload1.FileContent;
            //request.length = 1024 * 1024;

            //UpLoadService.IUpLoadService channel = ser.ChannelFactory.CreateChannel();
            //channel.UploadFile(request);
        }

        private void beginProgress()
        {
            //根据ProgressBar.htm显示进度条界面
            string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("GB2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
        }
        private void setProgress(int percent)
        {
            string jsBlock = "<script>SetPorgressBar('" + percent.ToString() + "'); </script>";
            Response.Write(jsBlock);
            Response.Flush();
        }

        private void finishProgress()
        {
            string jsBlock = "<script>SetCompleted();</script>";
            Response.Write(jsBlock);
            Response.Flush();
        }
        void batchRequest_BatchRequestProcesEvent(int proces)
        {
            //this.procestip.Text = proces.ToString();
            string jsBlock = "<script>SetPorgressBar('" + proces.ToString() + "'); </script>";
            Response.Write(jsBlock);
            Response.Flush();
            
        }
        //public void TransferUpload(long iPkgIdx,List<long> PkgList, Stream fileStream, long Tranters, string uploadFileName,int uploadFileSize)
        //{
        //    UpLoadService.UpLoadServiceClient ser = new UpLoadService.UpLoadServiceClient();
        //    UpLoadService.FileUploadMessage request = new UpLoadService.FileUploadMessage();
        //    UpLoadService.IUpLoadService channel = ser.ChannelFactory.CreateChannel();

        //    long bufferSize = PkgList[(int)iPkgIdx];
        //    byte[] buffer = new byte[bufferSize];
        //    int bytesRead = fileStream.Read(buffer, 0, (int)bufferSize);
        //    request.Tranter = iPkgIdx + 1;
        //    request.Tranters = Tranters;
        //    request.FileName = uploadFileName;
        //    request.length = uploadFileSize;
        //    request.FileData = new MemoryStream(buffer);
        //    UpLoadService.FileReturnMessage data = channel.UploadFile(request);
        //    progress = (int)data.Tranter * 100 / (int)data.Tranters;
        //    Console.Write(this);
        //    //this.UpdatePanel1.Update();
        //    if (data.IsOk)
        //    {
        //        TransferUpload(iPkgIdx + 1,PkgList,fileStream,Tranters,uploadFileName,uploadFileSize);
        //    }
        //    else
        //    {
        //        progress = 0;
        //    }
        //}
        //public void UploadArr(Stream uploadFileStream, string uploadFileName, int uploadFileSize)
        //{
        //    Stream fileStream = null;
        //    try
        //    {
        //        using (fileStream = uploadFileStream)
        //        {


        //            UpLoadService.UpLoadServiceClient ser = new UpLoadService.UpLoadServiceClient();
        //            UpLoadService.FileUploadMessage request = new UpLoadService.FileUploadMessage();
        //            UpLoadService.IUpLoadService channel = ser.ChannelFactory.CreateChannel();



        //            long FileLength = fileStream.Length;
        //            List<long> PkgList = new List<long>();
        //            long PkgNum = FileLength / Convert.ToInt64(uploadFileSize);
        //            for (long iIdx = 0; iIdx < FileLength / Convert.ToInt64(uploadFileSize); iIdx++)
        //            {
        //                PkgList.Add(Convert.ToInt64(uploadFileSize));
        //            }
        //            long s = FileLength % Convert.ToInt64(uploadFileSize);
        //            if (s != 0)
        //            {
        //                PkgList.Add(s);
        //            }
        //            TransferUpload(0, PkgList, fileStream, PkgList.Count, uploadFileName, uploadFileSize);
        //            //for (long iPkgIdx = 0; iPkgIdx < PkgList.Count; iPkgIdx++)
        //            //{
        //                //long bufferSize = PkgList[(int)iPkgIdx];
        //                //byte[] buffer = new byte[bufferSize];
        //                //int bytesRead = fileStream.Read(buffer, 0, (int)bufferSize);
        //                //request.Tranter = iPkgIdx + 1;
        //                //request.Tranters = PkgList.Count;
        //                //request.FileName = uploadFileName;
        //                //request.length = uploadFileSize;
        //                //request.FileData = new MemoryStream(buffer);
        //                //UpLoadService.FileReturnMessage data = channel.UploadFile(request);
        //                //Console.Write(data);
        //            //}
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        if (fileStream != null)
        //        {
        //            fileStream.Close();
        //        }
        //    }
        //}
    }
}
