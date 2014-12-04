/**  智客知识管理平台。
* CONTACTGROUPS.cs
*
* 功 能： N/A
* 类 名： CONTACTGROUPS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:33   N/A    初版
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
	/// 数据访问类:CONTACTGROUPS
	/// </summary>
	public partial class CONTACTGROUPS:ICONTACTGROUPS
	{
		public CONTACTGROUPS()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CONTACTGROUPID", "CONTACTGROUPS"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CONTACTGROUPID,int USERID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from CONTACTGROUPS");
			strSql.Append(" where CONTACTGROUPID=@CONTACTGROUPID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CONTACTGROUPID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = CONTACTGROUPID;
			parameters[1].Value = USERID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.CONTACTGROUPS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CONTACTGROUPS(");
			strSql.Append("CONTACTGROUPID,USERID,CONTACTGROUPNAME,HIDEWHENONLINE,ONLINEWHENHIDE,REMINDMEWHENLOGIN,NOMESSAGE,ORDERVALUE,CREATETIME)");
			strSql.Append(" values (");
			strSql.Append("@CONTACTGROUPID,@USERID,@CONTACTGROUPNAME,@HIDEWHENONLINE,@ONLINEWHENHIDE,@REMINDMEWHENLOGIN,@NOMESSAGE,@ORDERVALUE,@CREATETIME)");
			SqlParameter[] parameters = {
					new SqlParameter("@CONTACTGROUPID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@CONTACTGROUPNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@HIDEWHENONLINE", SqlDbType.SmallInt,2),
					new SqlParameter("@ONLINEWHENHIDE", SqlDbType.SmallInt,2),
					new SqlParameter("@REMINDMEWHENLOGIN", SqlDbType.SmallInt,2),
					new SqlParameter("@NOMESSAGE", SqlDbType.SmallInt,2),
					new SqlParameter("@ORDERVALUE", SqlDbType.Int,4),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime)};
			parameters[0].Value = model.CONTACTGROUPID;
			parameters[1].Value = model.USERID;
			parameters[2].Value = model.CONTACTGROUPNAME;
			parameters[3].Value = model.HIDEWHENONLINE;
			parameters[4].Value = model.ONLINEWHENHIDE;
			parameters[5].Value = model.REMINDMEWHENLOGIN;
			parameters[6].Value = model.NOMESSAGE;
			parameters[7].Value = model.ORDERVALUE;
			parameters[8].Value = model.CREATETIME;

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
		public bool Update(ZK.Model.CONTACTGROUPS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CONTACTGROUPS set ");
			strSql.Append("CONTACTGROUPNAME=@CONTACTGROUPNAME,");
			strSql.Append("HIDEWHENONLINE=@HIDEWHENONLINE,");
			strSql.Append("ONLINEWHENHIDE=@ONLINEWHENHIDE,");
			strSql.Append("REMINDMEWHENLOGIN=@REMINDMEWHENLOGIN,");
			strSql.Append("NOMESSAGE=@NOMESSAGE,");
			strSql.Append("ORDERVALUE=@ORDERVALUE,");
			strSql.Append("CREATETIME=@CREATETIME");
			strSql.Append(" where CONTACTGROUPID=@CONTACTGROUPID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CONTACTGROUPNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@HIDEWHENONLINE", SqlDbType.SmallInt,2),
					new SqlParameter("@ONLINEWHENHIDE", SqlDbType.SmallInt,2),
					new SqlParameter("@REMINDMEWHENLOGIN", SqlDbType.SmallInt,2),
					new SqlParameter("@NOMESSAGE", SqlDbType.SmallInt,2),
					new SqlParameter("@ORDERVALUE", SqlDbType.Int,4),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime),
					new SqlParameter("@CONTACTGROUPID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4)};
			parameters[0].Value = model.CONTACTGROUPNAME;
			parameters[1].Value = model.HIDEWHENONLINE;
			parameters[2].Value = model.ONLINEWHENHIDE;
			parameters[3].Value = model.REMINDMEWHENLOGIN;
			parameters[4].Value = model.NOMESSAGE;
			parameters[5].Value = model.ORDERVALUE;
			parameters[6].Value = model.CREATETIME;
			parameters[7].Value = model.CONTACTGROUPID;
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
		public bool Delete(int CONTACTGROUPID,int USERID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CONTACTGROUPS ");
			strSql.Append(" where CONTACTGROUPID=@CONTACTGROUPID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CONTACTGROUPID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = CONTACTGROUPID;
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
		public ZK.Model.CONTACTGROUPS GetModel(int CONTACTGROUPID,int USERID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CONTACTGROUPID,USERID,CONTACTGROUPNAME,HIDEWHENONLINE,ONLINEWHENHIDE,REMINDMEWHENLOGIN,NOMESSAGE,ORDERVALUE,CREATETIME from CONTACTGROUPS ");
			strSql.Append(" where CONTACTGROUPID=@CONTACTGROUPID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CONTACTGROUPID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = CONTACTGROUPID;
			parameters[1].Value = USERID;

			ZK.Model.CONTACTGROUPS model=new ZK.Model.CONTACTGROUPS();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CONTACTGROUPID"]!=null && ds.Tables[0].Rows[0]["CONTACTGROUPID"].ToString()!="")
				{
					model.CONTACTGROUPID=int.Parse(ds.Tables[0].Rows[0]["CONTACTGROUPID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["USERID"]!=null && ds.Tables[0].Rows[0]["USERID"].ToString()!="")
				{
					model.USERID=int.Parse(ds.Tables[0].Rows[0]["USERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CONTACTGROUPNAME"]!=null && ds.Tables[0].Rows[0]["CONTACTGROUPNAME"].ToString()!="")
				{
					model.CONTACTGROUPNAME=ds.Tables[0].Rows[0]["CONTACTGROUPNAME"].ToString();
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
			strSql.Append("select CONTACTGROUPID,USERID,CONTACTGROUPNAME,HIDEWHENONLINE,ONLINEWHENHIDE,REMINDMEWHENLOGIN,NOMESSAGE,ORDERVALUE,CREATETIME ");
			strSql.Append(" FROM CONTACTGROUPS ");
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
			strSql.Append(" CONTACTGROUPID,USERID,CONTACTGROUPNAME,HIDEWHENONLINE,ONLINEWHENHIDE,REMINDMEWHENLOGIN,NOMESSAGE,ORDERVALUE,CREATETIME ");
			strSql.Append(" FROM CONTACTGROUPS ");
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
			strSql.Append("select count(1) FROM CONTACTGROUPS ");
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
			strSql.Append(")AS Row, T.*  from CONTACTGROUPS T ");
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
			parameters[1].Value = "CONTACTGROUPS";
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

