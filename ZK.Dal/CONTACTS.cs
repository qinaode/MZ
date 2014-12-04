/**  智客知识管理平台。
* CONTACTS.cs
*
* 功 能： N/A
* 类 名： CONTACTS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:34   N/A    初版
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
	/// 数据访问类:CONTACTS
	/// </summary>
	public partial class CONTACTS:ICONTACTS
	{
		public CONTACTS()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CONTACTID", "CONTACTS"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CONTACTID,int USERID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from CONTACTS");
			strSql.Append(" where CONTACTID=@CONTACTID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CONTACTID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = CONTACTID;
			parameters[1].Value = USERID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.CONTACTS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CONTACTS(");
			strSql.Append("CONTACTID,USERID,CONTACTGROUPID,REMARKNAME,HIDEWHENONLINE,ONLINEWHENHIDE,REMINDMEWHENLOGIN,NOMESSAGE,JOINTIME)");
			strSql.Append(" values (");
			strSql.Append("@CONTACTID,@USERID,@CONTACTGROUPID,@REMARKNAME,@HIDEWHENONLINE,@ONLINEWHENHIDE,@REMINDMEWHENLOGIN,@NOMESSAGE,@JOINTIME)");
			SqlParameter[] parameters = {
					new SqlParameter("@CONTACTID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@CONTACTGROUPID", SqlDbType.Int,4),
					new SqlParameter("@REMARKNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@HIDEWHENONLINE", SqlDbType.SmallInt,2),
					new SqlParameter("@ONLINEWHENHIDE", SqlDbType.SmallInt,2),
					new SqlParameter("@REMINDMEWHENLOGIN", SqlDbType.SmallInt,2),
					new SqlParameter("@NOMESSAGE", SqlDbType.SmallInt,2),
					new SqlParameter("@JOINTIME", SqlDbType.DateTime)};
			parameters[0].Value = model.CONTACTID;
			parameters[1].Value = model.USERID;
			parameters[2].Value = model.CONTACTGROUPID;
			parameters[3].Value = model.REMARKNAME;
			parameters[4].Value = model.HIDEWHENONLINE;
			parameters[5].Value = model.ONLINEWHENHIDE;
			parameters[6].Value = model.REMINDMEWHENLOGIN;
			parameters[7].Value = model.NOMESSAGE;
			parameters[8].Value = model.JOINTIME;

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
		public bool Update(ZK.Model.CONTACTS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CONTACTS set ");
			strSql.Append("CONTACTGROUPID=@CONTACTGROUPID,");
			strSql.Append("REMARKNAME=@REMARKNAME,");
			strSql.Append("HIDEWHENONLINE=@HIDEWHENONLINE,");
			strSql.Append("ONLINEWHENHIDE=@ONLINEWHENHIDE,");
			strSql.Append("REMINDMEWHENLOGIN=@REMINDMEWHENLOGIN,");
			strSql.Append("NOMESSAGE=@NOMESSAGE,");
			strSql.Append("JOINTIME=@JOINTIME");
			strSql.Append(" where CONTACTID=@CONTACTID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CONTACTGROUPID", SqlDbType.Int,4),
					new SqlParameter("@REMARKNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@HIDEWHENONLINE", SqlDbType.SmallInt,2),
					new SqlParameter("@ONLINEWHENHIDE", SqlDbType.SmallInt,2),
					new SqlParameter("@REMINDMEWHENLOGIN", SqlDbType.SmallInt,2),
					new SqlParameter("@NOMESSAGE", SqlDbType.SmallInt,2),
					new SqlParameter("@JOINTIME", SqlDbType.DateTime),
					new SqlParameter("@CONTACTID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4)};
			parameters[0].Value = model.CONTACTGROUPID;
			parameters[1].Value = model.REMARKNAME;
			parameters[2].Value = model.HIDEWHENONLINE;
			parameters[3].Value = model.ONLINEWHENHIDE;
			parameters[4].Value = model.REMINDMEWHENLOGIN;
			parameters[5].Value = model.NOMESSAGE;
			parameters[6].Value = model.JOINTIME;
			parameters[7].Value = model.CONTACTID;
			parameters[8].Value = model.USERID;

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
		public bool Delete(int CONTACTID,int USERID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CONTACTS ");
			strSql.Append(" where CONTACTID=@CONTACTID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CONTACTID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = CONTACTID;
			parameters[1].Value = USERID;

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
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.CONTACTS GetModel(int CONTACTID,int USERID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CONTACTID,USERID,CONTACTGROUPID,REMARKNAME,HIDEWHENONLINE,ONLINEWHENHIDE,REMINDMEWHENLOGIN,NOMESSAGE,JOINTIME from CONTACTS ");
			strSql.Append(" where CONTACTID=@CONTACTID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CONTACTID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = CONTACTID;
			parameters[1].Value = USERID;

			ZK.Model.CONTACTS model=new ZK.Model.CONTACTS();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CONTACTID"]!=null && ds.Tables[0].Rows[0]["CONTACTID"].ToString()!="")
				{
					model.CONTACTID=int.Parse(ds.Tables[0].Rows[0]["CONTACTID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["USERID"]!=null && ds.Tables[0].Rows[0]["USERID"].ToString()!="")
				{
					model.USERID=int.Parse(ds.Tables[0].Rows[0]["USERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CONTACTGROUPID"]!=null && ds.Tables[0].Rows[0]["CONTACTGROUPID"].ToString()!="")
				{
					model.CONTACTGROUPID=int.Parse(ds.Tables[0].Rows[0]["CONTACTGROUPID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["REMARKNAME"]!=null && ds.Tables[0].Rows[0]["REMARKNAME"].ToString()!="")
				{
					model.REMARKNAME=ds.Tables[0].Rows[0]["REMARKNAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["HIDEWHENONLINE"]!=null && ds.Tables[0].Rows[0]["HIDEWHENONLINE"].ToString()!="")
				{
					model.HIDEWHENONLINE=int.Parse(ds.Tables[0].Rows[0]["HIDEWHENONLINE"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ONLINEWHENHIDE"]!=null && ds.Tables[0].Rows[0]["ONLINEWHENHIDE"].ToString()!="")
				{
					model.ONLINEWHENHIDE=int.Parse(ds.Tables[0].Rows[0]["ONLINEWHENHIDE"].ToString());
				}
				if(ds.Tables[0].Rows[0]["REMINDMEWHENLOGIN"]!=null && ds.Tables[0].Rows[0]["REMINDMEWHENLOGIN"].ToString()!="")
				{
					model.REMINDMEWHENLOGIN=int.Parse(ds.Tables[0].Rows[0]["REMINDMEWHENLOGIN"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NOMESSAGE"]!=null && ds.Tables[0].Rows[0]["NOMESSAGE"].ToString()!="")
				{
					model.NOMESSAGE=int.Parse(ds.Tables[0].Rows[0]["NOMESSAGE"].ToString());
				}
				if(ds.Tables[0].Rows[0]["JOINTIME"]!=null && ds.Tables[0].Rows[0]["JOINTIME"].ToString()!="")
				{
					model.JOINTIME=DateTime.Parse(ds.Tables[0].Rows[0]["JOINTIME"].ToString());
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
			strSql.Append("select CONTACTID,USERID,CONTACTGROUPID,REMARKNAME,HIDEWHENONLINE,ONLINEWHENHIDE,REMINDMEWHENLOGIN,NOMESSAGE,JOINTIME ");
			strSql.Append(" FROM CONTACTS ");
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
			strSql.Append(" CONTACTID,USERID,CONTACTGROUPID,REMARKNAME,HIDEWHENONLINE,ONLINEWHENHIDE,REMINDMEWHENLOGIN,NOMESSAGE,JOINTIME ");
			strSql.Append(" FROM CONTACTS ");
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
			strSql.Append("select count(1) FROM CONTACTS ");
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
				strSql.Append("order by T.USERID desc");
			}
			strSql.Append(")AS Row, T.*  from CONTACTS T ");
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
			parameters[1].Value = "CONTACTS";
			parameters[2].Value = strWhere;
			parameters[3].Value = "USERID";
			parameters[4].Value = "USERID desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

