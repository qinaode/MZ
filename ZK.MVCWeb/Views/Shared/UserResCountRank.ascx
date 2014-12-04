<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="rankTit">
    用户贡献榜</div>
<ul class="rankMenu">
    <li class="current" style="cursor: pointer; width: 159px;">本周积分排行</li>
    <li style="cursor: pointer; width: 159px;">积分总排行</li>
</ul>
<ul class="rankCont">
    <li class="rankRow current">
        <ul class="rankRowCont">
            <li ><span style="width:159px; display:inline-block;text-align:center; font-size:13px;font-family:微软雅黑">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;用户名</span><span  style="width:159px;display:inline-block;text-align:center; font-size:13px;font-family:微软雅黑">经验值</span>
            </li>
            <%Dictionary<string, int> dicTotalCountWeek = (Dictionary<string, int>)ViewData["dicForCountWeek"];
              int m = 0;
              foreach (var item in dicTotalCountWeek)
              {
                  m++;
                                 
            %>
            <li class="rankRowContLi">
                <div class="No">
                    <%=m %></div>
                <div>
                    <span  style="width:146px; display:inline-block; text-align:center; font-size:12px;font-family:@微软雅黑"> 
                        <%=item.Key %></span><span  style="width:146px; display:inline-block; text-align:center; font-size:12px;font-family:@微软雅黑"><%=item.Value %></span></div>
                        </li>
                <% }
                %>
        </ul>
    </li>
    <li class="rankRow">
        <ul class="rankRowCont">
            <li ><span style="width:159px; display:inline-block;text-align:center; font-size:13px;font-family:微软雅黑">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;用户名</span><span  style="width:159px;display:inline-block;text-align:center; font-size:13px;font-family:微软雅黑" >经验值</span>
            </li>
            <%Dictionary<string, string> dicTotalCount = (Dictionary<string, string>)ViewData["dicForCount"];
              int l = 0;
              foreach (var item in dicTotalCount)
              {
                  l++;
                                  
            %>
            <li class="rankRowContLi">
                <div class="No">
                    <%=l %></div>
                <div >
                    <span style="width:146px; display:inline-block; text-align:center; font-size:12px;font-family:@微软雅黑">
                        <%=item.Key %></span><span class="type"  style="width:146px; display:inline-block; text-align:center; font-size:12px;font-family:@微软雅黑"><%=item.Value %></span></div>
            </li>
            <%}
                              
            %>
        </ul>
    </li>
</ul>
