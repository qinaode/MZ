﻿/**  智客知识管理平台。
* miniyun_file_tags.cs
*
* 功 能： N/A
* 类 名： miniyun_file_tags
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/24 20:30:11   N/A    初版
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
using MySql.Data.MySqlClient;
using ZK.IDAL;
using ZK.DBUtility;//Please add references
namespace ZK.Dal
{
	/// <summary>
	/// 数据访问类:miniyun_file_tags
	/// </summary>
	public partial class miniyun_file_tags:Iminiyun_file_tags
	{
		public miniyun_file_tags()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("id", "miniyun_file_tags"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from miniyun_file_tags");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
			parameters[0].Value = id;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.miniyun_file_tags model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into miniyun_file_tags(");
			strSql.Append("file_id,tag_id)");
			strSql.Append(" values (");
			strSql.Append("@file_id,@tag_id)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@file_id", MySqlDbType.Int32,11),
					new MySqlParameter("@tag_id", MySqlDbType.Int32,11)};
			parameters[0].Value = model.file_id;
			parameters[1].Value = model.tag_id;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Update(ZK.Model.miniyun_file_tags model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update miniyun_file_tags set ");
			strSql.Append("file_id=@file_id,");
			strSql.Append("tag_id=@tag_id");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@file_id", MySqlDbType.Int32,11),
					new MySqlParameter("@tag_id", MySqlDbType.Int32,11),
					new MySqlParameter("@id", MySqlDbType.Int32,11)};
			parameters[0].Value = model.file_id;
			parameters[1].Value = model.tag_id;
			parameters[2].Value = model.id;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from miniyun_file_tags ");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
			parameters[0].Value = id;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from miniyun_file_tags ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
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
		public ZK.Model.miniyun_file_tags GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,file_id,tag_id from miniyun_file_tags ");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
			parameters[0].Value = id;

			ZK.Model.miniyun_file_tags model=new ZK.Model.miniyun_file_tags();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"]!=null && ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["file_id"]!=null && ds.Tables[0].Rows[0]["file_id"].ToString()!="")
				{
					model.file_id=int.Parse(ds.Tables[0].Rows[0]["file_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["tag_id"]!=null && ds.Tables[0].Rows[0]["tag_id"].ToString()!="")
				{
					model.tag_id=int.Parse(ds.Tables[0].Rows[0]["tag_id"].ToString());
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
			strSql.Append("select id,file_id,tag_id ");
			strSql.Append(" FROM miniyun_file_tags ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM miniyun_file_tags ");
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
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from miniyun_file_tags T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			MySqlParameter[] parameters = {
					new MySqlParameter("@select_list", MySqlDbType.VarChar, 1000),
					new MySqlParameter("@table_name", MySqlDbType.VarChar, 1000),
					new MySqlParameter("@where", MySqlDbType.VarChar, 1000),
					new MySqlParameter("@primary_key", MySqlDbType.VarChar, 200),
					new MySqlParameter("@order_by", MySqlDbType.VarChar, 200),
					new MySqlParameter("@page_size", MySqlDbType.Int32),
					new MySqlParameter("@page_index", MySqlDbType.Int32),
					new MySqlParameter("@bl_page", MySqlDbType.Int32),
					};
			parameters[0].Value = "*";
			parameters[1].Value = "miniyun_file_tags";
			parameters[2].Value = strWhere;
			parameters[3].Value = "id";
			parameters[4].Value = "id desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
            //	return DbHelperMySQL.RunProcedure("Common_PageList",parameters,"ds");
            return new DataSet();
		}

		#endregion  Method
	}
}

