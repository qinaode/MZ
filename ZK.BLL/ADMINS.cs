/**  智客知识管理平台。
* ADMINS.cs
*
* 功 能： N/A
* 类 名： ADMINS
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
using System.Collections.Generic;
using ZK.Common;
using ZK.Model;
using ZK.DALFactory;
using ZK.IDAL;
namespace ZK.BLL
{
	/// <summary>
	/// ADMINS
	/// </summary>
	public partial class ADMINS
	{
		private readonly IADMINS dal=DataAccess.CreateADMINS();
		public ADMINS()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ADMINNAME)
		{
			return dal.Exists(ADMINNAME);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.ADMINS model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.ADMINS model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string ADMINNAME)
		{
			
			return dal.Delete(ADMINNAME);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string ADMINNAMElist )
		{
			return dal.DeleteList(ADMINNAMElist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.ADMINS GetModel(string ADMINNAME)
		{
			
			return dal.GetModel(ADMINNAME);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.ADMINS GetModelByCache(string ADMINNAME)
		{
			
			string CacheKey = "ADMINSModel-" + ADMINNAME;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ADMINNAME);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.ADMINS)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.ADMINS> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.ADMINS> DataTableToList(DataTable dt)
		{
			List<ZK.Model.ADMINS> modelList = new List<ZK.Model.ADMINS>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.ADMINS model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.ADMINS();
					if(dt.Rows[n]["ADMINNAME"]!=null && dt.Rows[n]["ADMINNAME"].ToString()!="")
					{
					model.ADMINNAME=dt.Rows[n]["ADMINNAME"].ToString();
					}
					if(dt.Rows[n]["ADMINPWD"]!=null && dt.Rows[n]["ADMINPWD"].ToString()!="")
					{
					model.ADMINPWD=dt.Rows[n]["ADMINPWD"].ToString();
					}
					if(dt.Rows[n]["DESCRIPTION"]!=null && dt.Rows[n]["DESCRIPTION"].ToString()!="")
					{
					model.DESCRIPTION=dt.Rows[n]["DESCRIPTION"].ToString();
					}
					if(dt.Rows[n]["LOGINED"]!=null && dt.Rows[n]["LOGINED"].ToString()!="")
					{
						model.LOGINED=int.Parse(dt.Rows[n]["LOGINED"].ToString());
					}
					if(dt.Rows[n]["ISLOCK"]!=null && dt.Rows[n]["ISLOCK"].ToString()!="")
					{
						model.ISLOCK=int.Parse(dt.Rows[n]["ISLOCK"].ToString());
					}
					if(dt.Rows[n]["LASTLOGINTIME"]!=null && dt.Rows[n]["LASTLOGINTIME"].ToString()!="")
					{
						model.LASTLOGINTIME=DateTime.Parse(dt.Rows[n]["LASTLOGINTIME"].ToString());
					}
					if(dt.Rows[n]["CREATETIME"]!=null && dt.Rows[n]["CREATETIME"].ToString()!="")
					{
						model.CREATETIME=DateTime.Parse(dt.Rows[n]["CREATETIME"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			return dal.GetList(PageSize,PageIndex,strWhere);
		}

		#endregion  Method
	}
}

