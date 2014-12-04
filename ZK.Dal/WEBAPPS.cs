/**  智客知识管理平台。
* WEBAPPS.cs
*
* 功 能： N/A
* 类 名： WEBAPPS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:40   N/A    初版
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
	/// 数据访问类:WEBAPPS
	/// </summary>
	public partial class WEBAPPS:IWEBAPPS
	{
		public WEBAPPS()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("APPID", "WEBAPPS"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int APPID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from WEBAPPS");
			strSql.Append(" where APPID=@APPID ");
			SqlParameter[] parameters = {
					new SqlParameter("@APPID", SqlDbType.Int,4)			};
			parameters[0].Value = APPID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.WEBAPPS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into WEBAPPS(");
			strSql.Append("APPID,FORUSERTYPE,APPNAME,CATEGORY,INTRODUCTION,APPIMAGE,APPURL,METHOD,POSTDATA,POPUP,CLIENTWEBBROWSER,WEBBROWSERWIDTH,WEBBROWSERHEIGHT,SHORTCUT,ORDERVALUE,CREATETIME)");
			strSql.Append(" values (");
			strSql.Append("@APPID,@FORUSERTYPE,@APPNAME,@CATEGORY,@INTRODUCTION,@APPIMAGE,@APPURL,@METHOD,@POSTDATA,@POPUP,@CLIENTWEBBROWSER,@WEBBROWSERWIDTH,@WEBBROWSERHEIGHT,@SHORTCUT,@ORDERVALUE,@CREATETIME)");
			SqlParameter[] parameters = {
					new SqlParameter("@APPID", SqlDbType.Int,4),
					new SqlParameter("@FORUSERTYPE", SqlDbType.SmallInt,2),
					new SqlParameter("@APPNAME", SqlDbType.NVarChar,50),
					new SqlParameter("@CATEGORY", SqlDbType.NVarChar,50),
					new SqlParameter("@INTRODUCTION", SqlDbType.NVarChar,255),
					new SqlParameter("@APPIMAGE", SqlDbType.NVarChar,255),
					new SqlParameter("@APPURL", SqlDbType.NVarChar,1024),
					new SqlParameter("@METHOD", SqlDbType.SmallInt,2),
					new SqlParameter("@POSTDATA", SqlDbType.NVarChar,1024),
					new SqlParameter("@POPUP", SqlDbType.Int,4),
					new SqlParameter("@CLIENTWEBBROWSER", SqlDbType.SmallInt,2),
					new SqlParameter("@WEBBROWSERWIDTH", SqlDbType.Int,4),
					new SqlParameter("@WEBBROWSERHEIGHT", SqlDbType.Int,4),
					new SqlParameter("@SHORTCUT", SqlDbType.SmallInt,2),
					new SqlParameter("@ORDERVALUE", SqlDbType.Int,4),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime)};
			parameters[0].Value = model.APPID;
			parameters[1].Value = model.FORUSERTYPE;
			parameters[2].Value = model.APPNAME;
			parameters[3].Value = model.CATEGORY;
			parameters[4].Value = model.INTRODUCTION;
			parameters[5].Value = model.APPIMAGE;
			parameters[6].Value = model.APPURL;
			parameters[7].Value = model.METHOD;
			parameters[8].Value = model.POSTDATA;
			parameters[9].Value = model.POPUP;
			parameters[10].Value = model.CLIENTWEBBROWSER;
			parameters[11].Value = model.WEBBROWSERWIDTH;
			parameters[12].Value = model.WEBBROWSERHEIGHT;
			parameters[13].Value = model.SHORTCUT;
			parameters[14].Value = model.ORDERVALUE;
			parameters[15].Value = model.CREATETIME;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.WEBAPPS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update WEBAPPS set ");
			strSql.Append("FORUSERTYPE=@FORUSERTYPE,");
			strSql.Append("APPNAME=@APPNAME,");
			strSql.Append("CATEGORY=@CATEGORY,");
			strSql.Append("INTRODUCTION=@INTRODUCTION,");
			strSql.Append("APPIMAGE=@APPIMAGE,");
			strSql.Append("APPURL=@APPURL,");
			strSql.Append("METHOD=@METHOD,");
			strSql.Append("POSTDATA=@POSTDATA,");
			strSql.Append("POPUP=@POPUP,");
			strSql.Append("CLIENTWEBBROWSER=@CLIENTWEBBROWSER,");
			strSql.Append("WEBBROWSERWIDTH=@WEBBROWSERWIDTH,");
			strSql.Append("WEBBROWSERHEIGHT=@WEBBROWSERHEIGHT,");
			strSql.Append("SHORTCUT=@SHORTCUT,");
			strSql.Append("ORDERVALUE=@ORDERVALUE,");
			strSql.Append("CREATETIME=@CREATETIME");
			strSql.Append(" where APPID=@APPID ");
			SqlParameter[] parameters = {
					new SqlParameter("@FORUSERTYPE", SqlDbType.SmallInt,2),
					new SqlParameter("@APPNAME", SqlDbType.NVarChar,50),
					new SqlParameter("@CATEGORY", SqlDbType.NVarChar,50),
					new SqlParameter("@INTRODUCTION", SqlDbType.NVarChar,255),
					new SqlParameter("@APPIMAGE", SqlDbType.NVarChar,255),
					new SqlParameter("@APPURL", SqlDbType.NVarChar,1024),
					new SqlParameter("@METHOD", SqlDbType.SmallInt,2),
					new SqlParameter("@POSTDATA", SqlDbType.NVarChar,1024),
					new SqlParameter("@POPUP", SqlDbType.Int,4),
					new SqlParameter("@CLIENTWEBBROWSER", SqlDbType.SmallInt,2),
					new SqlParameter("@WEBBROWSERWIDTH", SqlDbType.Int,4),
					new SqlParameter("@WEBBROWSERHEIGHT", SqlDbType.Int,4),
					new SqlParameter("@SHORTCUT", SqlDbType.SmallInt,2),
					new SqlParameter("@ORDERVALUE", SqlDbType.Int,4),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime),
					new SqlParameter("@APPID", SqlDbType.Int,4)};
			parameters[0].Value = model.FORUSERTYPE;
			parameters[1].Value = model.APPNAME;
			parameters[2].Value = model.CATEGORY;
			parameters[3].Value = model.INTRODUCTION;
			parameters[4].Value = model.APPIMAGE;
			parameters[5].Value = model.APPURL;
			parameters[6].Value = model.METHOD;
			parameters[7].Value = model.POSTDATA;
			parameters[8].Value = model.POPUP;
			parameters[9].Value = model.CLIENTWEBBROWSER;
			parameters[10].Value = model.WEBBROWSERWIDTH;
			parameters[11].Value = model.WEBBROWSERHEIGHT;
			parameters[12].Value = model.SHORTCUT;
			parameters[13].Value = model.ORDERVALUE;
			parameters[14].Value = model.CREATETIME;
			parameters[15].Value = model.APPID;

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
		public bool Delete(int APPID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from WEBAPPS ");
			strSql.Append(" where APPID=@APPID ");
			SqlParameter[] parameters = {
					new SqlParameter("@APPID", SqlDbType.Int,4)			};
			parameters[0].Value = APPID;

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
		public bool DeleteList(string APPIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from WEBAPPS ");
			strSql.Append(" where APPID in ("+APPIDlist + ")  ");
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
		public ZK.Model.WEBAPPS GetModel(int APPID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 APPID,FORUSERTYPE,APPNAME,CATEGORY,INTRODUCTION,APPIMAGE,APPURL,METHOD,POSTDATA,POPUP,CLIENTWEBBROWSER,WEBBROWSERWIDTH,WEBBROWSERHEIGHT,SHORTCUT,ORDERVALUE,CREATETIME from WEBAPPS ");
			strSql.Append(" where APPID=@APPID ");
			SqlParameter[] parameters = {
					new SqlParameter("@APPID", SqlDbType.Int,4)			};
			parameters[0].Value = APPID;

			ZK.Model.WEBAPPS model=new ZK.Model.WEBAPPS();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["APPID"]!=null && ds.Tables[0].Rows[0]["APPID"].ToString()!="")
				{
					model.APPID=int.Parse(ds.Tables[0].Rows[0]["APPID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FORUSERTYPE"]!=null && ds.Tables[0].Rows[0]["FORUSERTYPE"].ToString()!="")
				{
					model.FORUSERTYPE=int.Parse(ds.Tables[0].Rows[0]["FORUSERTYPE"].ToString());
				}
				if(ds.Tables[0].Rows[0]["APPNAME"]!=null && ds.Tables[0].Rows[0]["APPNAME"].ToString()!="")
				{
					model.APPNAME=ds.Tables[0].Rows[0]["APPNAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CATEGORY"]!=null && ds.Tables[0].Rows[0]["CATEGORY"].ToString()!="")
				{
					model.CATEGORY=ds.Tables[0].Rows[0]["CATEGORY"].ToString();
				}
				if(ds.Tables[0].Rows[0]["INTRODUCTION"]!=null && ds.Tables[0].Rows[0]["INTRODUCTION"].ToString()!="")
				{
					model.INTRODUCTION=ds.Tables[0].Rows[0]["INTRODUCTION"].ToString();
				}
				if(ds.Tables[0].Rows[0]["APPIMAGE"]!=null && ds.Tables[0].Rows[0]["APPIMAGE"].ToString()!="")
				{
					model.APPIMAGE=ds.Tables[0].Rows[0]["APPIMAGE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["APPURL"]!=null && ds.Tables[0].Rows[0]["APPURL"].ToString()!="")
				{
					model.APPURL=ds.Tables[0].Rows[0]["APPURL"].ToString();
				}
				if(ds.Tables[0].Rows[0]["METHOD"]!=null && ds.Tables[0].Rows[0]["METHOD"].ToString()!="")
				{
					model.METHOD=int.Parse(ds.Tables[0].Rows[0]["METHOD"].ToString());
				}
				if(ds.Tables[0].Rows[0]["POSTDATA"]!=null && ds.Tables[0].Rows[0]["POSTDATA"].ToString()!="")
				{
					model.POSTDATA=ds.Tables[0].Rows[0]["POSTDATA"].ToString();
				}
				if(ds.Tables[0].Rows[0]["POPUP"]!=null && ds.Tables[0].Rows[0]["POPUP"].ToString()!="")
				{
					model.POPUP=int.Parse(ds.Tables[0].Rows[0]["POPUP"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CLIENTWEBBROWSER"]!=null && ds.Tables[0].Rows[0]["CLIENTWEBBROWSER"].ToString()!="")
				{
					model.CLIENTWEBBROWSER=int.Parse(ds.Tables[0].Rows[0]["CLIENTWEBBROWSER"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WEBBROWSERWIDTH"]!=null && ds.Tables[0].Rows[0]["WEBBROWSERWIDTH"].ToString()!="")
				{
					model.WEBBROWSERWIDTH=int.Parse(ds.Tables[0].Rows[0]["WEBBROWSERWIDTH"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WEBBROWSERHEIGHT"]!=null && ds.Tables[0].Rows[0]["WEBBROWSERHEIGHT"].ToString()!="")
				{
					model.WEBBROWSERHEIGHT=int.Parse(ds.Tables[0].Rows[0]["WEBBROWSERHEIGHT"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SHORTCUT"]!=null && ds.Tables[0].Rows[0]["SHORTCUT"].ToString()!="")
				{
					model.SHORTCUT=int.Parse(ds.Tables[0].Rows[0]["SHORTCUT"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ORDERVALUE"]!=null && ds.Tables[0].Rows[0]["ORDERVALUE"].ToString()!="")
				{
					model.ORDERVALUE=int.Parse(ds.Tables[0].Rows[0]["ORDERVALUE"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CREATETIME"]!=null && ds.Tables[0].Rows[0]["CREATETIME"].ToString()!="")
				{
					model.CREATETIME=DateTime.Parse(ds.Tables[0].Rows[0]["CREATETIME"].ToString());
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
			strSql.Append("select APPID,FORUSERTYPE,APPNAME,CATEGORY,INTRODUCTION,APPIMAGE,APPURL,METHOD,POSTDATA,POPUP,CLIENTWEBBROWSER,WEBBROWSERWIDTH,WEBBROWSERHEIGHT,SHORTCUT,ORDERVALUE,CREATETIME ");
			strSql.Append(" FROM WEBAPPS ");
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
			strSql.Append(" APPID,FORUSERTYPE,APPNAME,CATEGORY,INTRODUCTION,APPIMAGE,APPURL,METHOD,POSTDATA,POPUP,CLIENTWEBBROWSER,WEBBROWSERWIDTH,WEBBROWSERHEIGHT,SHORTCUT,ORDERVALUE,CREATETIME ");
			strSql.Append(" FROM WEBAPPS ");
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
			strSql.Append("select count(1) FROM WEBAPPS ");
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
				strSql.Append("order by T.APPID desc");
			}
			strSql.Append(")AS Row, T.*  from WEBAPPS T ");
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
			parameters[1].Value = "WEBAPPS";
			parameters[2].Value = strWhere;
			parameters[3].Value = "APPID";
			parameters[4].Value = "APPID desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

