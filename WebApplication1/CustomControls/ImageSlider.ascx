<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageSlider.ascx.cs" Inherits="WebApplication1.CustomControls.ImageSlider" %>
<div id="box">
	<div class="prev"></div>
    <div class="next"></div>
    <asp:Repeater ID="repImages" runat="server">
        <HeaderTemplate>
            <ul class="bigUl">
        </HeaderTemplate>
        <ItemTemplate>
                <li><a href='<%# Eval("NewsPath") %>'> <img src='<%# Eval("NewsImagePath") %>' /></a></li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
<%--	<ul class="bigUl">
		<li style="z-index:1"><a href="#"><img src="images/1.jpg" /></a></li>
		<li><a href="#"><img src="images/2.jpg" /></a></li>
		<li><a href="#"><img src="images/1.jpg" /></a></li>
		<li><a href="#"><img src="images/2.jpg" /></a></li>
		<li><a href="#"><img src="images/1.jpg" /></a></li>
		<li><a href="#"><img src="images/2.jpg" /></a></li>
		<li><a href="#"><img src="images/1.jpg" /></a></li>
	</ul>--%>
	<ul class="numberUl">
		<li class="night"><a href="javascript:;">1</a></li>
		<li><a href="javascript:;">2</a></li>
		<li><a href="javascript:;">3</a></li>
		<li><a href="javascript:;">4</a></li>
		<li><a href="javascript:;">5</a></li>
		<li><a href="javascript:;">6</a></li>
		<li><a href="javascript:;">7</a></li>
	</ul>
	<div>
       <asp:Repeater ID="repTexts" runat="server">
            <HeaderTemplate>
                <ul class="textUl">
            </HeaderTemplate>
            <ItemTemplate>
                    <li><a href='<%# Eval("NewsPath") %>'> <%# Eval("NewsTitle").ToString().Length > 18 ? Eval("NewsTitle").ToString().Substring(0, 16)+"..." : Eval("NewsTitle").ToString() %> </a></li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
		<%--<ul class="textUl">
		<li style="display:block;"><a href="#">这里是文字这里是文字这里是文字文字</a></li>
		<li><a href="#">这里是文字这里是文字这里是文</a></li>
		<li><a href="#">这里是文字这里是文字这里是文字文字</a></li>
		<li><a href="#">这里是文字这里是文字这里是文</a></li>
		<li><a href="#">这里是文字这里是文字这里是文字文字</a></li>
		<li><a href="#">这里是文字这里是文字这里是文</a></li>
		<li><a href="#">这里是文字这里是文字这里是文字</a></li>
		</ul>--%>
	</div>
</div>