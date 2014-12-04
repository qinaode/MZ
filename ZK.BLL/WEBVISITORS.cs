/**  智客知识管理平台。
* WEBVISITORS.cs
*
* 功 能： N/A
* 类 名： WEBVISITORS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:41   N/A    初版
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
	/// 1
	/// </summary>
	public partial class WEBVISITORS
	{
		private readonly IWEBVISITORS dal=DataAccess.CreateWEBVISITORS();
		public WEBVISITORS()
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
		public bool Exists(int VISITORID,int USERID)
		{
			return dal.Exists(VISITORID,USERID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.WEBVISITORS model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.WEBVISITORS model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int VISITORID,int USERID)
		{
			
			return dal.Delete(VISITORID,USERID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.WEBVISITORS GetModel(int VISITORID,int USERID)
		{
			
			return dal.GetModel(VISITORID,USERID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.WEBVISITORS GetModelByCache(int VISITORID,int USERID)
		{
			
			string CacheKey = "WEBVISITORSModel-" + VISITORID+USERID;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(VISITORID,USERID);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.WEBVISITORS)objModel;
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
		public List<ZK.Model.WEBVISITORS> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.WEBVISITORS> DataTableToList(DataTable dt)
		{
			List<ZK.Model.WEBVISITORS> modelList = new List<ZK.Model.WEBVISITORS>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.WEBVISITORS model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.WEBVISITORS();
					if(dt.Rows[n]["VISITORID"]!=null && dt.Rows[n]["VISITORID"].ToString()!="")
					{
						model.VISITORID=int.Parse(dt.Rows[n]["VISITORID"].ToString());
					}
					if(dt.Rows[n]["VISITORCODE"]!=null && dt.Rows[n]["VISITORCODE"].ToString()!="")
					{
					model.VISITORCODE=dt.Rows[n]["VISITORCODE"].ToString();
					}
					if(dt.Rows[n]["USERID"]!=null && dt.Rows[n]["USERID"].ToString()!="")
					{
						model.USERID=int.Parse(dt.Rows[n]["USERID"].ToString());
					}
					if(dt.Rows[n]["SRCURL"]!=null && dt.Rows[n]["SRCURL"].ToString()!="")
					{
					model.SRCURL=dt.Rows[n]["SRCURL"].ToString();
					}
					if(dt.Rows[n]["CLIENTIPADDR"]!=null && dt.Rows[n]["CLIENTIPADDR"].ToString()!="")
					{
					model.CLIENTIPADDR=dt.Rows[n]["CLIENTIPADDR"].ToString();
					}
					if(dt.Rows[n]["CLIENTLOCATION"]!=null && dt.Rows[n]["CLIENTLOCATION"].ToString()!="")
					{
					model.CLIENTLOCATION=dt.Rows[n]["CLIENTLOCATION"].ToString();
					}
					if(dt.Rows[n]["CLIENTOS"]!=null && dt.Rows[n]["CLIENTOS"].ToString()!="")
					{
					model.CLIENTOS=dt.Rows[n]["CLIENTOS"].ToString();
					}
					if(dt.Rows[n]["WEBBROWSER"]!=null && dt.Rows[n]["WEBBROWSER"].ToString()!="")
					{
					model.WEBBROWSER=dt.Rows[n]["WEBBROWSER"].ToString();
					}
					if(dt.Rows[n]["REMARKNAME"]!=null && dt.Rows[n]["REMARKNAME"].ToString()!="")
					{
					model.REMARKNAME=dt.Rows[n]["REMARKNAME"].ToString();
					}
					if(dt.Rows[n]["REMARKTEXT"]!=null && dt.Rows[n]["REMARKTEXT"].ToString()!="")
					{
					model.REMARKTEXT=dt.Rows[n]["REMARKTEXT"].ToString();
					}
					if(dt.Rows[n]["FLAG"]!=null && dt.Rows[n]["FLAG"].ToString()!="")
					{
						model.FLAG=int.Parse(dt.Rows[n]["FLAG"].ToString());
					}
					if(dt.Rows[n]["ISACTIVE"]!=null && dt.Rows[n]["ISACTIVE"].ToString()!="")
					{
						model.ISACTIVE=int.Parse(dt.Rows[n]["ISACTIVE"].ToString());
					}
					if(dt.Rows[n]["LOGINTIMES"]!=null && dt.Rows[n]["LOGINTIMES"].ToString()!="")
					{
						model.LOGINTIMES=int.Parse(dt.Rows[n]["LOGINTIMES"].ToString());
					}
					if(dt.Rows[n]["LASTLOGINTIME"]!=null && dt.Rows[n]["LASTLOGINTIME"].ToString()!="")
					{
						model.LASTLOGINTIME=DateTime.Parse(dt.Rows[n]["LASTLOGINTIME"].ToString());
					}
					if(dt.Rows[n]["SENDMSGS"]!=null && dt.Rows[n]["SENDMSGS"].ToString()!="")
					{
						model.SENDMSGS=int.Parse(dt.Rows[n]["SENDMSGS"].ToString());
					}
					if(dt.Rows[n]["RECVMSGS"]!=null && dt.Rows[n]["RECVMSGS"].ToString()!="")
					{
						model.RECVMSGS=int.Parse(dt.Rows[n]["RECVMSGS"].ToString());
					}
					if(dt.Rows[n]["LEAVEMSGS"]!=null && dt.Rows[n]["LEAVEMSGS"].ToString()!="")
					{
						model.LEAVEMSGS=int.Parse(dt.Rows[n]["LEAVEMSGS"].ToString());
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

