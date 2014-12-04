using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZK.Manage.ashx
{
    /// <summary>
    /// AdmintrationMag 的摘要说明
    /// </summary>
    public class AdmintrationMag : IHttpHandler
    {
        ZK.BLL.ZK_ChannelGroup chanelGroupbll = new BLL.ZK_ChannelGroup();
        ZK.Model.ZK_ChannelGroup chanelGroupmdl = new ZK.Model.ZK_ChannelGroup();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string Rezult = string.Empty;
            string strSQL=string.Empty;

            int id = Convert.ToInt32(context.Request.Form["ID"]);
            chanelGroupmdl = chanelGroupbll.GetModel(id);

            int depOrder = Convert.ToInt32(chanelGroupmdl.channelGroupLevel);
            int depParentid = Convert.ToInt32(chanelGroupmdl.channelGroupParent);
            
             strSQL = "channelGroupParent=" + depParentid + " And " + "channelGroupLevel<" + depOrder + " Order by channelGroupLevel";
            

            System.Data.DataSet ds = chanelGroupbll.GetList(strSQL);

            List<ZK.Model.ZK_ChannelGroup> depList = new List<Model.ZK_ChannelGroup>();
            depList=chanelGroupbll.DataTableToList(ds.Tables[0]);

            int upid = Convert.ToInt32(depList[depList.Count - 1].channelGroupLevel);

            int upOrgid = depList[depList.Count - 1].channelGroupID;
            ZK.Model.ZK_ChannelGroup depmdlB = new Model.ZK_ChannelGroup();
            depmdlB = chanelGroupbll.GetModel(upOrgid);

            ZK.Model.ZK_ChannelGroup depmdl1= new Model.ZK_ChannelGroup();
            ZK.Model.ZK_ChannelGroup depmdl2 = new Model.ZK_ChannelGroup();

            depmdl1.channelGroupID = chanelGroupmdl.channelGroupID;
            depmdl1.channelGroupDesc = chanelGroupmdl.channelGroupDesc;
            depmdl1.channelGroupLevel = upid;
            depmdl1.channelGroupName = chanelGroupmdl.channelGroupName;
            depmdl1.channelGroupParent = chanelGroupmdl.channelGroupParent;
            depmdl1.channelID = chanelGroupmdl.channelID;

            depmdl2.channelGroupID = depmdlB.channelGroupID;
            depmdl2.channelGroupDesc = depmdlB.channelGroupDesc;
            depmdl2.channelGroupLevel = depOrder;
            depmdl2.channelGroupName = depmdlB.channelGroupName;
            depmdl2.channelGroupParent = depmdlB.channelGroupParent;
            depmdl2.channelID = depmdlB.channelID;

            chanelGroupbll.Update(depmdl1);
            chanelGroupbll.Update(depmdl2);
                                         
            context.Response.Write("Rezult");               
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}