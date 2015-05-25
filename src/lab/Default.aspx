<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="EEVirtualLab.EEVirtualLab" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>IITD EE Virtual Lab</title>
    <script type="text/javascript" src="script.js"></script>
    <link rel="stylesheet" href="style.css" type="text/css" media="screen" />
    <!--[if IE 6]><link rel="stylesheet" href="style.ie6.css" type="text/css" media="screen" /><![endif]-->
    <!--[if IE 7]><link rel="stylesheet" href="style.ie7.css" type="text/css" media="screen" /><![endif]-->
</head>
<body>
    <form id="form1" runat="server">
    <div id="art-main">
        <div class="art-Sheet">
            <div class="art-Sheet-tl">
            </div>
            <div class="art-Sheet-tr">
            </div>
            <div class="art-Sheet-bl">
            </div>
            <div class="art-Sheet-br">
            </div>
            <div class="art-Sheet-tc">
            </div>
            <div class="art-Sheet-bc">
            </div>
            <div class="art-Sheet-cl">
            </div>
            <div class="art-Sheet-cr">
            </div>
            <div class="art-Sheet-cc">
            </div>
            <div class="art-Sheet-body">
                <div class="art-Header">
                    <div class="art-Header-png">
                    </div>
                    <div class="art-Header-jpeg">
                    </div>
                    <div class="art-Logo">
                        <h1 id="name-text" class="art-Logo-name">
                            <a href="#">IIT Delhi Virtual Lab</a></h1>
                        <div id="slogan-text" class="art-Logo-text">
                            Power Electronics and Electric Drives Laboratory</div>
                    </div>
                </div>
                <div class="art-contentLayout">
                    <div class="art-sidebar1">
                        <div class="art-Block">
                            <div class="art-Block-tl">
                            </div>
                            <div class="art-Block-tr">
                            </div>
                            <div class="art-Block-bl">
                            </div>
                            <div class="art-Block-br">
                            </div>
                            <div class="art-Block-tc">
                            </div>
                            <div class="art-Block-bc">
                            </div>
                            <div class="art-Block-cl">
                            </div>
                            <div class="art-Block-cr">
                            </div>
                            <div class="art-Block-cc">
                            </div>
                            <div class="art-Block-body">
                                <div class="art-BlockHeader">
                                    <div class="art-header-tag-icon">
                                        <div class="t">
                                            Experiment</div>
                                    </div>
                                </div>
                                <div class="art-BlockContent">
                                    <div class="art-BlockContent-body">
                                        <div>
                                            <asp:DropDownList ID="lstExperiments" runat="server" Width="95%" DataTextField="Name"
                                                AutoPostBack="True" Enabled="False">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="cleared">
                                        </div>
                                    </div>
                                </div>
                                <div class="cleared">
                                </div>
                            </div>
                        </div>
                        <div class="art-Block">
                            <div class="art-Block-tl">
                            </div>
                            <div class="art-Block-tr">
                            </div>
                            <div class="art-Block-bl">
                            </div>
                            <div class="art-Block-br">
                            </div>
                            <div class="art-Block-tc">
                            </div>
                            <div class="art-Block-bc">
                            </div>
                            <div class="art-Block-cl">
                            </div>
                            <div class="art-Block-cr">
                            </div>
                            <div class="art-Block-cc">
                            </div>
                            <div class="art-Block-body">
                                <div class="art-BlockHeader">
                                    <div class="art-header-tag-icon">
                                        <div class="t">
                                            Select Parameters</div>
                                    </div>
                                </div>
                                <div class="art-BlockContent">
                                    <div class="art-BlockContent-body">
                                        <div>
                                            <table width="95%">
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="dgParameters" runat="server" CellPadding="4" Width="100%" AutoGenerateColumns="False"
                                                            ForeColor="#333333" GridLines="None">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField DataField="Display_Name" HeaderText="Parameter" ItemStyle-Width="50%" />
                                                                <asp:BoundField DataField="Variable_Name_In_PSIM" Visible="true"  ItemStyle-Width="25%" />
                                                                <asp:TemplateField HeaderText="Value" ItemStyle-Width="150%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox1" Text='<%# DataBinder.Eval(Container.DataItem, "Value") %>'
                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Validation_Message" HeaderText=" Message" ItemStyle-ForeColor="Red"  ItemStyle-Width="5%" />
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="dgOutput" runat="server" CellPadding="4" Width="95%" AutoGenerateColumns="False"
                                                            ForeColor="#333333" GridLines="None">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField DataField="Display_Name" HeaderText="Output Variable" ItemStyle-Width="50%" />
                                                                 <asp:TemplateField HeaderText="Graph" ItemStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:Dropdownlist ID="lstGraphs" runat="server" Width="80%" AutoPostBack="True" Text='<%# DataBinder.Eval(Container.DataItem, "Value") %>'>
                                                                                <asp:ListItem Text = "Graph 1" Value = "Graph 1" />
                                                                                <asp:ListItem Text = "Graph 2" Value = "Graph 2" />
                                                                                <asp:ListItem Text = "Graph 3" Value = "Graph 3" /> 
                                                                            </asp:Dropdownlist>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="cleared">
                                                        </div>
                                                        <div class="cleared">
                                                        </div>
                                                        <table width="85%">
                                                            <tr align="center">
                                                                <td align="center">
                                                                    <span class="art-button-wrapper"><span class="l"></span><span class="r"></span>
                                                                        <asp:Button class="art-button" ID="btnRunSimulation" runat="server" Text="Run Simulation" />
                                                                    </span>
                                                                </td>
                                                                <td align="center">
                                                                    <span class="art-button-wrapper"><span class="l"></span><span class="r"></span>
                                                                        <asp:Button class="art-button" ID="btnSaveResults" runat="server" Text="Save Results" />
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="cleared">
                                </div>
                            </div>
                        </div>
                        <div class="art-Block">
                            <div class="art-Block-tl">
                            </div>
                            <div class="art-Block-tr">
                            </div>
                            <div class="art-Block-bl">
                            </div>
                            <div class="art-Block-br">
                            </div>
                            <div class="art-Block-tc">
                            </div>
                            <div class="art-Block-bc">
                            </div>
                            <div class="art-Block-cl">
                            </div>
                            <div class="art-Block-cr">
                            </div>
                            <div class="art-Block-cc">
                            </div>
                            <div class="art-Block-body">
                                <div class="art-BlockHeader">
                                    <div class="art-header-tag-icon">
                                        <div class="t">
                                            Developed By</div>
                                    </div>
                                </div>
                                <div class="art-BlockContent">
                                    <div class="art-BlockContent-body">
                                        <div>
                                            <img src="images/contact.jpg" alt="an image" style="margin: 0 auto; display: block;
                                                width: 95%" />
                                            <br />
                                            <b>Wizda Solutions.</b><br />
                                            Mumbai<br />
                                            <br />
                                            Sriram Krishnamoorthy<br />
                                            Email: <a href="mailto:info@company.com">sriram@wizda.in</a><br />
                                            Phone: +91-7208061443
                                            <br />
                                            <br />
                                            <br />
                                            &nbsp;</div>
                                        <div class="cleared">
                                        </div>
                                    </div>
                                </div>
                                <div class="cleared">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="art-content">
                        <div class="art-Post">
                            <div class="art-Post-body">
                                <div class="art-Post-inner">
                                    <h4  title ="hdrChartCircuit" class="art-PostHeader">
                                        <img src="images/PostHeaderIcon.png" width="26" height="26" alt="PostHeaderIcon" />
                                        Circuit Diagram
                                    </h4>
                                    <div class="art-PosztContent">
                                        <asp:Chart ID="ChartCircuit" runat="server" Height="293px" Width="620px">
                                            <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        </asp:Chart>
                                    </div>
                                    <div class="cleared">
                                    </div>
                                </div>
                                <div class="cleared">
                                </div>
                            </div>
                        </div>
                        <div class="art-Post">
                            <div class="art-Post-body">
                                <div class="art-Post-inner">
                                    <h2 class="art-PostHeader">
                                        <img src="images/PostHeaderIcon.png" width="26" height="26" alt="PostHeaderIcon" />
                                        Output Graph 1</h2>
                                    <div class="art-PostContent">
                                        <asp:Chart ID="Chart1" runat="server" Height="293px" Width="620px">
                                            <Legends>
                                                <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                </asp:Legend>
                                            </Legends>
                                            <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                            <Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                    BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                                                    BackGradientStyle="TopBottom">
                                                    <AxisY LineColor="64, 64, 64, 64">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                                    </AxisY>
                                                    <AxisX LineColor="64, 64, 64, 64">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                                    </AxisX>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                    </div>
                                    <div class="cleared">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="art-Post">
                            <div class="art-Post-body">
                                <div class="art-Post-inner">
                                    <h2 class="art-PostHeader">
                                        <img src="images/PostHeaderIcon.png" width="26" height="26" alt="PostHeaderIcon" />
                                        Output Graph 2</h2>
                                    <div class="art-PostContent">
                                        <asp:Chart ID="Chart2" runat="server" Height="293px" Width="620px">
                                            <Legends>
                                                <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                </asp:Legend>
                                            </Legends>
                                            <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                            <Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea2" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                    BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                                                    BackGradientStyle="TopBottom">
                                                    <AxisY LineColor="64, 64, 64, 64">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                                    </AxisY>
                                                    <AxisX LineColor="64, 64, 64, 64">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                                    </AxisX>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                    </div>
                                    <div class="cleared">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="art-Post">
                            <div class="art-Post-body">
                                <div class="art-Post-inner">
                                    <h2 class="art-PostHeader">
                                        <img src="images/PostHeaderIcon.png" width="26" height="26" alt="PostHeaderIcon" />
                                        Output Graph 3</h2>
                                    <div class="art-PostContent">
                                        <asp:Chart ID="Chart3" runat="server" Height="293px" Width="620px">
                                            <Legends>
                                                <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                </asp:Legend>
                                            </Legends>
                                            <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                            <Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea3" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                    BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                                                    BackGradientStyle="TopBottom">
                                                    <AxisY LineColor="64, 64, 64, 64">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                                    </AxisY>
                                                    <AxisX LineColor="64, 64, 64, 64">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                                    </AxisX>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                    </div>
                                    <div class="cleared">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="cleared">
                </div>
                <div class="art-Footer">
                    <div class="art-Footer-inner">
                        <a href="#" class="art-rss-tag-icon" title="RSS"></a>
                        <div class="art-Footer-text">
                            <p>
                                Copyright &copy; 2011 Indian Institute of Technology, Delhi. All Rights Reserved.</p>
                        </div>
                    </div>
                    <div class="art-Footer-background">
                    </div>
                </div>
                <div class="cleared">
                </div>
            </div>
        </div>
        <div class="cleared">
        </div>
        <p class="art-page-footer">
    </div>
    </form>
</body>
</html>
