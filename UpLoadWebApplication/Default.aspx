<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UpLoadWebApplication._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
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
        <!-- <asp:Label ID="Label1" runat="server" Text="Label" /> -->
    
    </div>
    </form>
</body>
</html>
