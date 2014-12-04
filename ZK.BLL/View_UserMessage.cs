/**  智客知识管理平台。
* View_UserMessage.cs
*
* 功 能： N/A
* 类 名： View_UserMessage
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/26 11:22:42   N/A    初版
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
	/// View_UserMessage
	/// </summary>
	public partial class View_UserMessage
	{
		private readonly IView_UserMessage dal=DataAccess.CreateView_UserMessage();
		public View_UserMessage()
		{}
		#region  Method

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.View_UserMessage model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.View_UserMessage model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.Delete();
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.View_UserMessage GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.View_UserMessage GetModelByCache()
		{
			//该表无主键信息，请自定义主键/条件字段
			string CacheKey = "View_UserMessageModel-" ;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel();
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.View_UserMessage)objModel;
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
		public List<ZK.Model.View_UserMessage> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.View_UserMessage> DataTableToList(DataTable dt)
		{
			List<ZK.Model.View_UserMessage> modelList = new List<ZK.Model.View_UserMessage>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.View_UserMessage model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.View_UserMessage();
					if(dt.Rows[n]["SID"]!=null && dt.Rows[n]["SID"].ToString()!="")
					{
						model.SID=long.Parse(dt.Rows[n]["SID"].ToString());
					}
					if(dt.Rows[n]["TITLE"]!=null && dt.Rows[n]["TITLE"].ToString()!="")
					{
					model.TITLE=dt.Rows[n]["TITLE"].ToString();
					}
					if(dt.Rows[n]["CONTENT"]!=null && dt.Rows[n]["CONTENT"].ToString()!="")
					{
					model.CONTENT=dt.Rows[n]["CONTENT"].ToString();
					}
					if(dt.Rows[n]["LINK"]!=null && dt.Rows[n]["LINK"].ToString()!="")
					{
					model.LINK=dt.Rows[n]["LINK"].ToString();
					}
					if(dt.Rows[n]["ONLINE"]!=null && dt.Rows[n]["ONLINE"].ToString()!="")
					{
						model.ONLINE=int.Parse(dt.Rows[n]["ONLINE"].ToString());
					}
					if(dt.Rows[n]["SENDTIME"]!=null && dt.Rows[n]["SENDTIME"].ToString()!="")
					{
						model.SENDTIME=DateTime.Parse(dt.Rows[n]["SENDTIME"].ToString());
					}
					if(dt.Rows[n]["SENDTO"]!=null && dt.Rows[n]["SENDTO"].ToString()!="")
					{
						model.SENDTO=int.Parse(dt.Rows[n]["SENDTO"].ToString());
					}
					if(dt.Rows[n]["isSee"]!=null && dt.Rows[n]["isSee"].ToString()!="")
					{
						if((dt.Rows[n]["isSee"].ToString()=="1")||(dt.Rows[n]["isSee"].ToString().ToLower()=="true"))
						{
						model.isSee=true;
						}
						else
						{
							model.isSee=false;
						}
					}
					if(dt.Rows[n]["FORUSERTYPE"]!=null && dt.Rows[n]["FORUSERTYPE"].ToString()!="")
					{
						model.FORUSERTYPE=int.Parse(dt.Rows[n]["FORUSERTYPE"].ToString());
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

