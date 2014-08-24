<%@ Page language="c#" Codebehind="Warning.aspx.cs" AutoEventWireup="True" Inherits="Herbalife.HelpDesk.UI.Public.Warning" EnableEventValidation="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>提示</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style type="text/css">
			.topline { BORDER-RIGHT: #83c470 1px solid; BORDER-TOP: #83c470 1px solid; BORDER-LEFT: #83c470 1px solid; BORDER-BOTTOM: #83c470 1px solid }
			.boxline { BORDER-RIGHT: #83c470 1px solid; BORDER-TOP: #83c470 0px solid; BORDER-LEFT: #83c470 1px solid; BORDER-BOTTOM: #83c470 1px solid }
			INPUT { BORDER-RIGHT: #83c470 1px solid; BORDER-TOP: #83c470 1px solid; BORDER-LEFT: #83c470 1px solid; BORDER-BOTTOM: #83c470 1px solid }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="760" border="0">
				<tr>
					<td style="HEIGHT: 5px">&nbsp;</td>
					<td style="FONT-WEIGHT: bold; COLOR: #ff0000; LETTER-SPACING: 1em; HEIGHT: 51px" align="center"
						class="topline"><asp:Label ID="lblErrorLevel" Runat="server">警告</asp:Label></td>
					<td style="HEIGHT: 5px">&nbsp;</td>
				</tr>
				<tr>
					<td width="80">&nbsp;</td>
					<td vAlign="top" align="center" width="420">
						<table class="boxline" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td style="HEIGHT: 54px" vAlign="middle" align="center">&nbsp;
									<asp:label id="lblError" runat="server" ForeColor="#0000C0" Font-Size="12px"></asp:label></td>
							</tr>
							<tr>
								<td style="HEIGHT: 33px" align="center">&nbsp; <input id="btnOK" type="button" value=" 确  定 " name="btnOK" runat="server" onserverclick="btnOK_ServerClick">
									<input id="showdetail" type="button" value="显示详细 ▼" name="showdetail" runat="server" onserverclick="showdetail_ServerClick"></td>
							</tr>
							<tr id="detail" runat="server">
								<td vAlign="top" align="center">&nbsp;
									<asp:textbox id="txtMessage" runat="server" Width="400px" TextMode="MultiLine"></asp:textbox></td>
							</tr>
							<tr>
								<td height="14"></td>
							</tr>
						</table>
					</td>
					<td width="260">&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
