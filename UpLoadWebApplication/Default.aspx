<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UpLoadWebApplication._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
  <%--   <script type="text/javascript" language="javascript">
         function SetPorgressBar(pos) {
             //设置进度条居中
             var screenHeight = window["mainWindow"].offsetHeight;
             var screenWidth = window["mainWindow"].offsetWidth;
             ProgressBarSide.style.width = Math.round(screenWidth / 2);
             ProgressBarSide.style.left = Math.round(screenWidth / 4);
             ProgressBarSide.style.top = Math.round(screenHeight / 2);
             ProgressBarSide.style.height = "21px";
             ProgressBarSide.style.display = "";

             //设置进度条百分比                       
             ProgressBar.style.width = pos + "%";
             ProgressText.innerHTML = pos + "%";
         }

         //完成后隐藏进度条
         function SetCompleted() {
             ProgressBarSide.style.display = "none";
         }
    </script>--%>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <!-- <div style="width:200px;border:1px solid #333;height:10px;">
            <div runat="server" style="height:100%;width:<%=this.progress %>%;background-color:red;display:inline-block;"></div>
        </div>
        <span runat="server"><%=this.progress %>%</span><br /> -->
          <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="upload" />
        
   <%--     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">
            <ContentTemplate>
                  当前进度：<asp:Label ID="procestip" runat="server" Text="0"></asp:Label>
	        </ContentTemplate>
        </asp:UpdatePanel>--%>

        <!-- <asp:Label ID="Label1" runat="server" Text="Label" /> -->
    
    </div>

    </form>
</body>
</html>
