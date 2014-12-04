/**  智客知识管理平台。
* ACCOUNTS.cs
*
* 功 能： N/A
* 类 名： ACCOUNTS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:32   N/A    初版
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
	/// ACCOUNTS
	/// </summary>
	public partial class ACCOUNTS
	{
		private readonly IACCOUNTS dal=DataAccess.CreateACCOUNTS();
		public ACCOUNTS()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int USERID)
		{
			return dal.Exists(USERID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.ACCOUNTS model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.ACCOUNTS model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int USERID)
		{
			
			return dal.Delete(USERID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string USERIDlist )
		{
			return dal.DeleteList(USERIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.ACCOUNTS GetModel(int USERID)
		{
			
			return dal.GetModel(USERID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.ACCOUNTS GetModelByCache(int USERID)
		{
			
			string CacheKey = "ACCOUNTSModel-" + USERID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(USERID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.ACCOUNTS)objModel;
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
		public List<ZK.Model.ACCOUNTS> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.ACCOUNTS> DataTableToList(DataTable dt)
		{
			List<ZK.Model.ACCOUNTS> modelList = new List<ZK.Model.ACCOUNTS>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.ACCOUNTS model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.ACCOUNTS();
					if(dt.Rows[n]["USERID"]!=null && dt.Rows[n]["USERID"].ToString()!="")
					{
						model.USERID=int.Parse(dt.Rows[n]["USERID"].ToString());
					}
					if(dt.Rows[n]["RANDOMVALUE"]!=null && dt.Rows[n]["RANDOMVALUE"].ToString()!="")
					{
						model.RANDOMVALUE=decimal.Parse(dt.Rows[n]["RANDOMVALUE"].ToString());
					}
					if(dt.Rows[n]["REGISTED"]!=null && dt.Rows[n]["REGISTED"].ToString()!="")
					{
						model.REGISTED=int.Parse(dt.Rows[n]["REGISTED"].ToString());
					}
					if(dt.Rows[n]["REGISTERTIME"]!=null && dt.Rows[n]["REGISTERTIME"].ToString()!="")
					{
						model.REGISTERTIME=DateTime.Parse(dt.Rows[n]["REGISTERTIME"].ToString());
					}
					if(dt.Rows[n]["REGISTERIPADDR"]!=null && dt.Rows[n]["REGISTERIPADDR"].ToString()!="")
					{
					model.REGISTERIPADDR=dt.Rows[n]["REGISTERIPADDR"].ToString();
					}
					if(dt.Rows[n]["SECTION"]!=null && dt.Rows[n]["SECTION"].ToString()!="")
					{
					model.SECTION=dt.Rows[n]["SECTION"].ToString();
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

