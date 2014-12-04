/**  智客知识管理平台。
* ZK_FileList.cs
*
* 功 能： N/A
* 类 名： ZK_FileList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/31 13:58:01   N/A    初版
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
	/// 数据访问类:ZK_FileList
	/// </summary>
	public partial class ZK_FileList:IZK_FileList
	{
		public ZK_FileList()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("fileID", "ZK_FileList"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int fileID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ZK_FileList");
			strSql.Append(" where fileID=@fileID");
			SqlParameter[] parameters = {
					new SqlParameter("@fileID", SqlDbType.Int,4)
			};
			parameters[0].Value = fileID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZK.Model.ZK_FileList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ZK_FileList(");
			strSql.Append("fileName,filePath,parentID,isDir,ownerID,fileTypeID,clickNum,createTime,imageURL,fileDesc,trastatus,isTraf,fileOldID)");
			strSql.Append(" values (");
			strSql.Append("@fileName,@filePath,@parentID,@isDir,@ownerID,@fileTypeID,@clickNum,@createTime,@imageURL,@fileDesc,@trastatus,@isTraf,@fileOldID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@fileName", SqlDbType.NVarChar,50),
					new SqlParameter("@filePath", SqlDbType.NVarChar,100),
					new SqlParameter("@parentID", SqlDbType.Int,4),
					new SqlParameter("@isDir", SqlDbType.Int,4),
					new SqlParameter("@ownerID", SqlDbType.Int,4),
					new SqlParameter("@fileTypeID", SqlDbType.Int,4),
					new SqlParameter("@clickNum", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@imageURL", SqlDbType.NVarChar,200),
					new SqlParameter("@fileDesc", SqlDbType.NVarChar,200),
					new SqlParameter("@trastatus", SqlDbType.Int,4),
					new SqlParameter("@isTraf", SqlDbType.Int,4),
					new SqlParameter("@fileOldID", SqlDbType.Int,4)};
			parameters[0].Value = model.fileName;
			parameters[1].Value = model.filePath;
			parameters[2].Value = model.parentID;
			parameters[3].Value = model.isDir;
			parameters[4].Value = model.ownerID;
			parameters[5].Value = model.fileTypeID;
			parameters[6].Value = model.clickNum;
			parameters[7].Value = model.createTime;
			parameters[8].Value = model.imageURL;
			parameters[9].Value = model.fileDesc;
			parameters[10].Value = model.trastatus;
			parameters[11].Value = model.isTraf;
			parameters[12].Value = model.fileOldID;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.ZK_FileList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ZK_FileList set ");
			strSql.Append("fileName=@fileName,");
			strSql.Append("filePath=@filePath,");
			strSql.Append("parentID=@parentID,");
			strSql.Append("isDir=@isDir,");
			strSql.Append("ownerID=@ownerID,");
			strSql.Append("fileTypeID=@fileTypeID,");
			strSql.Append("clickNum=@clickNum,");
			strSql.Append("createTime=@createTime,");
			strSql.Append("imageURL=@imageURL,");
			strSql.Append("fileDesc=@fileDesc,");
			strSql.Append("trastatus=@trastatus,");
			strSql.Append("isTraf=@isTraf,");
			strSql.Append("fileOldID=@fileOldID");
			strSql.Append(" where fileID=@fileID");
			SqlParameter[] parameters = {
					new SqlParameter("@fileName", SqlDbType.NVarChar,50),
					new SqlParameter("@filePath", SqlDbType.NVarChar,100),
					new SqlParameter("@parentID", SqlDbType.Int,4),
					new SqlParameter("@isDir", SqlDbType.Int,4),
					new SqlParameter("@ownerID", SqlDbType.Int,4),
					new SqlParameter("@fileTypeID", SqlDbType.Int,4),
					new SqlParameter("@clickNum", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@imageURL", SqlDbType.NVarChar,200),
					new SqlParameter("@fileDesc", SqlDbType.NVarChar,200),
					new SqlParameter("@trastatus", SqlDbType.Int,4),
					new SqlParameter("@isTraf", SqlDbType.Int,4),
					new SqlParameter("@fileOldID", SqlDbType.Int,4),
					new SqlParameter("@fileID", SqlDbType.Int,4)};
			parameters[0].Value = model.fileName;
			parameters[1].Value = model.filePath;
			parameters[2].Value = model.parentID;
			parameters[3].Value = model.isDir;
			parameters[4].Value = model.ownerID;
			parameters[5].Value = model.fileTypeID;
			parameters[6].Value = model.clickNum;
			parameters[7].Value = model.createTime;
			parameters[8].Value = model.imageURL;
			parameters[9].Value = model.fileDesc;
			parameters[10].Value = model.trastatus;
			parameters[11].Value = model.isTraf;
			parameters[12].Value = model.fileOldID;
			parameters[13].Value = model.fileID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(int fileID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_FileList ");
			strSql.Append(" where fileID=@fileID");
			SqlParameter[] parameters = {
					new SqlParameter("@fileID", SqlDbType.Int,4)
			};
			parameters[0].Value = fileID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string fileIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_FileList ");
			strSql.Append(" where fileID in ("+fileIDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public ZK.Model.ZK_FileList GetModel(int fileID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 fileID,fileName,filePath,parentID,isDir,ownerID,fileTypeID,clickNum,createTime,imageURL,fileDesc,trastatus,isTraf,fileOldID from ZK_FileList ");
			strSql.Append(" where fileID=@fileID");
			SqlParameter[] parameters = {
					new SqlParameter("@fileID", SqlDbType.Int,4)
			};
			parameters[0].Value = fileID;

			ZK.Model.ZK_FileList model=new ZK.Model.ZK_FileList();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["fileID"]!=null && ds.Tables[0].Rows[0]["fileID"].ToString()!="")
				{
					model.fileID=int.Parse(ds.Tables[0].Rows[0]["fileID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["fileName"]!=null && ds.Tables[0].Rows[0]["fileName"].ToString()!="")
				{
					model.fileName=ds.Tables[0].Rows[0]["fileName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["filePath"]!=null && ds.Tables[0].Rows[0]["filePath"].ToString()!="")
				{
					model.filePath=ds.Tables[0].Rows[0]["filePath"].ToString();
				}
				if(ds.Tables[0].Rows[0]["parentID"]!=null && ds.Tables[0].Rows[0]["parentID"].ToString()!="")
				{
					model.parentID=int.Parse(ds.Tables[0].Rows[0]["parentID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["isDir"]!=null && ds.Tables[0].Rows[0]["isDir"].ToString()!="")
				{
					model.isDir=int.Parse(ds.Tables[0].Rows[0]["isDir"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ownerID"]!=null && ds.Tables[0].Rows[0]["ownerID"].ToString()!="")
				{
					model.ownerID=int.Parse(ds.Tables[0].Rows[0]["ownerID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["fileTypeID"]!=null && ds.Tables[0].Rows[0]["fileTypeID"].ToString()!="")
				{
					model.fileTypeID=int.Parse(ds.Tables[0].Rows[0]["fileTypeID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["clickNum"]!=null && ds.Tables[0].Rows[0]["clickNum"].ToString()!="")
				{
					model.clickNum=int.Parse(ds.Tables[0].Rows[0]["clickNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["createTime"]!=null && ds.Tables[0].Rows[0]["createTime"].ToString()!="")
				{
					model.createTime=DateTime.Parse(ds.Tables[0].Rows[0]["createTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["imageURL"]!=null && ds.Tables[0].Rows[0]["imageURL"].ToString()!="")
				{
					model.imageURL=ds.Tables[0].Rows[0]["imageURL"].ToString();
				}
				if(ds.Tables[0].Rows[0]["fileDesc"]!=null && ds.Tables[0].Rows[0]["fileDesc"].ToString()!="")
				{
					model.fileDesc=ds.Tables[0].Rows[0]["fileDesc"].ToString();
				}
				if(ds.Tables[0].Rows[0]["trastatus"]!=null && ds.Tables[0].Rows[0]["trastatus"].ToString()!="")
				{
					model.trastatus=int.Parse(ds.Tables[0].Rows[0]["trastatus"].ToString());
				}
				if(ds.Tables[0].Rows[0]["isTraf"]!=null && ds.Tables[0].Rows[0]["isTraf"].ToString()!="")
				{
					model.isTraf=int.Parse(ds.Tables[0].Rows[0]["isTraf"].ToString());
				}
				if(ds.Tables[0].Rows[0]["fileOldID"]!=null && ds.Tables[0].Rows[0]["fileOldID"].ToString()!="")
				{
					model.fileOldID=int.Parse(ds.Tables[0].Rows[0]["fileOldID"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select fileID,fileName,filePath,parentID,isDir,ownerID,fileTypeID,clickNum,createTime,imageURL,fileDesc,trastatus,isTraf,fileOldID ");
			strSql.Append(" FROM ZK_FileList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" fileID,fileName,filePath,parentID,isDir,ownerID,fileTypeID,clickNum,createTime,imageURL,fileDesc,trastatus,isTraf,fileOldID ");
			strSql.Append(" FROM ZK_FileList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM ZK_FileList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.fileID desc");
			}
			strSql.Append(")AS Row, T.*  from ZK_FileList T ");
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
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
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
			parameters[1].Value = "ZK_FileList";
			parameters[2].Value = strWhere;
			parameters[3].Value = "fileID";
			parameters[4].Value = "fileID desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

