/**  智客知识管理平台。
* View_AllFileList.cs
*
* 功 能： N/A
* 类 名： View_AllFileList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/28 13:41:42   N/A    初版
*
* Copyright (c) 2012 BeiJing HaoLian Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：北京浩联教育科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZK.IDAL;
using ZK.DBUtility;//Please add references
namespace ZK.Dal
{
    /// <summary>
    /// 数据访问类:View_AllFileList
    /// </summary>
    public partial class View_AllFileList : IView_AllFileList
    {
        public View_AllFileList()
        { }
        #region  Method



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ZK.Model.View_AllFileList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into View_AllFileList(");
            strSql.Append("aid,id,fileID,fileName,cateID,cateName,fileTypeID,fileTypeName,channelID,channelName,createTime,imageURL,clickNum,isDir,filePath,fileDesc,USERID,USERNAME)");
            strSql.Append(" values (");
            strSql.Append("@aid,@id,@fileID,@fileName,@cateID,@cateName,@fileTypeID,@fileTypeName,@channelID,@channelName,@createTime,@imageURL,@clickNum,@isDir,@filePath,@fileDesc,@USERID,@USERNAME)");
            SqlParameter[] parameters = {
					new SqlParameter("@aid", SqlDbType.BigInt,8),
					new SqlParameter("@id", SqlDbType.BigInt,8),
					new SqlParameter("@fileID", SqlDbType.Int,4),
					new SqlParameter("@fileName", SqlDbType.NVarChar,50),
					new SqlParameter("@cateID", SqlDbType.Int,4),
					new SqlParameter("@cateName", SqlDbType.NVarChar,50),
					new SqlParameter("@fileTypeID", SqlDbType.Int,4),
					new SqlParameter("@fileTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@channelID", SqlDbType.Int,4),
					new SqlParameter("@channelName", SqlDbType.NVarChar,50),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@imageURL", SqlDbType.NVarChar,50),
					new SqlParameter("@clickNum", SqlDbType.Int,4),
					new SqlParameter("@isDir", SqlDbType.Int,4),
					new SqlParameter("@filePath", SqlDbType.NVarChar,100),
					new SqlParameter("@fileDesc", SqlDbType.NVarChar,200),
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@USERNAME", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.aid;
            parameters[1].Value = model.id;
            parameters[2].Value = model.fileID;
            parameters[3].Value = model.fileName;
            parameters[4].Value = model.cateID;
            parameters[5].Value = model.cateName;
            parameters[6].Value = model.fileTypeID;
            parameters[7].Value = model.fileTypeName;
            parameters[8].Value = model.channelID;
            parameters[9].Value = model.channelName;
            parameters[10].Value = model.createTime;
            parameters[11].Value = model.imageURL;
            parameters[12].Value = model.clickNum;
            parameters[13].Value = model.isDir;
            parameters[14].Value = model.filePath;
            parameters[15].Value = model.fileDesc;
            parameters[16].Value = model.USERID;
            parameters[17].Value = model.USERNAME;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZK.Model.View_AllFileList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update View_AllFileList set ");
            strSql.Append("aid=@aid,");
            strSql.Append("id=@id,");
            strSql.Append("fileID=@fileID,");
            strSql.Append("fileName=@fileName,");
            strSql.Append("cateID=@cateID,");
            strSql.Append("cateName=@cateName,");
            strSql.Append("fileTypeID=@fileTypeID,");
            strSql.Append("fileTypeName=@fileTypeName,");
            strSql.Append("channelID=@channelID,");
            strSql.Append("channelName=@channelName,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("imageURL=@imageURL,");
            strSql.Append("clickNum=@clickNum,");
            strSql.Append("isDir=@isDir,");
            strSql.Append("filePath=@filePath,");
            strSql.Append("fileDesc=@fileDesc,");
            strSql.Append("USERID=@USERID,");
            strSql.Append("USERNAME=@USERNAME");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
					new SqlParameter("@aid", SqlDbType.BigInt,8),
					new SqlParameter("@id", SqlDbType.BigInt,8),
					new SqlParameter("@fileID", SqlDbType.Int,4),
					new SqlParameter("@fileName", SqlDbType.NVarChar,50),
					new SqlParameter("@cateID", SqlDbType.Int,4),
					new SqlParameter("@cateName", SqlDbType.NVarChar,50),
					new SqlParameter("@fileTypeID", SqlDbType.Int,4),
					new SqlParameter("@fileTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@channelID", SqlDbType.Int,4),
					new SqlParameter("@channelName", SqlDbType.NVarChar,50),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@imageURL", SqlDbType.NVarChar,50),
					new SqlParameter("@clickNum", SqlDbType.Int,4),
					new SqlParameter("@isDir", SqlDbType.Int,4),
					new SqlParameter("@filePath", SqlDbType.NVarChar,100),
					new SqlParameter("@fileDesc", SqlDbType.NVarChar,200),
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@USERNAME", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.aid;
            parameters[1].Value = model.id;
            parameters[2].Value = model.fileID;
            parameters[3].Value = model.fileName;
            parameters[4].Value = model.cateID;
            parameters[5].Value = model.cateName;
            parameters[6].Value = model.fileTypeID;
            parameters[7].Value = model.fileTypeName;
            parameters[8].Value = model.channelID;
            parameters[9].Value = model.channelName;
            parameters[10].Value = model.createTime;
            parameters[11].Value = model.imageURL;
            parameters[12].Value = model.clickNum;
            parameters[13].Value = model.isDir;
            parameters[14].Value = model.filePath;
            parameters[15].Value = model.fileDesc;
            parameters[16].Value = model.USERID;
            parameters[17].Value = model.USERNAME;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from View_AllFileList ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
			};

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZK.Model.View_AllFileList GetModel()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 aid,id,fileID,fileName,cateID,cateName,fileTypeID,fileTypeName,channelID,channelName,createTime,imageURL,clickNum,isDir,filePath,fileDesc,USERID,USERNAME from View_AllFileList ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
			};

            ZK.Model.View_AllFileList model = new ZK.Model.View_AllFileList();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["aid"] != null && ds.Tables[0].Rows[0]["aid"].ToString() != "")
                {
                    model.aid = long.Parse(ds.Tables[0].Rows[0]["aid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = long.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["fileID"] != null && ds.Tables[0].Rows[0]["fileID"].ToString() != "")
                {
                    model.fileID = int.Parse(ds.Tables[0].Rows[0]["fileID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["fileName"] != null && ds.Tables[0].Rows[0]["fileName"].ToString() != "")
                {
                    model.fileName = ds.Tables[0].Rows[0]["fileName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cateID"] != null && ds.Tables[0].Rows[0]["cateID"].ToString() != "")
                {
                    model.cateID = int.Parse(ds.Tables[0].Rows[0]["cateID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["cateName"] != null && ds.Tables[0].Rows[0]["cateName"].ToString() != "")
                {
                    model.cateName = ds.Tables[0].Rows[0]["cateName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fileTypeID"] != null && ds.Tables[0].Rows[0]["fileTypeID"].ToString() != "")
                {
                    model.fileTypeID = int.Parse(ds.Tables[0].Rows[0]["fileTypeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["fileTypeName"] != null && ds.Tables[0].Rows[0]["fileTypeName"].ToString() != "")
                {
                    model.fileTypeName = ds.Tables[0].Rows[0]["fileTypeName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["channelID"] != null && ds.Tables[0].Rows[0]["channelID"].ToString() != "")
                {
                    model.channelID = int.Parse(ds.Tables[0].Rows[0]["channelID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["channelName"] != null && ds.Tables[0].Rows[0]["channelName"].ToString() != "")
                {
                    model.channelName = ds.Tables[0].Rows[0]["channelName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["createTime"] != null && ds.Tables[0].Rows[0]["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(ds.Tables[0].Rows[0]["createTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["imageURL"] != null && ds.Tables[0].Rows[0]["imageURL"].ToString() != "")
                {
                    model.imageURL = ds.Tables[0].Rows[0]["imageURL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["clickNum"] != null && ds.Tables[0].Rows[0]["clickNum"].ToString() != "")
                {
                    model.clickNum = int.Parse(ds.Tables[0].Rows[0]["clickNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isDir"] != null && ds.Tables[0].Rows[0]["isDir"].ToString() != "")
                {
                    model.isDir = int.Parse(ds.Tables[0].Rows[0]["isDir"].ToString());
                }
                if (ds.Tables[0].Rows[0]["filePath"] != null && ds.Tables[0].Rows[0]["filePath"].ToString() != "")
                {
                    model.filePath = ds.Tables[0].Rows[0]["filePath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fileDesc"] != null && ds.Tables[0].Rows[0]["fileDesc"].ToString() != "")
                {
                    model.fileDesc = ds.Tables[0].Rows[0]["fileDesc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["USERID"] != null && ds.Tables[0].Rows[0]["USERID"].ToString() != "")
                {
                    model.USERID = int.Parse(ds.Tables[0].Rows[0]["USERID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["USERNAME"] != null && ds.Tables[0].Rows[0]["USERNAME"].ToString() != "")
                {
                    model.USERNAME = ds.Tables[0].Rows[0]["USERNAME"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select aid,id,fileID,fileName,cateID,cateName,fileTypeID,fileTypeName,channelID,channelName,createTime,imageURL,clickNum,isDir,filePath,fileDesc,USERID,USERNAME ");
            strSql.Append(" FROM View_AllFileList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" aid,id,fileID,fileName,cateID,cateName,fileTypeID,fileTypeName,channelID,channelName,createTime,imageURL,clickNum,isDir,filePath,fileDesc,USERID,USERNAME ");
            strSql.Append(" FROM View_AllFileList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM View_AllFileList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.fileID desc");
            }
            strSql.Append(")AS Row, T.*  from View_AllFileList T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize, int PageIndex, string strWhere)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@select_list", SqlDbType.VarChar, 1000),
					new SqlParameter("@table_name", SqlDbType.VarChar, 1000),
					new SqlParameter("@where", SqlDbType.VarChar, 1000),
					new SqlParameter("@primary_key", SqlDbType.VarChar, 200),
					new SqlParameter("@order_by", SqlDbType.VarChar, 200),
					new SqlParameter("@page_size", SqlDbType.SmallInt),
					new SqlParameter("@page_index", SqlDbType.Int),
					new SqlParameter("@bl_page", SqlDbType.Int),
					};
            parameters[0].Value = "*";
            parameters[1].Value = "View_AllFileList";
            parameters[2].Value = strWhere;
            parameters[3].Value = " aid ";
            parameters[4].Value = "fileID desc";
            parameters[5].Value = PageSize;
            parameters[6].Value = PageIndex;
            parameters[7].Value = "1";
            return DbHelperSQL.RunProcedure("Common_PageList", parameters, "ds");
        }

        #endregion  Method
    }
}

