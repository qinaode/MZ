using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZK.Common;
using System.Data;
using System.Xml;
using System.IO;

namespace ZK.Manage.MoralManagement
{
    public partial class ManagementPpt : System.Web.UI.Page
    {
        static string XMLFilePath = "/Files/Moral/Images.xml";
        //static string XMLFilePath = "/files/bfiles/Moral/Images.xml";
        static string XMLNodePath = "/Images/Moral";
        static string nodePath = "/Images/Moral/img";
        int pagesize = 15;
        int totalcount = 0;
        #region 页面加载数据绑定
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPptImg();
                if (!string.IsNullOrEmpty(Request.QueryString["checkedlist"]))
                {
                    DeleteList();
                }
            }
        }

        #endregion

        #region 绑定数据到Repeater
        /// <summary>
        /// 绑定数据到Repeater
        /// </summary>
        private void BindPptImg()
        {
            Repeater1.DataSource = LoadData();
            Repeater1.DataBind();
            //AspNetPager1.RecordCount = LoadData().Rows.Count;
        }
        #endregion

        #region 加载数据并带有分类
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        protected DataTable LoadData()
        {//拼一个表单
            DataTable dt = new DataTable();
            DataColumn[] columns ={
                                    new DataColumn ("pic_name"),
                                    new DataColumn("pic_path"),
                                    new DataColumn("pic_state"),
                                    new DataColumn("pic_order"),
                                    new DataColumn("pic_link"),
                                    new DataColumn("pic_action"),
                                    new DataColumn("pic_id")
                                  };

            dt.Columns.AddRange(columns);
            //得到XML节点的列表
            string xmlpath = Server.MapPath(XMLFilePath);
            XmlNodeList nodelist = XMLHelper.GetXmlNodeListByXpath(xmlpath, nodePath);
            foreach (XmlNode item in nodelist)
            {
                DataRow dr = dt.NewRow();
                dr["pic_name"] = item.Attributes["pic_name"].Value;
                dr["pic_path"] = item.InnerText;
                //if (item.Attributes["pic_state"].Value == "1")
                //{
                //    dr["pic_state"] = "未启用";

                //}
                //else
                //{
                //    dr["pic_state"] = "启用";
                //}
                dr["pic_state"] = item.Attributes["pic_state"].Value;
                dr["pic_order"] = item.Attributes["pic_order"].Value;
                dr["pic_link"] = item.Attributes["pic_link"].Value;
                dr["pic_id"] = item.Attributes["pic_id"].Value;
                dt.Rows.Add(dr);
            }
            totalcount = dt.Rows.Count;
            AspNetPager1.RecordCount = totalcount;
            AspNetPager1.PageSize = pagesize;

            if (pagesize * (AspNetPager1.CurrentPageIndex - 1) > 0)
            {
                for (int i = 0; i < pagesize * (AspNetPager1.CurrentPageIndex - 1); i++)
                {
                    dt.Rows.RemoveAt(0);
                }
            }

            if (dt.Rows.Count > pagesize)
            {
                while (true)
                {
                    if (dt.Rows.Count > pagesize)
                    {
                        dt.Rows.RemoveAt(pagesize);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return dt;
        }
        #endregion
        #region 分页控件
        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            Repeater1.DataSource = LoadData();
            Repeater1.DataBind();
        }
        #endregion

        #region 搜索页面
        /// <summary>
        /// 搜索页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string txtpic_name = txtMoralPicName.Text;
            if (String.IsNullOrEmpty(txtpic_name))
            {
                MessageBox.Show(this, "请输入图片名");

            }
            else
            {
                string xmlpath = Server.MapPath(XMLFilePath);
                string nodePath = "/Images/Moral/img";
                XmlNodeList nodelist = XMLHelper.GetXmlNodeListByXpath(xmlpath, nodePath);
                //List<XmlNode> lists = XMLHelper.GetXmlNodeListByXpathAndAttr(xmlpath, nodePath, "Index", txtpic_name);
                List<XmlNode> lists = XMLHelper.GetXmlNodeListByXpathAndAttr(xmlpath, nodePath, "pic_name", txtpic_name);
                DataTable dt = new DataTable();
                DataColumn[] columns ={
                                    new DataColumn ("pic_name"),
                                    new DataColumn("pic_path"),
                                    new DataColumn("pic_state"),
                                    new DataColumn("pic_order"),
                                    new DataColumn("pic_link"),
                                    new DataColumn("pic_action"),
                                    new DataColumn("pic_id")
                                  };

                dt.Columns.AddRange(columns);
                foreach (XmlNode item in lists)
                {
                    DataRow dr = dt.NewRow();
                    dr["pic_name"] = item.Attributes["pic_name"].Value;
                    dr["pic_path"] = item.InnerText;
                    //if (item.Attributes["pic_state"].Value == "1")
                    //{
                    //    dr["pic_state"] = "未启用";
                    //}
                    //else
                    //{
                    //    dr["pic_state"] = "未启用";
                    //}
                    dr["pic_state"] = item.Attributes["pic_state"].Value;
                    dr["pic_order"] = item.Attributes["pic_order"].Value;
                    dr["pic_link"] = item.Attributes["pic_link"].Value;
                    dr["pic_id"] = item.Attributes["pic_id"].Value;
                    dt.Rows.Add(dr);
                }
                Repeater1.DataSource = dt.DefaultView;
                Repeater1.DataBind();

            }
        }
        #endregion

        #region 上传幻灯片图片时候的判断，上传的操作在upload()中实现
        /// <summary>
        /// 上传幻灯片图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string imgLink = txtAddLink.Text;
            string imag = txt_Moralppt.Value;
            if (imag == string.Empty)
            {
                MessageBox.Show(this, "请先添加图片！");
                return;
            }
            if (imgLink == string.Empty)
            {
                MessageBox.Show(this, "链接地址不能为空！");
                return;
            }
            if (imgLink != "" && imgLink != "")
            {
                UpLoad();
            }
        }

        #endregion
        #region 操作的事件
        /// <summary>
        /// 操作的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void actionItem_Commond(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CN_btn_Delete")
            {
                string pic_id = e.CommandArgument.ToString();
                Delect("pic_id", pic_id);
            }
            if (e.CommandName == "CN_btn_MoveUp")
            {
                string id = e.CommandArgument.ToString();
                Move(id, "Up");
                BindPptImg();

            }
            if (e.CommandName == "CN_btn_MoveDown")
            {
                string id = e.CommandArgument.ToString();
                Move(id, "Down");
                BindPptImg();
            }
            if (e.CommandName == "CN_btn_State")
            {
                string id = e.CommandArgument.ToString();
                ChangeState(id);
                BindPptImg();
            }

        }

        #endregion






        #region 改变状态的方法

        private void ChangeState(string id)
        {
            string xmlpath = Server.MapPath(XMLFilePath);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlpath);
            XmlNodeList nodelist = xmlDoc.SelectNodes(nodePath);
            //XmlNodeList nodelist = XMLHelper.GetXmlNodeListByXpath(xmlpath, nodePath);
            for (int i = 0; i < nodelist.Count; i++)
            {
                if (nodelist[i].Attributes["pic_id"].Value == id)
                {
                    if (nodelist[i].Attributes["pic_state"].Value == "1")
                    {
                        nodelist[i].Attributes["pic_state"].Value = "0";
                    }
                    else
                    {
                        nodelist[i].Attributes["pic_state"].Value = "1";

                    }
                    xmlDoc.Save(xmlpath);
                    break;
                }

            }
            BindPptImg();
        }
        #endregion
        #region 操作的行为
        private void Move(string id, string action)
        {
            string xmlpath = Server.MapPath(XMLFilePath);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlpath);
            XmlNodeList nodelist = xmlDoc.SelectNodes(nodePath);
            string pic_id = id;
            int k = 0;
            if (action.ToLower() == "up")
            {
                k = -1;
            }
            else
            {
                k = 1;
            }
            for (int i = 0; i < nodelist.Count; i++)
            {
                if (nodelist[i].Attributes["pic_id"].Value == pic_id)
                {
                    if (i == 0 && k == -1)//上移且是第一个则返回
                    {
                        return;
                    }
                    else//是第一个但是是下移，不是第一个但是是上移，不是第一个的上下移动(最后一个的上移，下移)
                    {
                        if (i == nodelist.Count - 1 && k == 1)
                        {
                            return;
                        }
                        List<string> textList = new List<string> { nodelist[i].InnerText, nodelist[i + k].InnerText };

                        for (int j = 0; j < nodelist[i].Attributes.Count; j++)
                        {
                            List<string> list = new List<string> { nodelist[i].Attributes[j].Value, 
                                                                    nodelist[i + k].Attributes[j].Value};
                            if (j == 0)
                            {
                                continue;
                            }
                            else
                            {
                                nodelist[i].Attributes[j].Value = list[1];
                                // nodelist[i].InnerText = list[3];
                                nodelist[i + k].Attributes[j].Value = list[0];
                                // nodelist[i + k].InnerText = list[1];
                            }
                        }
                        nodelist[i].InnerText = textList[1];
                        nodelist[i + k].InnerText = textList[0];
                        xmlDoc.Save(xmlpath);
                        break;
                    }
                }

            }
        }
        #endregion



        #region 按照id单个删除。在删除的时候将各个节点的属性【Index】进行重新修改，排序修改为1,2,3，....
        /// <summary>
        /// 按照id单个删除。在删除的时候将各个节点的属性【Index】进行重新修改，排序修改为1,2,3，....
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pic_id"></param>
        private void Delect(string id, string pic_id)
        {
            string xmlpath = Server.MapPath(XMLFilePath);
            //删除文件
            string filePath = "";
            XmlNodeList nodelist1 = XMLHelper.GetXmlNodeListByXpath(xmlpath, nodePath);

            foreach (XmlNode node in nodelist1)
            {

                if (pic_id.Equals(node.Attributes["pic_id"].Value.ToString()))
                {
                    filePath = node.InnerText;
                    break;
                }
            }
            string Path = Server.MapPath(filePath);

            File.Delete(Path);
            //删除XML节点

            bool flag = XMLHelper.DeleteXmlNodeByXPath(xmlpath, nodePath, id, pic_id);
            //为其重新排序
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlpath);
            XmlNodeList nodelist = xmlDoc.SelectNodes(nodePath);
            int i = 1;
            int NIndex = nodelist.Count;
            foreach (XmlNode node in nodelist)
            {
                node.Attributes["Index"].Value = i.ToString();
                i++;
            }
            xmlDoc.Save(xmlpath); //保存到XML文档



            if (flag == true)
            {
                MessageBox.Show(this, "删除成功！");
            }
            else
            {
                MessageBox.Show(this, "删除失败！");
            }
            BindPptImg();
        }
        #endregion

        #region 删除列表
        /// <summary>
        /// 删除列表
        /// </summary>
        private void DeleteList()
        {
            string strDelList = Request.QueryString["checkedlist"];
            string xmlpath = Server.MapPath(XMLFilePath);
            string[] strid = strDelList.Split(',');

            for (int i = 0; i < strid.Length; i++)
            {
                delPath(strid[i]);
                bool flag = XMLHelper.DeleteXmlNodeByXPath(xmlpath, nodePath, "pic_id", strid[i]);

            }


            BindPptImg();
        }
        #endregion

        #region  上传数据
        /// <summary>
        /// 上传数据
        /// </summary>
        private void UpLoad()
        {
            // string re_filename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + new Guid();
            string re_filename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            //全名
            string filename = this.txt_Moralppt.PostedFile.FileName;
            //获得后缀名
            int index = filename.LastIndexOf(".");
            string fileExtendName = filename.Substring(index);
            //为图片新命名
            string saveFileName = re_filename + fileExtendName;
            //保存路径（包括文件名和后缀名）
            //string savePath = "/Files/bfiles/Moral/pptImage/" + saveFileName;
            string savePath = "/Files/Moral/pptImage/" + saveFileName;
            //XML文件存放的物理路径
            string xmlpath = Server.MapPath(XMLFilePath);
            //启用为0，不启用为1，默认不启用,所以pic_staticpic_order 默认为0
            List<string> nodeAttr = new List<string>();
            nodeAttr.Add("Index");
            nodeAttr.Add("pic_id");
            nodeAttr.Add("pic_name");
            nodeAttr.Add("pic_order");
            nodeAttr.Add("pic_state");
            nodeAttr.Add("pic_link");
            nodeAttr.Add("pic_index");


            string pic_id = re_filename;
            string pic_name = filename;
            string pic_order = "0";
            string pic_state = "1";
            string pic_link = txtAddLink.Text;
            string pic_index = filename.Remove(index);
            List<string> node = new List<string>();

            //不带后缀名的路径
            // node.Add(ZK.Common.CommonFunction.GetPathWithoutExtension(pic_name));
            //获得XML中img的节点数，然后计数+1得到即将插入的节点的pic_index
            XmlNodeList nodelist = XMLHelper.GetXmlNodeListByXpath(xmlpath, nodePath);
            string Index = (nodelist.Count + 1).ToString();
            node.Add(Index);
            node.Add(pic_id);
            node.Add(pic_name);
            node.Add(pic_order);
            node.Add(pic_state);
            node.Add(pic_link);
            node.Add(pic_index);
            //node.Add(savePath);
            //创建节点
            bool res_xml = XMLHelper.CreateXmlNodeByXPath(xmlpath, XMLNodePath, "img", savePath, nodeAttr, node);
            this.txt_Moralppt.PostedFile.SaveAs(Server.MapPath(savePath));
            if (res_xml == true)
            {
                MessageBox.Show(this, "上传和添加链接成功");
            }
            else
            {
                MessageBox.Show(this, "上传和添加链接成功");
            }

            BindPptImg();
        }
        #endregion


        #region 添加图片链接
        /// <summary>
        /// 添加图片链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImglink_Click(object sender, EventArgs e)
        {
            string imgLink = txtAddLink.Text;
            string imag = txt_Moralppt.Value;
            if (imag == string.Empty)
            {
                MessageBox.Show(this, "请先添加图片！");
                return;
            }
            if (imgLink == string.Empty)
            {
                MessageBox.Show(this, "链接地址不能为空！");
                return;
            }
            if (imgLink != "" && imgLink != "")
            {
                UpLoad();

            }
        }
        #region 删除图片文件
        private void delPath(string pic_id)
        {
            string xmlpath = Server.MapPath(XMLFilePath);
            string filePath = "";
            XmlNodeList nodelist1 = XMLHelper.GetXmlNodeListByXpath(xmlpath, nodePath);
            foreach (XmlNode node in nodelist1)
            {
                if (pic_id.Equals(node.Attributes["pic_id"].Value.ToString()))
                {
                    filePath = node.InnerText;
                    break;
                }
            }
            string Path = Server.MapPath(filePath);
            File.Delete(Path);
        }
        #endregion

        #endregion
    }
}